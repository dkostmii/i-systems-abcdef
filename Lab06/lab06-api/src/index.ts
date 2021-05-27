import app from './App'

import config from './config'

import Logger from './loaders/logger'

import http from 'http'
import Container from 'typedi'

import { buildUrl } from './util/buildurl'

function Message(verified?: boolean) {
  Logger.info('[App] Server is listening on:')
  Logger.info(`[App] local: http://localhost:${config.port}/`)

  if (verified) {
    Logger.info(`[App] external: ${buildUrl()}`)
  }
}

(async (): Promise<void> => {
  (await app()).listen(config.port, () => {
    if (config.host) {

      const token: any = Container.has('HOST_TOKEN') && Container.get('HOST_TOKEN')

      const options = {
        hostname: config.host.name,
        port: config.port,
        method: 'GET',
        path: '/hostcheck'
      }

      const req = http.request(options, res => {
        let body = ''

        res.on('data', data => {
          body += data
        })

        res.on('end', () => {
          let jsonData: { token?: string } = JSON.parse(body)

          if (jsonData.token) {
            Logger.silly("[HOSTCHECK] Got token: " + jsonData.token)
            if (token && jsonData.token == (token as string)) {
              Container.set('HOST_VERIFIED', true)
              Message(true)
            } else {
              Logger.error("Cannot verify provided EXTHOST!")
              Container.set('HOST_VERIFIED', false)
              Message(false)
            }
          }
        })
      })

      req.on('error', err => {
        Logger.error("Cannot verify provided EXTHOST!")

        Logger.silly(err)

        Container.set('HOST_VERIFIED', false)

        Message(false)
      })

      req.end()
    } else {
      Logger.warn("[HOSTCHECK] Missing EXTHOST in ENV! Server will run in local mode.")
      Message()
    }
  })
})()