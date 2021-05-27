import { Container } from 'typedi'

import { randomBytes } from 'crypto'

import Logger from './logger'

export default async function() {
  const token = randomBytes(32).toString('hex')

  Container.set('HOST_TOKEN', token)
  Logger.silly('[HOSTCHECK] Injected token: ' + token)
}