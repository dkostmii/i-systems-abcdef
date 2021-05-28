import { Container, Inject, Service } from 'typedi'

import { Op } from 'sequelize'

import argon2, { verify } from 'argon2'

import { IUser, IUserDb, IUserDTO, IUserSignInDTO, IUserSignUpDTO } from '../interfaces/user'
import { IUserRole } from '../interfaces/userRole'

import { Logger } from 'winston'

import config from '../config'

import { Model } from "sequelize"
import { read } from 'fs'
import { randomBytes } from 'crypto'

@Service()
export class AuthService {
  constructor(
    @Inject(config.db.dbReadPrefix + 'User') private readUserModel: any,
    @Inject(config.db.dbWritePrefix + 'User') private writeUserModel: any,
    @Inject(config.db.dbReadPrefix + 'UserRole') private readUserRoleModel: any,
    @Inject(config.db.dbWritePrefix + 'UserRole') private writeUserRoleModel: any,
    @Inject('logger') private logger: Logger
  ) { }

  hideSensitive(data: IUser): IUserDTO {
    return {
      id: data.id,
      email: data.email,
      phone: data.phone,
      fullName: data.fullName,
      role: data.role
    }
  }

  async attachUserRole(data: IUserDb): Promise<IUser> {
    return {
      id: data.id,
      email: data.email,
      phone: data.phone,
      password: data.password,
      salt: data.salt,
      fullName: data.fullName,
      role: await this.readUserRoleModel.findByPk(data.userRoleId),
    }
  }

  private async createPassword(password: string): Promise<{ password: string, salt: string }> {
    const salt = randomBytes(32)
    const pass = await argon2.hash(password, { salt })

    return { password: pass, salt: salt.toString('hex') }
  }

  private async verifyPassword(salted: string, password: string): Promise<{ password: string, salt: string }> {
    const verified = await argon2.verify(salted, password)

    if (verified) {
      // reusing logic here
      return await this.createPassword(password)
    }

    // Rejection here
    throw new Error("Invalid password!")
  }


  async signIn(data: IUserSignInDTO): Promise<{ user?: IUserDTO, error?: any }> {
    this.logger.info("[AuthService] Calling signIn operation in AuthService")

    this.logger.silly("[AuthService] Provided data: " + JSON.stringify(data))

    const found = await this.readUserModel.findOne({
      where: { 
        [Op.or]: [
          { email: data.login },
          { phone: data.login }
        ]
      }
    })

    const user = await this.writeUserModel.findByPk((found as IUserDb).id)

      if (!user) {
        return { error: { message: "Not found!", status: 404 } }
      }

    if (!found) {
      return { error: { message: "Not found!", status: 404 } }
    }

    try {
      const { password, salt } = await this.verifyPassword(found.password, data.password)

      await user.update({ password, salt })
    } catch (e) {
      return { error: { message: e, status: 401 } }
    }

    return { 
      user: this.hideSensitive(
          await this.attachUserRole(found)
        ) 
    }
  }

  async signUp(data: IUserSignUpDTO): Promise<{ user?: IUserDTO, error?: any }> {
    this.logger.info("[AuthService] Calling singUp operation in AuthService")

    this.logger.silly("[AuthService] Provided data: " + JSON.stringify(data))

    const found = await this.readUserModel.findOne({
      where: { 
        [Op.or]: [
          { email: data.email },
          { phone: data.phone }
        ]
      }
    })

    if (found) {
      return { error: { message: "Already registered!", status: 409 } }
    }

    const { password, salt } = await this.createPassword(data.password)

    const role = await this.readUserRoleModel.findOne({
      where: {
        name: data.role
      },
      raw: true
    })

    if (!role) {
      return { error: { message: "Role specified not found", status: 404 } }
    }

    const created = await this.writeUserModel.create({
      email: data.email,
      phone: data.phone,
      password,
      salt,
      fullName: data.fullName,
      userRoleId: role.id
    })

    if (!created) {
      return { error: { message: "Cannot create user record", status: 500 } }
    }

    return {
      user: {
        id: (created as IUserDb).id,        
        email: data.email,
        phone: data.phone,
        fullName: data.fullName,
        role: role,
      }
    }
  }
}
