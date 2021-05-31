import React from 'react'

import SignInForm from '../Components/SignInForm'

import './SignInPage.css'

function SignInPage({ userService, refreshUser }) {
  return (
    <div className="signin-page-container">
      <h1>SignInPage</h1>
      <SignInForm refreshUser={refreshUser} submit={ data => userService.signIn(data) } />
    </div>
  )
}

export default SignInPage
