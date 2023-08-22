const colors = ['green', 'red', 'rgba(133,122,200)', '#f15025'];
const btn = document.getElementById('btn')
const color = document.querySelector('.color')

function renderColor(){
  // get ramdom number between 0-3
  const randomNumber = getRandomNumber();
  document.body.style.backgroundColor = colors[randomNumber];
  color.textContent = colors[randomNumber];
}

btn.addEventListener('click', renderColor())

function getRandomNumber(){
  return Math.floor(Math.random() * colors.length);
}
