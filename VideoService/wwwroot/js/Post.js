

document.addEventListener("DOMContentLoaded", () => {
    // Используем строгий синтаксис во избежание ошибок и уязвимостей
    "use sctrict";




    var acc = document.getElementsByClassName("show-answers-but");

    var i;

    for (i = 0; i < acc.length; i++) {
        acc[i].addEventListener("click", function () {

            this.classList.toggle("active");

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


function ReadMoreOrLess(id) {
    const moreText = document.getElementById('moreText' + '(' + id + ')');
    const but = document.getElementById('readMoreBut' + '(' + id + ')');
    const dots = document.getElementById('dots' + '(' + id + ')');

    if (moreText.style.display === 'none'
        || moreText.style.display === '') {
        moreText.style.display = 'inline';
        dots.style.display = "none";
        but.textContent = 'Свернуть';
    } else {
        moreText.style.display = 'none';
        dots.style.display = "inline";
        but.textContent = 'Читать далее';
    }
}



//function ShowTimeDifference(postId, mainCommentId) {
//    console.log(222);
//    //var currentdate = new Date();
//    //var datetime = currentdate.getDate() + "/"
//    //    + (currentdate.getMonth() + 1) + "/"
//    //    + currentdate.getFullYear() + " @ "
//    //    + currentdate.getHours() + ":"
//    //    + currentdate.getMinutes() + ":"
//    //    + currentdate.getSeconds();

//    //console.log(777);
    

//    //console.log(datetime);
//    this.innerHTML = $.get("/Post/ShowTimeDifference", { postId: postId, mainCommentId: mainCommentId });
//}

//var localLikesCount;
//var localDislikesCount;

function ChangeReactionsCount(postId, mainCommentId, like) {

    function ChangeReactionsCountOnServer(like, increment) { $.post("/Post/changeReactionsCount", { postId: postId, mainCommentId: mainCommentId, like: like, increment: increment }); };

    var wrapper = document.getElementById(`undercomment-buttons(${mainCommentId})`);

    var comment = document.getElementById(`comment(${mainCommentId})`);

    var likeIcon = wrapper.querySelector("#like");
    var dislikeIcon = wrapper.querySelector("#dislike");

    var likeElem = wrapper.querySelector("#likesCount");
    var dislikeElem = wrapper.querySelector("#dislikesCount");

    var localLikesCount = likeElem.getAttribute("data-likes-count");
    var localDislikesCount = dislikeElem.getAttribute("data-dislikes-count");

    //console.log(localLikesCount);
    //console.log(localDislikesCount);

    var likesCount = likeElem.textContent;
    var dislikesCount = dislikeElem.textContent;


    var isAuthenticated = document.getElementById("IsAuthenticated").innerText;

    // Если пользователь авторизован
    if (isAuthenticated == 'True') {

        // Если тыкаем на лайк
        if (like == "true") {

            if (localLikesCount == 0) {
                $(likeIcon).css('fontVariationSettings', "'FILL' 1");

                likesCount++;
                localLikesCount++;

                ChangeReactionsCountOnServer(true, true);

                // Если мы тыкаем на лайк и при этом уже дизлайкнули запись
                if (localDislikesCount == 1) {
                    $(dislikeIcon).css('fontVariationSettings', "'FILL' 0");
                    localDislikesCount--;
                    dislikesCount--;

                    ChangeReactionsCountOnServer(false, false);
                }
            }
            // Если уже лайкали
            else if (localLikesCount == 1) {
                $(likeIcon).css('fontVariationSettings', "'FILL' 0");
                likesCount--;
                localLikesCount--;

                ChangeReactionsCountOnServer(true, false);

            }
            likeElem.innerHTML = likesCount;
            dislikeElem.innerHTML = dislikesCount;
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

                    ChangeReactionsCountOnServer(true, false);
                }
                dislikesCount++;
                localDislikesCount++;

                ChangeReactionsCountOnServer(false, true);

            }
            // Если уже дизлайкали запись
            else if (localDislikesCount == 1) {
                $(dislikeIcon).css('fontVariationSettings', "'FILL' 0");

                dislikesCount--;
                localDislikesCount--;

                //$.get("/Post/changeReactionsCount", { postId: postId, mainCommentId: mainCommentId, like: false, increment: false }, function (data) {
                //    if (data == "true") console.log("sendData");
                //});

                ChangeReactionsCountOnServer(false, false);

                //$.post("/Post/changeReactionsCount", { postId: postId, mainCommentId: mainCommentId, like: false, increment: false });
            }
            dislikeElem.innerHTML = dislikesCount;
            likeElem.innerHTML = likesCount;
        }

        likeElem.setAttribute("data-likes-count", localLikesCount);
        dislikeElem.setAttribute("data-dislikes-count", localDislikesCount);

    }
    // Если пользователь не авторизован
    else {
        console.log("User dont authenticated");

        //var popup = wrapper.querySelector("#myPopup");

        var popup = comment.querySelector("#myPopup");

        console.log(popup)

        popup.classList.toggle("show");
    }
}

function ShowSendCommentSection(mainCommentId) {

    //var wrapper = document.getElementById(`undercomment-buttons(${mainCommentId})`);
    var sendCommentSection = document.getElementById(`sendCommentWrapper(${mainCommentId})`);

    sendCommentSection.style.display = "block";
}