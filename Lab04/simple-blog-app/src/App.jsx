import React from 'react'

import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from 'react-router-dom'

import BlogPostsPage from './Pages/BlogPostsPage'
import UsersPage from './Pages/UsersPage'

import ServiceProvider from './Services/ServiceProvider'

import './App.css'

function App() {
  const host = "https://localhost:44354"

  const blogPostService = ServiceProvider.getServiceInstance('BlogPostService', host)
  const userService = ServiceProvider.getServiceInstance('UserService', host)

  return (
    <Router>
      <div className='app-container'>
        <div className='header-container'>
          <div className='logo-container'>
            <Link to="/">Simple blog</Link>
          </div>
          <nav>
            <Link to="/BlogPosts">Blog Posts</Link>
            <Link to="/Users">Authors</Link>
          </nav>
        </div>
        <div className='app-heading-container'>
          <h1>simple-blog</h1>
          <p>A simple blog web application</p>
        </div>

            <Switch>
              <Route exact path="/">
                <div className="page-description-container">
                  <div className="page-description-wrap">This is the main page. Navigate by links in the header</div>
                </div>
              </Route>

              <Route path="/BlogPosts">
                <div className='page-container'>
                  <div className='page-container-wrap'>
                    <BlogPostsPage 
                      blogPostService={blogPostService}
                      userService={userService}
                      />
                  </div>
                </div>
              </Route>

              <Route path="/Users">
                <div className='page-container'>
                  <div className='page-container-wrap'>
                    <UsersPage userService={userService} />
                  </div>
                </div>
              </Route>
            </Switch>
      </div>
    </Router>
  )
}

export default App

