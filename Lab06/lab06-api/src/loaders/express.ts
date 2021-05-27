import express, { Application, Request, Response, NextFunction } from 'express'

import { Container } from 'typedi'

import cors from 'cors'

import config from '../config'
import routes from '../routes'

import Logger from './logger'
import morgan from 'morgan'

export default async function({ app }: { app: Application }): Promise<void> {
  app.use(express.json())
  app.use(cors())
  app.use(morgan(':method :url :status :res[content-length] - :response-time ms'))

  app.use(config.api.prefix, routes())
  Logger.info(`[Express] API is running on: http://localhost:${config.port}${config.api.prefix}`)

  // Hostcheck endpoint


  app.get('/hostcheck', (req, res, next) => {
    const token = Container.has('HOST_TOKEN') && Container.get('HOST_TOKEN')

    if (token && (token as string).length > 0) {
      Container.set('HOST_TOKEN', '')
      return res.status(200).json({ token })
    }
    return next()
  })

  // Healthcheck endpoints
  app.get('/status', (req, res) => {
    res.status(200).end()
  })

  app.head('/status', (req, res) => {
    res.status(200).end()
  })

  // 404
  app.use((req, res, next) => {
    next({ error: new Error("Not found"), status: 404 })
  })

  // Error handling
  app.use((err: { error: Error, status?: number },
           req: Request,
           res: Response,
           next: NextFunction) => {
            
            Logger.error(`${req.method} ${req.url} - Status: ${err.status || 500} Error: ${err.error ? err.error.message : err}`)
            res.status(err.status || 500).json({ message: err.error ? err.error.message : err })
  })
}
