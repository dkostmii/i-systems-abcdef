import React from 'react'

import './ErrorPage.css'

function ErrorPage({ error }) {
  return (
    <div className="error-page-container">
      <h1>Error occurred: {error.name + ": " + error.message}</h1>
    </div>
  )
}

export default ErrorPage
