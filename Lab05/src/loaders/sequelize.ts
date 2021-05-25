'use strict';

import fs from "fs"
import path from "path"

import { 
 

  Sequelize as SequelizeType
} from 'sequelize'

const Sequelize = require("sequelize")


import Logger from './logger'

// Dir with models
const basename = path.resolve(path.join(__dirname, '..', 'models'))

console.log(basename)

// Dev or Production?
const env = process.env.NODE_ENV || 'development';

export default async function(): Promise<DbMultiObject> {

  const configRead = require(__dirname + '/../config/config.json')[env]["PSEN_READ"]
  const configWrite = require(__dirname + '/../config/config.json')[env]["PSEN_WRITE"]

  const db: DbMultiObject = {
    dbRead: { },
    dbWrite: { }
  };

  let sequelize : { dbRead: SequelizeType, dbWrite: SequelizeType };

  sequelize = {
    dbRead: new Sequelize(
      configRead.database,
      configRead.username,
      configRead.password,
      configRead
    ),
    dbWrite: new Sequelize(
      configWrite.database,
      configWrite.username,
      configWrite.password,
      configWrite
    )
  };

  sequelize.dbRead.sync()
    .then(() => {
      Logger.silly(`[Sequelize] Connection to ${configRead.database} db successful`)
    })

  sequelize.dbWrite.sync()
    .then(() => {
      Logger.silly(`[Sequelize] Connection to ${configWrite.database} db successful`)
    })


  fs
    .readdirSync(basename)
    .filter(file => {
      return (file.indexOf('.') !== 0) && (file !== basename) && (file.slice(-3) === '.js');
    })
    .forEach(file => {
      const readModel = require(path.join(basename, file))(sequelize.dbRead, Sequelize.DataTypes)
      const writeModel = require(path.join(basename, file))(sequelize.dbWrite, Sequelize.DataTypes)

      db.dbRead[readModel.name] = readModel;
      db.dbWrite[writeModel.name] = writeModel;
    });

  Object.keys(db.dbRead).forEach(modelName => {
    if ((db.dbRead[modelName] as AssociateModel).associate) {
      (db.dbRead[modelName] as AssociateModel).associate(db.dbRead);
    }
  });

  Object.keys(db.dbWrite).forEach(modelName => {
    if ((db.dbWrite[modelName] as AssociateModel).associate) {
      (db.dbWrite[modelName] as AssociateModel).associate(db.dbWrite)
    }
  })

  return db;
}
