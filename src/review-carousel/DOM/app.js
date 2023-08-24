const img = document.getElementById('person-img');
const author = document.getElementById('author');
const job = document.getElementById('job');
const info = document.getElementById('info');

const prevBtn = document.querySelector('.prev-btn');
const nextBtn = document.querySelector('.next-btn');
const randomBtn = document.querySelector('.random-btn');

let currentItem = 0;

// load initial item
window.addEventListener('DOMContentLoaded', function(){
  showPerson();
})

// show person based on item
function showPerson(){
  const item = reviews[currentItem];
  img.src = item.avatar;
  author.textContent = item.full_name;
  job.textContent = item.job_title;
  info.textContent = item.slogan;
}

//show next person

nextBtn.addEventListener('click', function(){
  currentItem++;
  if(currentItem > reviews.length - 1){
    currentItem = 0;
  }
  showPerson(currentItem);
})

prevBtn.addEventListener('click', function(){
  currentItem--;
  if(currentItem <0){
    currentItem = reviews.length - 1;
  }
  showPerson(currentItem);
})

// show random person

randomBtn.addEventListener('click', function(){
  currentItem = Math.floor(Math.random() * reviews.length);
  console.log(currentItem)
  showPerson();
})
