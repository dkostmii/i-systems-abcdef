import { Model } from "sequelize"

declare global {
  export interface DbObject {
    sequelize?: DbObject,
    [modelName: string]: any
  }

  export interface AssociateModel extends Model {
    associate: (db: DbObject) => void;
  }

  export interface DbMultiObject {
    dbRead: DbObject,
    dbWrite: DbObject
  }
}
