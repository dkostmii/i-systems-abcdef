import dotenv from 'dotenv'

const envFound = dotenv.config()
if (envFound.error) {
  throw new Error(`Error on loading .env: ${envFound.error.message}`)
}
process.env.NODE_ENV = process.env.NODE_ENV || 'development'

const port = parseInt(process.env.port ? process.env.port : '5000', 10)

const host = process.env.EXTHOST ? {
  name: process.env.EXTHOST
} : { }

const config: any = {
  api: {
    prefix: '/api'
  },
  logs: {
    level: process.env.LOG_LEVEL || 'silly',
  },
  db: {
    dbReadPrefix: 'read_',
    dbWritePrefix: 'write_'
  },
  port
}

if (process.env.EXTHOST) {
  config.host = host;
}

export default config
