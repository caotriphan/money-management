function renderClock(){
const clock = document.querySelector('.clock');

const formatTime = (n) => n >= 10 ? n : '0' + n;

setInterval(() => {
  const now = new Date();
  const hr = now.getHours();
  const displayHour = hr > 12 ? hr - 12 : hr;
  const aa = hr >= 12 ? 'PM' : 'AM';
  clock.innerText = `${displayHour} : ${formatTime(now.getMinutes())} : ${formatTime(now.getSeconds())} ${aa}`
}, 1000)
}

renderClock();
