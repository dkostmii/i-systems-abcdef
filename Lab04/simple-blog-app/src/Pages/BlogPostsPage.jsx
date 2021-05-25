import React, { useState, useEffect } from 'react'
import { Link, Route, Switch, useRouteMatch } from 'react-router-dom'

import BlogPost from '../Components/BlogPost'

import './BlogPostPage.css'

function BlogPostsPage({ blogPostService, userService }) {
  // State
  const [state, setState] = useState({
    data: [],
    isLoaded: false,
    error: null
  })

  const [post, setPost] = useState({
    authorId: null,
    title: '',
    contents: '',
  })

  const refreshPostData = data => setPost(prevState => { return {...prevState, ...data} })

  
  //URL match
  let match = useRouteMatch()

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
      <div className="blog-post-page-links">
        <Link to={match.url}>View Blog</Link>
        <Link to={`${match.url}/New`}>Create a blog post</Link>
      </div>
      
      <Switch>
        <Route exact path={match.path}>
          <h1>Blog Posts Page</h1>
          {render}
        </Route>

        <Route exact path={`${match.path}/New`}>
          <div className="new-post-form">
            <input type="text" 
                  placeholder="Author ID"
                  onChange={ e => refreshPostData({ authorId: e.target.value }) }
            />
            <input type="text" 
                  placeholder="Title"
                  onChange={ e => refreshPostData({ title: e.target.value }) }
            />
            <input type="text"
                  placeholder="Content"
                  onChange={ e => refreshPostData({ contents: e.target.value }) }
            />
            <button 
              onClick={ () => blogPostService.postBlogPost(post) }
              disabled={
                (!post.authorId && post.authorId !== 0) || !post.title || !post.contents || post.authorId < 0 }>
              Create
            </button>
          </div>
        </Route>
      </Switch>
    </div>
  )
}

export default BlogPostsPage
