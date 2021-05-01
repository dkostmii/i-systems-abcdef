import React, { useEffect, useState } from 'react'

import User from '../Components/User'

function UsersPage({ userService }) {
  const [state, setState] = useState({
    data: [],
    isLoaded: false,
    error: null
  })

  if (!userService.getAllUsers) {
    throw new Error('Provided userService is missing getAllUsers() method')
  }

  useEffect(() => {
    if (!state.isLoaded && !state.error) {
      userService.getAllUsers()
      .then(fetched => {
        // has error
        if (fetched.error) {
          console.log(fetched)
          setState(prevState => {
            return { ...prevState, error: fetched.error }
          })
        // is OK
        } else {
          setState(prevState => {
            // modify if empty only
            if (prevState.data.length === 0) {
              return { ...prevState, data: fetched.data, isLoaded: true }
            }

            return prevState
          })
        }
      })
    }
  }, [userService, state.error, state.isLoaded])

  const render = (() => {
    if (state.error) {
      return <p>Error occurred: {state.error.message}</p>
    } else if (!state.isLoaded) {
      return <p>Loading...</p>
    }

    return state.data.map(item => {
      return (
        <User
          key={item.id}
          name={item.fullName}
          email={item.email}
        />
      ) 
    })
  })()

  return (
    <div>
      <h1>Users Page</h1>
      {render}
    </div>
  )
}

export default UsersPage
