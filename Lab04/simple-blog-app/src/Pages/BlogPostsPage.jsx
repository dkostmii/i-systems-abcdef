import React, { useState, useEffect } from 'react'

import BlogPost from '../Components/BlogPost'

function BlogPostsPage({ blogPostService, userService }) {
  // State
  const [state, setState] = useState({
    data: [],
    isLoaded: false,
    error: null
  })

  // Validating services
  if (!blogPostService.getAllPosts) {
    throw new Error('Provided blogPostService is missing getAllPosts() method')
  }

  if (!userService.getAuthorName) {
    throw new Error('Provided userService is missing getAuhorName(id) method')
  }

  // First render, every next render, when component destroys
  useEffect(() => {
    if (!state.isLoaded && !state.error) {
      blogPostService.getAllPosts()
      .then(fetched => {
        // has error
        if (fetched.error) {
          setState(prevState => {
            return { ...prevState, error: fetched.error }
          })
        // is OK
        } else {

          // load author names
          const final = fetched.data.map(item => {
            return userService.getAuthorName(item.authorId)
            .then(authorName => {

              // failed to fetch
              if (authorName.error) {
                console.log("Failed to fetch the author name")
                return {...item, authorId: undefined, authorName: `Error: ${authorName.error}` }
              }

              // OK
              return {...item, authorId: undefined, authorName }
            })
          })

          // add data to component state when it's loaded
          Promise.all(final)
          .then(final => {
            setState(prevState => {
              return { ...prevState, data: final, isLoaded: true }
            })
          }, error => { 
            setState(prevState => {
              return { ...prevState, error }
            })
          })
        }
      })
    }
  }, [blogPostService, userService, state.error, state.isLoaded])

  const render = (() => {
    if (state.error) {
      return <p>Error occurred: {state.error.message}</p>
    } else if (!state.isLoaded) {
      return <p>Loading...</p>
    }

    return state.data.map(item => {
      return (
        <BlogPost 
          key={item.id}
          title={item.title}
          authorName={item.authorName}
          contents={item.contents}
        />
      )
    })
  })()

  return ( 
    <div>
      <h1>Blog Posts Page</h1>
      {render}
    </div>
  )
}

export default BlogPostsPage
