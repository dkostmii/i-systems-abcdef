import React, { useState } from 'react'

import { 
  validateEmail,
  validatePhone,
  validatePassword } from '../Util/Validate'

import './SignInForm.css'

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
    <div className="signin-form-container">
      <label>
        <span className="caption">
          Login
        </span>
        <input type="text"
          placeholder="abc123@poczta.pl"
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
        <span className="caption">
          Password
        </span>
        <input type="password"
          placeholder="hasło"
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
        className="submit-button"
        disabled={ !valid() }
        onClick={ () => {
            submit(state)
              .then(response => {
                if (response.error) {
                  setError(response.error.toString())
                } else if (response.data && response.data.user) {
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
