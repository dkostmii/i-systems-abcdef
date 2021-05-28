import { Router } from 'express'

import { Logger } from 'winston'

import { Container } from 'typedi'

import config from '../config'

import { buildUrl } from '../util/buildurl'

import { IUserSignUpDTO, IUserDb } from '../interfaces/user'

import { AuthService } from '../services/auth'

const route = Router()


export default function(app: Router) {
  app.use('/allusers', route)

  route.get('/', (req, res) => {
    const logger: Logger = Container.get('logger')
    logger.info('[/allusers] Calling / endpoint with body: ' + req.body)

    const { limit, offset } = req.query as { limit?: number, offset?: number }

    const authServiceInstance = Container.get(AuthService)

    const readUserModel: any = Container.get(config.db.dbReadPrefix + 'User')


    return readUserModel.findAll({ limit: limit || 20, offset: offset || 0 })
      .then(async (list?: IUserDb[]) => {
        if (!list) {
          return res.status(500).json({ message: 'Error fetching users records list' })
        }

        const count: number = await readUserModel.count({ })

        const resData: {
          count: number,
          next?: string,
          prev?: string,
          results: any[]
        } = {
          count,
          results: await Promise.all(
            list.map(user => {
              return authServiceInstance.attachUserRole(user)
                .then(attached => authServiceInstance.hideSensitive(attached))
                .catch(err => {
                  logger.error(`Error when attaching user role (id: ${user.id}): ` + err)
                })
            })
          )
        }
        
        if ((offset || 0) > 0) {
          resData.prev = buildUrl(config.api.prefix, `/allusers?limit=${limit || 20}&offset=${(offset || 0) - 1}`)
        }
        if ((offset || 0) < count / (limit || 20)) {
          resData.next = buildUrl(config.api.prefix, `/allusers?limit=${limit || 20}&offset=${(offset || 0) + 1}`)
        }

        return res.status(200).json(resData)
      })
      .catch((err: string) => {
        logger.error(err)

        return res.status(500).json({ message: 'Error fetching user records' })
      })
  })
}