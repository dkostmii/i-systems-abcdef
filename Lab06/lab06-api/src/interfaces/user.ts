import { IUserRole } from './userRole'

export interface IUser {
  id: number,
  email: string,
  phone: string,
  password: string,
  salt: string,
  fullName: string,
  role: IUserRole,
}

export type IUserDb = Omit<IUser, "role"> & { userRoleId: number }

export type IUserDTO = Omit<IUser, "password" | "salt">;

export type IUserPublic = Omit<IUser, "password" | "salt" | "email" | "phone">;

export interface IUserSignInDTO {
  login: string,
  password: string, // non hashed password
}

export interface IUserSignUpDTO {
  email: string,
  phone: string,
  password: string,
  fullName: string,
  role: string,
}
