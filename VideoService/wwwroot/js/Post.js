//function ReadMoreOrLess() {
//    var dots = document.getElementById("dots");
//    var moreText = document.getElementById("more");
//    var btnText = document.getElementById("readMoreBut");

//    if (dots.style.display === "none") {
//        dots.style.display = "inline";
//        btnText.innerHTML = "Read more";
//        moreText.style.display = "none";
//    } else {
//        dots.style.display = "none";
//        btnText.innerHTML = "Read less";
//        moreText.style.display = "inline";
//    }
//}

function ReadMoreOrLess(id) {
    const moreText = document.getElementById('moreText' + '(' + id + ')');
    const but = document.getElementById('readMoreBut' + '(' + id + ')');
    const dots = document.getElementById('dots' + '(' + id + ')')

    if (moreText.style.display === 'none'
        || moreText.style.display === '') {
        moreText.style.display = 'inline';
        dots.style.display = "none";
        but.textContent = 'Read Less';
    } else {
        moreText.style.display = 'none';
        but.textContent = 'Read More';
        dots.style.display = "inline";
    }
} 