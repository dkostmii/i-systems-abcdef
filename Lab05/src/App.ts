import express, { Application } from 'express'

import 'reflect-metadata'

import loaders from './loaders'

export default async function(): Promise<Application> {
  return await loaders(await express())
}
