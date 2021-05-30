export function WriteLocalStorage(token, data) {
  if (!data) {
    throw new Error('No data provided!')
  }

  if (token && token.length > 0) {
    return window.localStorage.setItem(token, JSON.stringify(data))
  }

  throw new Error('Token for local storage not provided!')
}

export function ReadLocalStorage(token) {
  if (token && token.length > 0) {
    const data = window.localStorage.getItem(token)
    
    return JSON.parse(data)
  }
}

export function ClearLocalStorage(token) {
  if (token && token.length > 0) {
    window.localStorage.removeItem(token)
  }
}
