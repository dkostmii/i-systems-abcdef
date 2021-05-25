export default class BlogPostService {
  constructor(host) {
    if (typeof host === "string") {
      this.host = host
    } else {
      throw new Error("Host URL is undefined")
    }
  }

  getAllPosts() {
    return fetch(this.host + '/BlogPosts')
      .then(response => response.json())
      .then(json => {
        if (json) {
          return { data: json }
        }
        return []
      }, error => { return { error } })
  }

  getPost(id) {
    return fetch(this.host + `/BlogPosts/${id}`)
      .then(response => response.json())
      .then(json => {
        if (json) {
          return { data: json }
        }
        return []
      }, error => { return { error }})
  }

  postBlogPost({ title, authorId, contents }) {
    return fetch(this.host + '/BlogPosts', {
      method: 'POST',
      headers: {
        'Content-Type':'application/json'
      },
      body: JSON.stringify({ title, authorId, contents })
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