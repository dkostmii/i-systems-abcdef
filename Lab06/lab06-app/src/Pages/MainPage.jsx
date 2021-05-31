import React from 'react'

import './MainPage.css'

function MainPage({ user }) {
  return (
    <div className="main-page-container">
      <h1>Lab06 Three Tier Project</h1>

      { user && <h2>Welcome, { user.name }</h2> }

      <p>This is the frontend for Lab06 Three Tier Project.</p>
      <p>This frontend uses the LocalStorage to store user data. Check it with command <b>window.localStorage.getItem('user') in your browser console.</b></p>

      <p>As a backend this project uses PostgreSQL, Sequelize, Express, Node.js as a RESTful API</p>
    </div>
  )
}

export default MainPage
