import { Router } from 'express'

import { Logger } from 'winston'

import { Container } from 'typedi'

import config from '../config'

import { AuthService } from '../services/auth'
import { IUserSignInDTO, IUserSignUpDTO } from '../interfaces/user'
import { sign } from 'crypto'

import { buildUrl } from '../util/buildurl'

const route = Router()

export default function(app: Router) { 
  app.use('/auth', route)

  // Verbose available routes
  route.get('/', (req, res) => {
    return res.status(200).json({
      signIn: buildUrl(config.api.prefix, '/auth/signin'),
      singUp: buildUrl(config.api.prefix, '/auth/signup'),
    })
  })

  route.post('/signin', async (req, res, next) => {
    const logger: Logger = Container.get('logger')

    logger.info('[/auth] Calling /auth/signin endpoint with body: ' + JSON.stringify(req.body))

    const authServiceInstance = Container.get(AuthService)
    try {
      const signInResult = await authServiceInstance.signIn(req.body as IUserSignInDTO)
      if (signInResult.user) {
        return res.json({ message: "Success!", user: signInResult.user }).status(200)
      } else {
        return res.json({ 
          message: signInResult.error.message || "Error"
        }).status(signInResult.error.status || 400)
      }
    }
    catch (e) {
      logger.error("[/auth] " + e);
      next(e)
    }
  })

  route.post('/signup', async (req, res, next) => {
    const logger: Logger = Container.get('logger')

    logger.info('[/auth] Calling /auth/signup endpoint with body: ' + JSON.stringify(req.body))

    const authServiceInstance = Container.get(AuthService)

    const signUpResult = await authServiceInstance.signUp(req.body as IUserSignUpDTO)
    if (signUpResult.user) {
      return res.json({ message: "Successfully signed up!", user: signUpResult.user }).status(200)
    } else {
      return res.json({ message: ""})
    }

    return res.json({ message: "Success!" }).status(200)
  })
}
