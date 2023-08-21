class PageViewModel{
  isFull$ = ko.observable(false)

  toggleMenu(){
    this.isFull$(!this.isFull$())
  }
}

ko.applyBindings(new PageViewModel())
