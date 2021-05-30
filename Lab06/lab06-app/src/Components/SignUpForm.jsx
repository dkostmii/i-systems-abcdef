import React, { useState, useEffect } from 'react'

import {
  validateEmail,
  validatePhone,
  validatePassword
} from '../Util/Validate'

function SignUpForm({ refreshUser, submit, getUserRoles }) {
  const [state, setState] = useState({
    email: '',
    phone: '',
    fullName: '',
    password: '',
    role: '',
    hidePass: true,
  })

  const valid = () => {
    return (
      validateEmail(state.email) &&
      validatePhone(state.phone) &&
      validatePassword(state.password)
    )
  }

  const [error, setError] = useState(null)

  const [userRoles, setUserRoles] = useState([])


  useEffect(() => {
    getUserRoles()
    .then(roles => {
      if (roles && roles.results) {
        setUserRoles(roles.results)
      } else if (roles.message) {
        setError(roles.message)
      }
    })
  }, [getUserRoles])

  return (
    <div>
      <label>
        FullName
        <input type="text" 
          onInput={
            e => setState(prevState => {
              return {
                ...prevState,
                fullName: e.target.value
              }
            })
          }
        />
      </label>

      <label>
        Email
        <input type="email" 
          onInput={
            e => setState(prevState => {
              return {
                ...prevState,
                email: e.target.value,
              }
            })
          }
        />
      </label>

      <label>
        Phone
        <input type="tel"
          onInput={
            e => setState(prevState => {
              return {
                ...prevState,
                phone: e.target.value,
              }
            })
          }
        /> 
      </label>

      <label>
        Password
        <input type={ state.hidePass ? "password" : "text" }
          onInput={ 
            e => setState(prevState => {
              return {
                ...prevState,
                password: e.target.value,
              }
            })
          }
        />
        <button
          onClick={
            () => setState(prevState => {
              return {
                ...prevState,
                hidePass: !state.hidePass 
              }
            })
          }
        >
          { state.hidePass ? 'Show' : 'Hide' }
        </button>
      </label>

      <label>
        Role
        <select
          onInput={
            e => {
              setState(prevState => {
                return {...prevState, role: e.target.value}
              })
            }
          }
        >
          {
            userRoles &&
            userRoles.map(role => {
              return (
                <option key={role.id} value={role.name}>
                  {role.name}
                </option>
              )
            })
          }
        </select>
      </label>

      <button
          disabled={ !valid() }
          onClick={ 
            () => {
              const data = {...state}
              Reflect.removeProperty(data, 'hidePass')
              submit(data)
                .then(response => {
                  if (response.error) {
                    setError(response.error.toString())
                  } else if (response.data) {
                    refreshUser({
                      id: response.data.user.id,
                      name: response.data.user.fullName,
                    })
                  } else if (response.data.message) {
                    setError(response.data.message)
                  }

                })
            }
          }
        >Sign Up</button>
      {
        error && <p style={{color: 'red'}}>Error occurred: {error}</p> 
      }
    </div>
  )
}

export default SignUpForm
