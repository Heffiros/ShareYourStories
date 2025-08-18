import Joi from 'joi'

export interface IUserLoginDto {
  email: string
  password: string
}

export interface IUserSignupDto {
  email: string
  password: string
  authorName: string
}

export interface IUserDto {
  email:             string
  authorName:        string
  birthDate:         Date
  isAdmin:           Boolean       
  profilePictureUrl: String
  createdAt:         Date       
  updatedAt:         Date
}

export const loginSchema = Joi.object({
  email: Joi.string().email().required(),
  password: Joi.string().min(8).required(),
})

export const signupSchema = Joi.object({
  email: Joi.string().email().required(),
  password: Joi.string().min(8).required(),
  authorName: Joi.string().required(),
})
