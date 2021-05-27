import { Application } from 'express'

import { Container } from 'typedi'

import config from '../config'

import Logger from './logger'

export default async function(db: DbMultiObject): Promise<void> {
  Container.set('logger', Logger)

  for (let model in db.dbRead) {
    Container.set(config.db.dbReadPrefix + model, db.dbRead[model])
  }

  for (let model in db.dbWrite) {
    Container.set(config.db.dbWritePrefix + model, db.dbWrite[model])
  }
}