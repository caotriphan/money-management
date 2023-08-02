const getTransactions = async () => {
  const response = await fetch('http://localhost:5094/transactions', {
    headers: {
      'authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVHJpIFBoYW4iLCJuYmYiOjE2OTA4ODMxNDEsImV4cCI6MTY5MDk1MTU0MSwiaXNzIjoiTW9uZXlBcGkiLCJhdWQiOiJNb25leUFwaSJ9.ROoXBCnOWW9VMjgl4C17u-VT_5esLdnTyWG6Dub0E-E'
    }
  });
  const json = await response.json();

  return json;
}

const saveTransaction = async (note, amount, date) => {
  const response = await fetch('http://localhost:5094/transactions', {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiVHJpIFBoYW4iLCJuYmYiOjE2OTA4ODMxNDEsImV4cCI6MTY5MDk1MTU0MSwiaXNzIjoiTW9uZXlBcGkiLCJhdWQiOiJNb25leUFwaSJ9.ROoXBCnOWW9VMjgl4C17u-VT_5esLdnTyWG6Dub0E-E'
    },
    body: JSON.stringify({ note, amount, transactionDate: date })
  })

  return await response.json();
}
