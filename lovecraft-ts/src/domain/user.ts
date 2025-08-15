import bcrypt from 'bcryptjs'
import jwt from 'jsonwebtoken'
import { PrismaClient } from '@prisma/client'

const prisma = new PrismaClient()
const JWT_SECRET = 'supersecret' // plus tard mettre en env

export interface UserProps {
  email: string
  password: string
}

export class User {
  email: string
  private passwordHash: string
  id?: number

  constructor(props: { email: string; passwordHash: string; id?: number }) {
    this.email = props.email
    this.passwordHash = props.passwordHash
    this.id = props.id
  }

  static async register(props: UserProps) {
    const hashed = await bcrypt.hash(props.password, 10)
    const created = await prisma.user.create({
      data: { email: props.email, password: hashed }
    })
    return new User({ email: created.email, passwordHash: created.password, id: created.id })
  }

  static async login(email: string, password: string) {
    const record = await prisma.user.findUnique({ where: { email } })
    if (!record) throw new Error('Invalid credentials')

    const user = new User({ email: record.email, passwordHash: record.password, id: record.id })
    const valid = await user.verifyPassword(password)
    if (!valid) throw new Error('Invalid credentials')

    return user
  }

  async verifyPassword(password: string) {
    return bcrypt.compare(password, this.passwordHash)
  }

  generateToken() {
    return jwt.sign({ id: this.id, email: this.email }, JWT_SECRET, { expiresIn: '15m' })
  }

  generateRefreshToken() {
    return jwt.sign({ id: this.id }, JWT_SECRET, { expiresIn: '7d' })
  }
}
