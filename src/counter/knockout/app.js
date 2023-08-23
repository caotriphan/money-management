class PageViewModel {
  count$ = ko.observable(0);
  color = ko.computed(function(){
    if(this.count$() > 0){
      return 'green';
    } else if( this.count$() < 0){
      return 'red';
    } else {
      return '#222';
    }
  },this)

  increase() {
    let curr = this.count$();
    this.count$(curr + 1);
  }

  decrease() {
    let curr = this.count$();
    this.count$(curr - 1);
  }

  reset() {
    this.count$(0);
  }
}

ko.applyBindings(new PageViewModel())
