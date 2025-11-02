import { User } from '@prisma/client'
import { IUserDto } from '../schemas/User'

export const toUserDto = (user: User): IUserDto => ({
  id: user.id,
  email: user.email,
  authorName: user.authorName,
  birthDate: user.birthDate,
  isAdmin: user.isAdmin,
  profilePictureUrl: user.profilePictureUrl,
  createdAt: user.createdAt,
  updatedAt: user.updatedAt
})