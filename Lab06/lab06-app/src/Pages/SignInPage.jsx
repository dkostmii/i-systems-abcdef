import React from 'react'

import SignInForm from '../Components/SignInForm'

function SignInPage({ userService, refreshUser }) {
  return (
    <div>
      <h1>SignInPage</h1>
      <SignInForm refreshUser={refreshUser} submit={ data => userService.signIn(data) } />
    </div>
  )
}

export default SignInPage
