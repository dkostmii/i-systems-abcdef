import { Router } from 'express'

import { Logger } from 'winston'

import { Container } from 'typedi'

import config from '../config'

import { IUserRole } from '../interfaces/userRole'

import { buildUrl } from '../util/buildurl'

const route = Router()

export default function(app: Router) {
  app.use('/getroles', route)

  route.get('/', (req, res) => {
    const logger: Logger = Container.get('logger')
    logger.info('[/getroles] Calling /getroles endpoint with body: ' + JSON.stringify(req.body))

    const readUserRoleModel: any = Container.get(config.db.dbReadPrefix + 'UserRole')

    return readUserRoleModel.findAll()
      .then((list: IUserRole) => {
        if (!list) {
          return res.status(500).json({ message: 'Error fetching user roles list' })
        }

        return res.status(200).json({ results: list })
      })
  })
}