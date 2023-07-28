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
  editingTransaction = ko.observable(new Transaction(0));
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

    const current = this.editingTransaction();

    if (current.id > 0) {
      let selected = this.transactions().find(s => s.id === current.id)
      this.transactions.replace(selected, current)
    } else {
      const maxIdTransaction = this.transactions()
        .sort((a, b) => a.id - b.id)
        .at(-1);
      current.id = maxIdTransaction.id + 1;
      this.transactions.unshift(current);
    }
    this.editingTransaction(new Transaction());
  }


}
const vm = new PageIndexViewModel();
vm.loadTransactions();
ko.applyBindings(vm);

// call this method after knockoutjs apply binding
vm.renderTimeAgo();
