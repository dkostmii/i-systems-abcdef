import React, { useState, useEffect } from 'react'

import { 
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect,
} from 'react-router-dom'

import NavigationLink from './Components/NavigationLink'

import ListUsersPage from './Pages/ListUsersPage'
import MainPage from './Pages/MainPage'
import SignInPage from './Pages/SignInPage'
import SignUpPage from './Pages/SignUpPage'
import UserProfilePage from './Pages/UserProfilePage'
import ErrorPage from './Pages/ErrorPage'

import { ServiceProvider } from './Services/ServiceProvider'

import { 
  ReadLocalStorage,
  WriteLocalStorage,
  ClearLocalStorage } from './Util/Storage'

import './App.css'


function App() {
  const [state, setState] = useState({
    user: ReadLocalStorage('user'),
  })

  const [userService, setUserService] = useState()
  const [userRoleService, setUserRoleService] = useState()

  const [error, setError] = useState(null)

  useEffect(() => {
    const initUserService = async () => {
        if (!userService) {
          setUserService((await ServiceProvider('http://localhost:5000/api'))('UserService'))
        }
      }

    const initUserRoleService = async () => {
      if (!userRoleService) {
        setUserRoleService((await ServiceProvider('http://localhost:5000/api'))('UserRoleService'))
      }
    }

    fetch('http://localhost:5000/status')
      .then(response => {
        if (response.ok) {
          initUserService()
          initUserRoleService()
        }
      })
      .catch(() => {
        setError({ name:'Fetch Error', message: 'Cannot connect to server' })
      })



    if (state.user) {
      WriteLocalStorage('user', state.user)
    }
  }, [state.user, userService, userRoleService])

  return (
    <Router>
      <div className="app-container">

        <div className="header-container">

          <div className="logo-container">
            <Link to="/">Three Tier</Link>
          </div>

          <nav>
          <NavigationLink to="/users">Our users</NavigationLink>

          { !state.user ?
            <>
              <NavigationLink to="/signin">Sign in</NavigationLink>
              <NavigationLink to="/signup">Sign up</NavigationLink>
            </>
          :
            <>
              <NavigationLink to="/profile">{state.user.name}</NavigationLink>
              <button className="button-link"
                onClick={ 
                  () => {
                    setState(prevState => { 
                          return {...prevState, user: undefined } 
                    })
                    ClearLocalStorage('user')
                  } }>
                Log out
              </button>
            </> }
            </nav>

        </div>

      </div>


      <Switch>
        <Route exact path="/">
          <MainPage user={ state.user }/>
        </Route>

        <Route path="/error">
          { error ? <ErrorPage error={error} /> : <Redirect to="/" /> }
        </Route>

        <Route path="/signin">
          {
            error && <Redirect to="/error" />
          }

          {
            !state.user ?
              <SignInPage 
                refreshUser={ user => setState({ user }) }
                userService={userService}
              />
            :
              <Redirect to="/profile" />
          }
        </Route>

        <Route path="/signup">
          { error && <Redirect to="/error" />  }
          {
            !state.user ?
              <SignUpPage
                refreshUser={ user => setState({ user }) }
                userService={userService}
                userRoleService={userRoleService}
              />
            :
              <Redirect to="/profile" />
          }
        </Route>

        <Route path="/users">
          {
            error && <Redirect to="/error" />
          }
          <ListUsersPage user={state.user} userService={userService} />
        </Route>

        <Route path="/profile">
          {
            error && <Redirect to="/error" />
          }
          { 
            state.user ?
              <UserProfilePage user={state.user} userService={userService} />
            :
              <Redirect to="/signin" /> 
          }
        </Route>
      </Switch>
    </Router>
  )
}

export default App
