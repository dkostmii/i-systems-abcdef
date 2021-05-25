import winston from 'winston'
import config from '../config'

const transportsOptions = process.env.NODE_ENV === 'development' ? {
  format: winston.format.combine(
    winston.format.cli(),
    winston.format.splat()
  ),
} : { }

if (!config.logs) {
  throw new Error("config.logs is undefined")
}
if (!config.logs.level) {
  throw new Error("config.logs.level is undefined")
}

const logger = winston.createLogger({
  level: config.logs.level,
  levels: winston.config.npm.levels,
  format: winston.format.combine(
    winston.format.timestamp({ format: 'YYYY-MM-DD HH:mm:ss' }),
    winston.format.errors({ stack: true }),
    winston.format.splat(),
    winston.format.json()
  ),
  transports: [ new winston.transports.Console(transportsOptions) ]
})

export default logger
