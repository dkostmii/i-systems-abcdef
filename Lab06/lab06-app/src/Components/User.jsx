import React from 'react'

import './User.css'

function User({ fullName, role, email, phone }) {
  return (
    <div className="user-container">
      <h1>{fullName}</h1>
      Role: { role ? role.name : 'Not specified' } <br/>
      Email: { email ? email : 'sign in to view' } <br/>
      Phone: { phone ? phone : 'sign in to view' } <br/>
    </div>
  )
}

export default User
