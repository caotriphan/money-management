class LoginViewModel {
  username$ = ko.observable('')
  password$ = ko.observable('')
  hasError$ = ko.observable(false)

  async handleLogin() {

    const resp = await fetch(baseUrl + 'users/login', {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({username: this.username$(), password: this.password$()})
    })

    const json = await resp.json();

    if (typeof(json) === 'object' && !!json.success) {
      const jwt = json.data;

      localStorage.setItem('token', jwt);

      window.location.href = 'index.html';
    } else {
      this.hasError$(true);
    }
  }
}

ko.applyBindings(new LoginViewModel())
