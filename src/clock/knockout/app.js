class PageViewModel {
  timeNow$ = ko.observable('10:36:01 AM');

  renderClock(){
    setInterval(() => {
      const now = new Date();
      const hr = now.getHours();
      const displayHour = hr > 12 ? hr - 12 : hr;
      const aa = hr >= 12 ? 'PM' : 'AM';
      this.timeNow$(`${displayHour} : ${now.getMinutes()} : ${now.getSeconds()} ${aa}`)
    }, 1000)
  }
}

const vm = new PageViewModel();
vm.renderClock();

ko.applyBindings(vm)
