import UserService from './UserService'
import BlogPostService from './BlogPostService'

// Singleton service provider

export default class ServiceProvider {
  static userService
  static blogPostService

  static getServiceInstance(serviceName, host) {
    if (typeof serviceName !== 'undefined') {
      switch (serviceName) {
        case 'BlogPostService':
          if (!this.blogPostService) {
            this.blogPostService = new BlogPostService(host)
          }
          return this.blogPostService

        case 'UserService':
          if (!this.userService) {
            this.userService = new UserService(host)
          }
          return this.userService

        default:
          throw new Error(`Service ${serviceName} doesn't exist`)
      }
    }
    throw new Error('serviceName is not provided')
  }
}
