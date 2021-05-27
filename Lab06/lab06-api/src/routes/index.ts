import { Router } from 'express'

import config from '../config'

import Auth from './auth'

import { buildUrl } from '../util/buildurl'

const router = Router()

export default function(): Router {
  Auth(router)

  // Verbose available routes
  router.get('/', (req, res) => {
    return res.status(200).json({ 
      auth: buildUrl(config.api.prefix, '/auth')
    })
  })

  return router
}