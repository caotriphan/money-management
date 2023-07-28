class Mngt {
  constructor(number, content, amount, dateValue = new Date()) {
    this.content = content;
    this.amount = amount;
    this.dateValue = dateValue;
  }
}

class PageMoMaViewModel {
  moneyMngts = ko.observableArray([]);
  moneymngt = ko.observable(new Mngt())

  handleSave() {
    const current = this.moneymngt();

    if (current.number > 0) {

    }
  }
}

ko.applyBindings(new PageMoMaViewModel())
