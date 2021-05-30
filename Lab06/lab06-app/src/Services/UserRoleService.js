export class UserRoleService {
  constructor(urlTree) {
    this.urlTree = urlTree
  }

  async getAll() {
    if (this.urlTree) {
      return fetch(this.urlTree.roles)
        .then(response => response.json())
        .then(
          json => {
            if (!json) {
              return []
            }

            return { data: json }
          },
          error => {
            return { error }
          }
        )
    }
    return { error: 'UrlTree is not loaded'}
  }
}