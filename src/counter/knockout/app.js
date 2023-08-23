class PageViewModel{
count$ = ko.observable(0);

increase(){
  let curr = this.count$();
  this.count$(curr+1);
}

decrease(){
  let curr = this.count$();
  this.count$(curr-1);
}

reset(){
this.count$(0);
}
}

ko.applyBindings(new PageViewModel())
