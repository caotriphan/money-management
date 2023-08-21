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

const formatDateString = (s) => {
  const date = new Date(s);
  return dayjs(date).format('YYYY-MM-DD HH-mm-ss')
}

const formatDate = (date, format = 'YYYY-MM-DD HH-mm-ss') => {
  return dayjs(date).format(format)
}

const formatMoney = (value) => {
  return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value)
}
