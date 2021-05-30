import React from 'react'

import SignUpForm from '../Components/SignUpForm'

function SignUpPage({ userService, userRoleService, refreshUser }) {
  return (
    <div>
      <h1>SignUpPage</h1>
      <SignUpForm 
        refreshUser={refreshUser} 
        submit={ data => userService.signUp(data) }
        getUserRoles={ 
          () => {
            return userRoleService.getAll()
              .then(response => {
                return response.data
              })
          }
        }
        />

    </div>
  )
}

export default SignUpPage
