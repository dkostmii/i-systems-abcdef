import { UserService } from '../Services/UserService'
import { UserRoleService } from '../Services/UserRoleService'

async function loadUrlTree(apiurl) {
  let data = { }

  return fetch(apiurl)
    .then(response => response.json())
    .then(
      json => {
        if (!json) {
          throw new Error('Error loading URL tree from server')
        }

        data = json

        return fetch(data.auth)
      },
      error => { throw new Error('Error connecting to server') })
    .then(response => response.json())
    .then(
      auth => {
        if (!auth) {
          throw new Error('Error loading signin URL from server')
        }

        return { ...data, auth }
      },
      error => { throw new Error('Error connecting to server') }
    )
}

export async function ServiceProvider(apiurl) {
  if (!apiurl) {
    throw new Error('API URL is not provided')
  }
  const urlTree = await loadUrlTree(apiurl)

  return function(serviceName) {
    switch(serviceName) {
      case 'UserService':
        return new UserService(urlTree)
        
      case 'UserRoleService':
        return new UserRoleService(urlTree)

      default:
        throw new Error(`Service ${serviceName} not found`)
    }
  }
}