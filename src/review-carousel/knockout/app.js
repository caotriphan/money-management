class Person{
  constructor(id, full_name, job_title, avatar, slogan){
    this.id = id;
    this.full_name = full_name;
    this.job_title = job_title;
    this.avatar = avatar;
    this.slogan = slogan;
  }
}

class PageViewModel {
  people$ = ko.observableArray([]);
  person$ = ko.observable(new Person());
  currentItem$ = ko.observable(0);

  async onLoad(){
    const resp = await fetch('data.json');
    const json = await resp.json();

    if(!json || !json.length){
      return;
    }

    for(let i of json){
      this.people$.push(i);
    }
  }

  nextPerson(){

  }

  prevPerson(){

  }

  randomPerson(){

  }
}

const vm = new PageViewModel();
vm.onLoad();

ko.applyBindings(vm);
