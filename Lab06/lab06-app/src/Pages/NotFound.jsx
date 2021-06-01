import React from 'react'

import { Link } from 'react-router-dom'

import './NotFound.css'

function NotFound() {
  return (
    <div className="not-found-container">
      <h1>Page not found</h1>
      <Link to="/">Return to main page</Link>
    </div>
  )
}

export default NotFound
