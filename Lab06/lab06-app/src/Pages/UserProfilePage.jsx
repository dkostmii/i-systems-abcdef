import React, { useState, useEffect } from 'react'

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
    <div>
      { !state.loaded && <h1>Loading...</h1> }
      { state.profile && !state.error &&
          <h1>Welcome, { state.profile.fullName }</h1> }
      {
        state.error && <p>Error occurred: {state.error}</p>
      }
    </div>
  )
}

export default UserProfilePage
