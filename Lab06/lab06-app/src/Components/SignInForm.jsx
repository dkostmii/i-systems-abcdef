import React, { useState } from 'react'

import { 
  validateEmail,
  validatePhone,
  validatePassword } from '../Util/Validate'

function SignInForm({ refreshUser, submit }) {
  const [state, setState] = useState({
    login: '',
    password: '',
  })

  const valid = () => {
    return (validateEmail(state.login) || validatePhone(state.login)) && validatePassword(state.password)
  }

  const [error, setError] = useState(null)

  return (
    <div>
      <label>
        Login
        <input type="text"
          onInput={
            e => setState(prevState => {
              return {
                ...prevState,
                login: e.target.value
              }
            })
          }
        />
      </label>

      <label>
        Password
        <input type="password" 
          onInput={
            e => setState(prevState => { 
              return {
                ...prevState,
                password: e.target.value,
              } 
            })
          }
        />
      </label>

      <button 
        disabled={ !valid() }
        onClick={ () => {
            submit(state)
              .then(response => {
                if (response.error) {
                  setError(response.error.toString())
                } else if (response.data) {
                  refreshUser({ 
                    id: response.data.user.id,
                    name: response.data.user.fullName
                  })
                } else if (response.data.message) {
                  setError(response.data.message)
                }
              })
          }
        }
      >Sign In</button>
      { error && <p style={{color: 'red'}}>Error occurred: {error}</p>}
    </div>
  )
}

export default SignInForm
