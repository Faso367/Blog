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



//function ReadMoreOrLess(id) {
//    const moreText = document.getElementById('moreText' + '(' + id + ')');
//    const but = document.getElementById('readMoreBut' + '(' + id + ')');
//    const dots = document.getElementById('dots' + '(' + id + ')')

//    if (moreText.style.display === 'none'
//        || moreText.style.display === '') {
//        moreText.style.display = 'inline';
//        dots.style.display = "none";
//        but.textContent = 'Свернуть';
//    } else {
//        moreText.style.display = 'none';
//        but.textContent = 'Читать далее';
//        dots.style.display = "inline";
//    }
//} 


function ReadMoreOrLess(id) {
    const moreText = document.getElementById('moreText' + '(' + id + ')');
    const but = document.getElementById('readMoreBut' + '(' + id + ')');
    const dots = document.getElementById('dots' + '(' + id + ')');
    //const MyText = document.getElementById('MyText' + '(' + id + ')');

    if (moreText.style.display === 'none'
        || moreText.style.display === '') {
        //dots.textContent = MyText.textContent;
        moreText.style.display = 'inline';
        dots.style.display = "none";
        but.textContent = 'Свернуть';
    } else {
        moreText.style.display = 'none';
        dots.style.display = "inline";
        //dots.textContent = '...';
        but.textContent = 'Читать далее';
    }
} 
function Increment() {
    //$.get("/Panel/Increment", function (count) {
    //    $("p").html(data);
    // });

   // var name = "23";
    const mainCommentId = 3;
    const like = true;

    //var request;
    var request = new XMLHttpRequest();
    //var request = new ActiveXObject("Microsoft.XMLHTTP");


    if (request != null) {
        //var url = "/Post/Increment";
        var url = "/Post/Test";
        request.open("POST", url, false);

        //var params = "{postId: '" + postId + "'}";

        //var params = "{name: '" + name + "'}";

        var params2 = `{"name": "${name}"},`;
        //`My favorite poem is ${poem} by ${author}.`;

        var params3 = "[{name: 23}]";

        //var params = "{postId: '" + postId + "'}" + "{mainCommentId: '" + mainCommentId + "'}" + "{like: '" + like + "'}";

        //request.setRequestHeader("Content-Type", "application/json");
        request.onreadystatechange = function () {
            if (request.readyState == 4 && request.status == 200) {
                //var response = JSON.parse(request.responseText);
                console.log(777);
                //alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
            }
        };
        request.send(params2);
        console.log("message was send");
    }

    //var test = "@LikeCountIncrement()";
    //    console.log(test);
    
}

function Test(postId, mainCommentId) {
    console.log(postId);
    console.log(mainCommentId);
}

//var incrementLikePossibility = true;
//var decrementLikePossibility = false;

//var incrementDislikePossibility = true;
//var decrementDislikePossibility = false;

var localLikesCount = 0;
var localDislikesCount = 0;

function Increment(postId, mainCommentId, like) {

    //var allWrappers = document.getElementsByClassName("undercomment-buttons");

    var wrapper = document.getElementById(`undercomment-buttons(${mainCommentId})`);

    var likeIcon = wrapper.querySelector("#like");
    var dislikeIcon = wrapper.querySelector("#dislike");

    var likeElem = wrapper.querySelector("#likesCount");
    var dislikeElem = wrapper.querySelector("#dislikesCount");

    var likesCount = likeElem.textContent;
    var dislikesCount = dislikeElem.textContent;

   // console.log(dislikesCount);
    // Если тыкаем на лайк
    if (like == "true") {

        

        //allWrappers.style.font - variation - settings = "'FILL' 0";

        if (localLikesCount == 0) {
            $(likeIcon).css('fontVariationSettings', "'FILL' 1");

            likesCount++;      
            localLikesCount++;

            // Если мы тыкаем на лайк и при этом уже дизлайкнули запись
            if (localDislikesCount == 1) {
                $(dislikeIcon).css('fontVariationSettings', "'FILL' 0");
                localDislikesCount--;
                dislikesCount--;
            }

            $.get("/Post/Increment", { postId: postId, mainCommentId: mainCommentId, like: like }, function (data) {
                if (data == "true") console.log("sendData");
            });

            //incrementLikePossibility = false;
            // Теперь можно как снять лайк, так и поставить dislike
            //decrementLikePossibility = true;
        }
        // Если уже лайкали
        else if (localLikesCount == 1) {
            $(likeIcon).css('fontVariationSettings', "'FILL' 0");
            likesCount--;
            localLikesCount--;

            $.get("/Post/Increment", { postId: postId, mainCommentId: mainCommentId, like: like }, function (data) {
                if (data == "true") console.log("sendData");
            });
            //incrementLikePossibility = true;
            // Теперь можно как снять лайк, так и поставить dislike
            //decrementLikePossibility = false;
        }
        likeElem.innerHTML = likesCount;
        dislikeElem.innerHTML = dislikesCount;
        //incrementDislikePossibility = true; ???
    }

    // Если тыкаем на дизлайк
    else if (like == "false") {
        if (localDislikesCount == 0) {

            $(dislikeIcon).css('fontVariationSettings', "'FILL' 1");

            // Если мы тыкаем на дизлайк и при этом уже лайкнули запись
            if (localLikesCount == 1) {

                $(likeIcon).css('fontVariationSettings', "'FILL' 0");

                localLikesCount--;
                likesCount--;
            }
            dislikesCount++;
            localDislikesCount++;

            $.get("/Post/Increment", { postId: postId, mainCommentId: mainCommentId, like: like }, function (data) {
                if (data == "true") console.log("sendData");
            });
        }
        // Если уже дизлайкали запись
        else if (localDislikesCount == 1) {
            $(dislikeIcon).css('fontVariationSettings', "'FILL' 0");

            dislikesCount--;
            localDislikesCount--;

            $.get("/Post/Increment", { postId: postId, mainCommentId: mainCommentId, like: like }, function (data) {
                if (data == "true") console.log("sendData");
            });
        }
        dislikeElem.innerHTML = dislikesCount;
        likeElem.innerHTML = likesCount;
    }



    //updateDisplay(like);

    //function updateDisplay(like) {
    //    if (like == "true") likeElem.innerHTML = likesCount;
    //    else if (like == "false") dislikeElem.innerHTML = dislikesCount;
    //};

    //console.log(postId);
    //console.log(mainCommentId);
    //console.log(like);

    //$.get("/Post/Test", `name=${name, mainCommentId}`, function (data) {
}

/*var answerBut = document.getElementById("answer");*/

function ShowSendCommentSection() {

    const section = document.getElementById("sendCommentWrapper");
    section.style.display = "block";
    /*section.style.display = "block";*/
    //if (section.style.maxHeight) {
    //    section.style.maxHeight = null;
    //} else {
    //    section.style.maxHeight = panel.scrollHeight + "px";
    //}

    //if (section.style.display === "block") {
    //    section.style.display = "none";
    //} else {
    //    section.style.display = "block";
    //}
}


// Функция для получения элемента-обертки по Id и получения вложенного в него элемента по Id
function GetElementInsideContainer(containerID, childID) {
    console.log(containerID);
    var elm = document.getElementById(childID);
    var parent = elm ? elm.parentNode : {};
    return (parent.id && parent.id === containerID) ? elm : {};
}

document.addEventListener("DOMContentLoaded", () => {
    // Используем строгий синтаксис во избежание ошибок и уязвимостей
    "use sctrict";


    //likeBut = document.getElementById("like").addEventListener("click", function () {
    //    Increment(postId, mainCommentId, like);
    //});


    //Increment();
    //GetMessage();

    var acc = document.getElementsByClassName("show-answers-but");

    var i;

    for (i = 0; i < acc.length; i++) {
        acc[i].addEventListener("click", function () {
            /* Toggle between adding and removing the "active" class,
            to highlight the button that controls the panel */
            this.classList.toggle("active");

            /* Toggle between hiding and showing the active panel */
            var panel = this.nextElementSibling;
            if (panel.style.maxHeight) {
                panel.style.maxHeight = null;
            } else {
                panel.style.maxHeight = panel.scrollHeight + "px";
            }

            if (panel.style.display === "block") {
                panel.style.display = "none";
            } else {
                panel.style.display = "block";
            }
        });
    }

});
//function ShowSubcomments(id) {
//    //const but = document.getElementById('showSubcommentsBut' + '(' + id + ')');
//    const subcommentText = document.getElementById('subcocommentText' + '(' + id + ')');

//    subcommentText.style.display = 'block';
//}