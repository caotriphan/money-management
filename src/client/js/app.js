class Transaction {
  constructor(number, content, amount, dateValue = new Date()) {
    this.content = content;
    this.amount = amount;
    this.dateValue = dateValue;
  }
}

class PageIndexViewModel {
  transactions = ko.observableArray([1, 2, 3].map(i => ({
    transactionDate: formatDate(new Date(2023, i, i))
  })));

  renderTimeAgo() {
    const nodes = document.querySelectorAll('.timeago');

    // use render method to render nodes in real time
    timeago.render(nodes);
  }

  handleSave() {
    console.log('save')
  }
}
const vm = new PageIndexViewModel();
ko.applyBindings(vm);

// call this method after knockoutjs apply binding
vm.renderTimeAgo();
