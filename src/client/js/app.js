class Transaction {
  constructor(number, content, amount, dateValue = new Date()) {
    this.content = content;
    this.amount = amount;
    this.dateValue = dateValue;
  }
}

class PageIndexViewModel {
  transactions = ko.observableArray([1, 2, 3]);

  handleSave() {
    console.log('save')
  }
}

ko.applyBindings(new PageIndexViewModel())
