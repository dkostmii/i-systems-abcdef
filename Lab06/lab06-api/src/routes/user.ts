import { Router } from 'express'

import { Logger } from 'winston'

import { Container } from 'typedi'

import config from '../config'

import { buildUrl } from '../util/buildurl'

import { IUserSignUpDTO, IUserDb } from '../interfaces/user'

import { AuthService } from '../services/auth'

const route = Router()

export default function(app: Router) {
  app.use('/profile', route)

  route.get('/', (req, res) => {
    const logger: Logger = Container.get('logger')
    logger.info('[/profile] Calling / endpoint with body: ' + JSON.stringify(req.body))
    
    const authServiceInstance = Container.get(AuthService)

    // id exists if user logged in
    const { id } = req.query as { id?: number }

    if (id) {
      const readUserModel: any = Container.get(config.db.dbReadPrefix + 'User')

      return readUserModel.findByPk(id)
        .then(async (found?: IUserDb) => {
          if (found) {
            return res.status(200).json({
              user: authServiceInstance.hideSensitive(
                await authServiceInstance.attachUserRole(found)
              )
            })
          }
          return res.status(404).json({ message: 'User id not found' })
        })
        .catch(() => res.status(500).json({ message: 'Cannot fetch user data' }))
    }

    return res.status(401).json({ message: 'Not authorized' })
  })

  route.put('/', (req, res) => {
    const logger: Logger = Container.get('logger')
    logger.info('[/profile] Calling / endpoint with body: ' + JSON.stringify(req.body))

    const authServiceInstance = Container.get(AuthService)

    // id exists if user logged in
    const { id } = req.body as { id?: number }

    const updateData: Partial<IUserSignUpDTO> = req.body as Partial<IUserSignUpDTO>

    if (id) {
      const writeUserModel: any = Container.get(config.db.dbWritePrefix + 'User')

      return writeUserModel.findByPk(id)
        .then((user?: IUserDb) => {
          if (!user) {
            return res.status(404).json({ message: 'User with provided id not found'})
          }

          return (user as any).update(updateData)
            .then(async (updatedUser?: IUserDb) => {
              if (!updatedUser) {
                return res.status(500).json({ message: 'Error on updating user record' })
              }

              return res.status(200).json({
                user: authServiceInstance.hideSensitive(
                  await authServiceInstance.attachUserRole(updatedUser)
                )
              })
            })
        })
    }

    return res.status(401).json({ message: 'Not authorized' })
  })
}