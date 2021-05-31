import React, { useState, useEffect } from 'react'

import User from '../Components/User'


import './ListUsersPage.css'

function ListUsersPage({ user, userService }) {
  const [state, setState] = useState({
    users: [],
    loaded: false,
    error: null
  })

  useEffect(() => {
    if (!state.error && !state.loaded && userService) {
      userService.getAll(user && user.id)
        .then(users => {
          if (users.error) {
            setState(prevState => {
              return { ...prevState, error: users.error }
            })
          } else {
            setState(prevState => { 
              return { ...prevState, loaded: true, users: users.data.results } 
            })
          }
        })
    }
  }, [user, userService, state.error, state.loaded])

  const render = state.users.map(user => {
    return (
      <User
        key={user.id}
        fullName={ user.fullName }
        role={ user.role }
        email={ user.email }
        phone={ user.phone } />
    )
  }) 

  return (
    <div className="list-users-page-container">
      <h1>ListUsersPage</h1>
      { state.loaded && render }
      { state.error && <p>Error occurred: {state.error} </p>}
    </div>
  )
}

export default ListUsersPage
