class Transaction {
  constructor(id, note, amount, transactionDate = new Date()) {
    this.id = id;
    this.note = note;
    this.amount = formatMoney(amount);
    this.transactionDate = formatDate(transactionDate);
    this.validNote$ = ko.observable(true);
    this.validAmount$ = ko.observable(true);
  }

  get isValid() {
    return this.validNote$() && this.validAmount$();
  }
}

class PageIndexViewModel {
  editingTransaction$ = ko.observable(new Transaction(0));
  transactions$ = ko.observableArray([])

  get transaction() {
    return this.editingTransaction$();
  }

  set transaction(value) {
    this.editingTransaction$(value);
  }

  loadTransactions() {
    for (let i = 0; i < 10; i++) {
      this.transactions$.push(new Transaction(i, 'transaction' + i, i * 10000, new Date(2023, i, i)))
    }
  }

  renderTimeAgo() {
    const nodes = document.querySelectorAll('.timeago');
    if (nodes.length) {

      // use render method to render nodes in real time
      timeago.render(nodes);
    }
  }

  validateTransaction() {
    const { note, amount } = this.transaction;

    this.transaction.validNote$(!!note);
    this.transaction.validAmount$(!!amount);

    return this.transaction.isValid;
  }

  handleSave() {

    const valid = this.validateTransaction();
    if (!valid) {
      return;
    }

    const { id, note, transactionDate, amount } = this.transaction;

    const current = new Transaction(id, note, amount, transactionDate);

    if (current.id > 0) {
      let selected = this.transactions$().find(s => s.id === current.id)
      this.transactions$.replace(selected, current)
    } else {
      const maxIdTransaction = this.transactions$()
        .sort((a, b) => a.id - b.id)
        .at(-1);
      current.id = maxIdTransaction.id + 1;
      this.transactions$.unshift(current);
    }

    this.transaction = new Transaction();
    // this.editingTransaction$(new Transaction());
    this.renderTimeAgo();
  }

  handleDelete(value) {
    const yes = confirm('Delete this transaction?')
    if (!yes) {
      return;
    }

    const { id } = value;
    this.transactions$.remove((s) => s.id === id)
  }

}

const vm = new PageIndexViewModel();
vm.loadTransactions();
ko.applyBindings(vm);

// call this method after knockoutjs apply binding
vm.renderTimeAgo();
