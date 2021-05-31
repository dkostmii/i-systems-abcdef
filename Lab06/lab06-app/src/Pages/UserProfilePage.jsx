import React, { useState, useEffect } from 'react'

import User from '../Components/User'

import './UserProfilePage.css'

function UserProfilePage({ user, userService }) {
  const [state, setState] = useState({
    profile: undefined,
    error: null,
    loaded: false,
  })

  useEffect(() => {
    if (!state.profile && !state.error && userService) {
      (async () => {
        const response = await userService.getUser(user.id)
        if (response.data) {
          setState(prevProps => {
            return {...prevProps, profile: response.data.user, loaded: true }
          })
        } else if (response.error) {
          setState(prevProps => {
            return {...prevProps, error: response.error.toString() }
          })
        }
      })()
    }
  }, [state.profile, state.error, user.id, userService])

  return (
    <div className="userprofile-page-container">
      { !state.loaded && <h1>Loading...</h1> }
      { state.profile && !state.error &&
          <h1>Welcome, { state.profile.fullName }</h1> }
      {
        state.error && <p>Error occurred: {state.error}</p>
      }
      { state.profile &&
      <>
        Your data: 
        <User 
          fullName={state.profile.fullName} 
          phone={state.profile.phone}
          email={state.profile.email}
          role={state.profile.role}
          />
      </> }
    </div>
  )
}

export default UserProfilePage
