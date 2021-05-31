import React from 'react'
import { Link, useLocation } from 'react-router-dom'

function NavigationLink({ to, children }) {
  const current = useLocation()

  if (current.pathname === to) {
    return (
      <label>{children}</label>
    )
  }
  return (
    <Link to={to}>{children}</Link>
  )
}

export default NavigationLink
