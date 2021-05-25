import app from './App'

import config from './config'

import Logger from './loaders/logger'


(async (): Promise<void> => {
  (await app()).listen(config.port, () => {
    Logger.info(`[App] Server is listening on ${config.host}/`)
  })
})()