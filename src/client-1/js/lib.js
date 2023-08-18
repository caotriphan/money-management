const getAuthenticatedUser = () => {
  const token = localStorage.getItem('token');
  if (!token){
    return null;
  }

  const jwt = jwt_decode(token);
  const expired = isExpired(jwt.exp);

  if (expired) {
    return null;
  }

  const username = jwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
  return { username };
}

const isExpired = (exp) => {
  return Date.now() >= exp * 1000;
}
