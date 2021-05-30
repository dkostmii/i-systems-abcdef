export class UserService {
  constructor(urlTree) {
    this.urlTree = urlTree
  }

  async getAll(id) {
    const url = this.urlTree.allusers + (id ? '?id=' + id : '')
    if (this.urlTree) {
      return fetch(url)
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
    return { error: 'UrlTree is not loaded' }
  }

  async getUser(id) {
    if (this.urlTree) {
      return fetch(this.urlTree.profile + '?id=' + id, {
        method: 'GET'
      })
      .then(response => response.json())
      .then(
        json => {
          if (!json) {
            throw new Error('No response data from server')
          }

          return { data: json }
        },
        error => {
          return { error }
        }
      )
    }
    return { error: 'UrlTree is not loaded' }
  }

  async updateUser(id) {
    if (this.urlTree) {
      return fetch(this.urlTree.profile, {
        body: {
          'id': id
        },
        method: 'PUT',
      })
      .then(response => response.json())
      .then(
        json => {
          if (!json) {
            throw new Error('No response data from server')
          }

          return { data: json }
        },
        error => {
          return { error }
        }
      )
    }
    return { error: 'UrlTree is not loaded' }
  }

  async signIn(data) {
    if (!data) {
      throw new Error('No sign-in data provided')
    }

    if (this.urlTree) {
      return fetch(this.urlTree.auth.signIn, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
          'Content-Type': 'application/json'
        }
      })
      .then(response => response.json())
      .then(
        json => {
          if (!json) {
            throw new Error('No response from server')
          }

          return { data: json }
        },
        error => { return { error } }
      )
    }
    return { error: 'UrlTree is not loaded' }
  }

  async signUp(data) {
    if (!data) {
      throw new Error('No sign-up data provided')
    }
    if (this.urlTree) {
      return fetch(this.urlTree.auth.signUp, {
        method: 'POST',
        body: JSON.stringify(data),
        headers: {
          'Content-Type': 'application/json'
        }
      })
      .then(response => response.json())
      .then(
        json => {
          if (!json) {
            throw new Error('No response from server')
          }

          return { data: json }
        },
        error => { return { error } }
      )
    }
    return { error: 'UrlTree is not loaded' }
  }
}
