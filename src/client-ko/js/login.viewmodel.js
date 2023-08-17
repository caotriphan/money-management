class LoginPageViewModel {
  username$ = ko.observable('')
  password$ = ko.observable('')
  hasError$ = ko.observable(false)

  async handleLogin() {
    // todo: validate form

    const req = await fetch('http://localhost:5094/users/login', {
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
      },
      method: 'POST',
      body: JSON.stringify({ username: this.username$(), password: this.password$() })
    })

    const resp = await req.json();
    if (typeof resp === 'object' && !!resp.success) {
      // success -> persist jwt to local storage to use it later
      const jwt = resp.data;
      localStorage.setItem('token', jwt);
      // redirect to index
      window.location.href = 'index.html';
    } else {
      this.hasError$(true);
    }
  }
}

ko.applyBindings(new LoginPageViewModel());
