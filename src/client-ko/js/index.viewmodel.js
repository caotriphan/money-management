class IndexPageViewModel {
  transactions$ = [1,2,3,4,5,6,7,8,9]
  username$ = ko.observable('Anonymous')

  handleLogout() {
    localStorage.removeItem('token')
    window.location.href = 'login.html'
  }

  authenticateUser() {
    const user = getAuthenticatedUser()
    if (user == null) {
      this.handleLogout();
    } else {
      this.username$(user.username);
    }
  }
}

const vm = new IndexPageViewModel();
vm.authenticateUser();

ko.applyBindings(vm)
