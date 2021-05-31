import React from 'react'

import SignUpForm from '../Components/SignUpForm'

import './SignUpPage.css'

function SignUpPage({ userService, userRoleService, refreshUser }) {
  return (
    <div className="signup-page-container">
      <h1>SignUpPage</h1>
      <SignUpForm 
        refreshUser={refreshUser} 
        submit={ data => userService.signUp(data) }
        getUserRoles={ 
          () => {
            if (userRoleService && userRoleService.getAll) {
              return userRoleService.getAll()
                .then(response => {
                  return response.data
                })
            }
            return new Promise(() => { return { message: 'Cannot fetch user roles' } })
          }
        }
        />

    </div>
  )
}

export default SignUpPage
