class Transaction {
  constructor(id, note, amount, transactionDate = new Date()) {
    this.id = id;
    this.note = note;
    this.amount = amount;
    this.transactionDate = formatDate(transactionDate);
  }
}

class PageIndexViewModel {
  // transactions = ko.observableArray([1, 2, 3].map(i => ({
  //   transactionDate: formatDate(new Date(2023, i, i))
  // })));

  transactions = ko.observableArray([])

  loadTransactions() {
    for (let i = 0; i < 10; i++) {
      this.transactions.push(new Transaction(i, 'transaction' + i, i * 10000, new Date(2023, i, i)))
    }
  }

  renderTimeAgo() {
    const nodes = document.querySelectorAll('.timeago');
    if (nodes.length) {

      // use render method to render nodes in real time
      timeago.render(nodes);
    }
  }

  handleSave() {
    console.log('save')
  }
}
const vm = new PageIndexViewModel();
vm.loadTransactions();
ko.applyBindings(vm);

// call this method after knockoutjs apply binding
vm.renderTimeAgo();
