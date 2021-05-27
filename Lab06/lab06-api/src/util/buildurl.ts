import { Container } from 'typedi'

import config from '../config'

export function buildUrl(...path: string[]): string {
  const postfix = path.length > 0 ? path.join('') : '/'

  if (Container.has('HOST_VERIFIED') && Container.get('HOST_VERIFIED')) {

    return 'http://' + config.host.name + ':' + config.port + postfix
  }
  return 'http://localhost:' + config.port + postfix
}
