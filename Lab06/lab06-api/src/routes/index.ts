import { Router } from 'express'

import config from '../config'

import Auth from './auth'
import UserProfile from './user'
import AllUsers from './allusers'

import { buildUrl } from '../util/buildurl'

const router = Router()

export default function(): Router {
  Auth(router)
  AllUsers(router)
  UserProfile(router)

  // Verbose available routes
  router.get('/', (req, res) => {
    return res.status(200).json({ 
      auth: buildUrl(config.api.prefix, '/auth'),
      allusers: buildUrl(config.api.prefix, '/allusers'),
      profile: buildUrl(config.api.prefix, '/profile')
    })
  })

  return router
}