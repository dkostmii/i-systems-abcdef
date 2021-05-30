import React, { useState, useEffect } from 'react'

import { 
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect
} from 'react-router-dom'

import ListUsersPage from './Pages/ListUsersPage'
import MainPage from './Pages/MainPage'
import SignInPage from './Pages/SignInPage'
import SignUpPage from './Pages/SignUpPage'
import UserProfilePage from './Pages/UserProfilePage'

import { ServiceProvider } from './Services/ServiceProvider'

import { 
  ReadLocalStorage,
  WriteLocalStorage,
  ClearLocalStorage } from './Util/Storage'


function App() {
  const [state, setState] = useState({
    user: ReadLocalStorage('user'),
  })

  const [userService, setUserService] = useState()
  const [userRoleService, setUserRoleService] = useState()

  const initUserService = async () => {
    setUserService((await ServiceProvider('http://localhost:5000/api'))('UserService'))
  }

  const initUserRoleService = async () => {
    setUserRoleService((await ServiceProvider('http://localhost:5000/api'))('UserRoleService'))
  }

  useEffect(() => {
    if (!userService) {
      initUserService()
      initUserRoleService()
    }

    if (state.user) {
      WriteLocalStorage('user', state.user)
    }
  }, [state.user, userService])

  return (
    <Router>
      <div className="app-container">
        <div className="header-container">
          <div className="logo-container">
            <Link to="/">Three Tier</Link>
          </div>

          <Link to="/users">Our users</Link>

          { !state.user ?
            <nav>
              <Link to="/signin">Sign in</Link>
              <Link to="/signup">Sign up</Link>
            </nav>
          :
            <nav>
              <Link to="/profile">{state.user.name}</Link>
              <button 
                onClick={ 
                  () => {
                    setState(prevState => { 
                          return {...prevState, user: undefined } 
                    })
                    ClearLocalStorage('user')
                  } }>
                Log out
              </button>
            </nav> }

        </div>

      </div>


      <Switch>
        <Route exact path="/">
          <MainPage user={ state.user }/>
        </Route>

        <Route path="/signin">
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
          <ListUsersPage user={state.user} userService={userService} />
        </Route>

        <Route path="/profile">
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
