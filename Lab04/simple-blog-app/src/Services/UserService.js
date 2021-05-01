export default class UserService {
  constructor(host) {
    if (typeof host === "string") {
      this.host = host
    } else {
      throw new Error("Host URL is undefined")
    }
  }

  getAuthorName(id) {
    return this.getUser(id)
    .then(user => user.data.fullName,
          error => { return { error }})
  }

  getAllUsers() {
    return fetch(this.host + '/Users')
      .then(response => response.json())
      .then(json => { 
        console.log(json)
        if (json) {
          return { data: json }
        }
        return []
      }, error => { return { error } })
  }

  getUser(id) {
    return fetch(this.host + `/Users/${id}`)
      .then(response => response.json())
      .then(json => {
        if (json) {
          return { data: json }
        }
        return []
      }, error => { return { error }})
  }

  postUser(fullName, email) {
    return fetch(this.host + '/Users', {
      method: 'POST',
      headers: {
        'Content-Type':'application/json'
      },
      body: JSON.stringify({ fullName, email })
    })
    .then(response => response.json())
    .then(json => {
      if (json) {
        return { data: json }
      }
      return []
    }, error => { return { error } })
  }
}