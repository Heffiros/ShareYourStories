import { User } from '../domain/user'

const users: User[] = []

export function saveUser(user: User) {
  users.push(user)
  return user
}

export function getAllUsers() {
  return users
}
