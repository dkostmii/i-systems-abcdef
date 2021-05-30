export function validateEmail(email) {
  const re = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/
  return re.test(String(email).toLowerCase())
}

export function validatePhone(phone) {
  const re = /^\+(?:[0-9] ?){6,14}[0-9]$/
  return re.test(phone);
}

export function validatePassword(password) {
  return password.length >= 8
}

