export function getUrlBuilder(baseUrl) {
  if (baseUrl.slice(-1) === '/') {
    baseUrl = baseUrl.slice(0, -1)
  }

  return function buildUrl(...chunks) {
    const postfix = chunks.length > 0 ? chunks.join('/') + '/' : ''

    return baseUrl + '/' + postfix
  }
}
