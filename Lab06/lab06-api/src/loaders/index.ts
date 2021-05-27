import { Application } from 'express'

import hostcheckLoader from './hostcheck'
import expressLoader from './express'
import sequelizeLoader from './sequelize'
import dependencyInjector from './dinjector'

import config from '../config'

import Logger from './logger'
import { Container } from 'winston'


export default async function(app: Application): Promise<Application> {
  Logger.info("Logger level: " + Logger.level)

  // Verify provided EXTHOST
  if (config.host) {
    await hostcheckLoader()
    Logger.info("Hostcheck loaded!")
  }

  await expressLoader({ app })
  Logger.info("Express loaded!")

  sequelizeLoader()
    .then(async db => {
      Logger.info("Sequelize loaded database models!")

      if (Object.keys(db.dbRead).length <= 1) {
        Logger.warn("Empty read db object!")
      }

      if (Object.keys(db.dbWrite).length <= 1) {
        Logger.warn("Empty write db object!")
      }

      Logger.silly("Read db object models: " + JSON.stringify(Object.keys(db.dbRead)))
      Logger.silly("Write db object models: " + JSON.stringify(Object.keys(db.dbWrite)))

      await dependencyInjector(db)
      Logger.info("Dependency injector loaded!")
    })

  return app
}
