class Transaction{
  constructor(
    id,
    smday = new Date(),
    spendfor,
    amount,
  ) {
    this.id = id;
    this.smday = formatDate(smday);
    this.spendfor = spendfor;
    this.amount = amount;
    this.amountFormated = formatMoney(amount);
  }
}

class MoneyAppViewModel {
  transactions$ = ko.observableArray([{
    smday: '', spendfor: '', amount: ''
  }]);
  transaction$ = ko.observable(new Transaction(0, ''))
  username$ = ko.observable('Anonymous');

  handleLogout(){
    localStorage.removeItem('token')
    window.location.href = 'login.html'
  }

  async onLoad() {
    // reset value
    this.transactions$([]);

    const resp = await fetch(baseUrl + 'transactions?from=2023-01-01&to=9999-01-01', {
      headers: {
        'Authorization': 'Bearer ' + localStorage.getItem('token')
      }
    });
    const json = await resp.json(); // []

    const transactions = json.map(i => ({
      id: i.id,
      smday: i.transactionDate,
      spendfor: i.note,
      amount: i.amount,
    }))

    this.transactions$(transactions);
    vm.renderTimeAgo();

    // for (let i of json) {
    //   const tran = {
    //     id: i.id,
    //     smday: i.transactionDate,
    //     spendfor: i.note,
    //     amount: i.amount,
    //   }

    //   this.transactions$.push(tran);
    // }
  }

  renderTimeAgo(){
    const nodes = document.querySelectorAll('.timeago');

    if(nodes.length){
     timeago.render(nodes)
    }
 }

  async handleSave(){
    // const current = this.transaction$();

    // if (current.id > 0) {
    //   let selected = this.transactions$().find((s) => s.id === current.id)
    //   this.transactions$.replace(selected,current)
    // } else {
    //   const maxIDTransaction = this.transactions$().sort((a,b) => a.id - b.id).at(-1);
    //   current.id = maxIDTransaction.id + 1;
    //   this.transactions$.unshift(current);
    // }

    //this.transaction$(new Transaction());
    const newTran = {
      note: this.transaction$().spendfor,
      amount: this.transaction$().amount,
      transactionDate: new Date()
    }

    const resp = await fetch(baseUrl + 'transactions', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + localStorage.getItem('token')
      },
      body: JSON.stringify(newTran)
    })

    await this.onLoad();
  }

  authenticateUser(){
    const user = getAuthenticatedUser();
    if (user === null){
      this.handleLogout();
    } else {
      this.username$(user.username)
    }
  }
}

const vm = new MoneyAppViewModel();
vm.authenticateUser();
vm.onLoad();

ko.applyBindings(vm);
