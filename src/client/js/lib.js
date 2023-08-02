const formatDateString = (s) => {
  const date = new Date(s); // Wed Feb 01 2023 00:00:00 GMT+0700 (Indochina Time)
  // 2016-06-30 09:20:00
  return dayjs(date).format('YYYY-MM-DD HH:mm:ss');
}

const formatDate = (date, format = 'YYYY-MM-DD HH:mm:ss') => {
  // const date = new Date(s); // Wed Feb 01 2023 00:00:00 GMT+0700 (Indochina Time)
  // 2016-06-30 09:20:00
  return dayjs(date).format(format);
}

const formatMoney = (value) => {
  return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value)
}
