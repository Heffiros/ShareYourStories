// types/auth.d.ts
export interface User {
  id: number
  email: string
  authorName: string
  birthDate: Date
  isAdmin: Boolean
  profilePictureUrl: string
  createdAt: Date
  updatedAt: Date
}

export interface LoginCredentials {
  email: string
  password: string
}

export interface LoginResponse {
  token: string
  user: User
}
