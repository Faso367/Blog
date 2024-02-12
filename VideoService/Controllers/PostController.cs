﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using VideoService.Data.FileManager;
using VideoService.Data.Repository;
using VideoService.Models;
using VideoService.Models.Comments;

namespace VideoService.Controllers
{
    public class PostController : Controller
    {
        private IRepository _repo;
        //private IFileManager _fileManager;

        public PostController(IRepository repo, IFileManager fileManager)
        {
            // Создаем экземпляр интерфейса, чтобы пользоваться его методами
            _repo = repo;
            //_fileManager = fileManager;
        }


        // Если это like, то true. Если dislike, то false
        //[HttpPost]
        //public async Task<int> Increment(int postId, int mainCommentId, bool like)
        //[HttpGet]
        //public ActionResult changeReactionsCount(int postId, int mainCommentId, bool like, bool increment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Получаем имя текущего пользователя
        //        string? currentUserName = User.Identity?.Name;

        //        //Console.WriteLine($"{postId} {mainCommentId} {like}");

        //        var post = _repo.GetPost(postId);

        //        if (post != null && !currentUserName.IsNullOrEmpty())
        //        {

        //            MainComment? mainComment = post.MainComments.Find(x => x.Id == mainCommentId);

        //            if (mainComment != null)
        //            {
        //                if (increment)
        //                {
        //                    if (like) mainComment.LikesCount++;
        //                    else mainComment.DislikesCount++;
        //                }
        //                else
        //                {
        //                    if (like) mainComment.LikesCount--;
        //                    else mainComment.DislikesCount--;
        //                }

        //                _repo.UpdatePost(post);

        //                if (_repo.SaveChanges())
        //                    return new JsonResult("true");
        //            }
        //        }
        //    }
        //    return new JsonResult("false");
        //}

        [HttpPost]
        public void changeReactionsCount(int postId, int mainCommentId, bool like, bool increment)
        {

            /*Надо сделать проверку на то, делался ли лайк до этого */

            if (ModelState.IsValid)
            {
                //Получаем имя текущего пользователя
                string? currentUserName = User.Identity?.Name;

                //Console.WriteLine($"{postId} {mainCommentId} {like}");

                var post = _repo.GetPost(postId);

                if (post != null && !currentUserName.IsNullOrEmpty())
                {

                    MainComment? mainComment = post.MainComments.Find(x => x.Id == mainCommentId);

                    if (mainComment != null)
                    {
                        

                        if (increment)
                        {
                            if (like) mainComment.LikesCount++;
                            else mainComment.DislikesCount++;
                        }
                        else
                        {
                            if (like) mainComment.LikesCount--;
                            else mainComment.DislikesCount--;
                        }

                        _repo.UpdatePost(post);

                        _repo.SaveChanges();
                        //    return new JsonResult("true");
                    }
                }
            }
            
        }

        [HttpGet]
        public ActionResult ShowTimeDifference(int postId, int mainCommentId)
        {
            Console.WriteLine(postId);

            if (ModelState.IsValid)
            {
                //Получаем имя текущего пользователя
                //string? currentUserName = User.Identity?.Name;

                var post = _repo.GetPost(postId);

                //if (post != null && !currentUserName.IsNullOrEmpty())
                if (post != null)
                {

                    MainComment? mainComment = post.MainComments.Find(x => x.Id == mainCommentId);

                    var s = FindTimeDifferenceAndCovertToWords(mainComment.Created);

                    Console.WriteLine("Вывод: " + s);

                    if (mainComment != null)
                        return new JsonResult(s);
                }
            }
            return new JsonResult("error");
        }

        private static string FindTimeDifferenceAndCovertToWords(DateTime postCreatedTime)
        {
            string result = "";

            TimeSpan timeDifference = DateTime.Now - postCreatedTime;

            //Func<double, int> DoubleToInt = Dtime => Convert.ToInt32(Math.Round(Dtime)) - 1;

            int totalDays = Convert.ToInt32(Math.Round(timeDifference.TotalDays)) - 1;
            int totalHours = Convert.ToInt32(Math.Round(timeDifference.TotalHours)) - 1;
            int totalMinutes = Convert.ToInt32(Math.Round(timeDifference.TotalMinutes)) - 1;

            if (totalDays > 0)
            {
                if (totalDays > 364)
                    result = CreateCorrectNumeral(totalDays / 365, "year");
                else if (totalDays > 30) // НЕ ВСЕГДА ПРАВИЛЬНО
                    result = CreateCorrectNumeral(totalDays / 30, "month");
                else
                    result = CreateCorrectNumeral(totalDays, "day");

            }
            else if (totalHours > 0)
                result = CreateCorrectNumeral(totalHours, "hour");

            else if (totalMinutes > 0)
                result = CreateCorrectNumeral(totalMinutes, "minute");

            else
                result = "меньше минуты назад";

            return result;

            string CreateCorrectNumeral(int time, string type)
            {

                var timeUnits_defaultValues = new Dictionary<string, string[]> {
                    {"minute", new string[] { "минуту", "минуты", "минут" }},
                    {"hour", new string[] { "час", "часа", "часов" }},
                    {"day", new string[] { "день", "дня", "дней" }},
                    {"month", new string[] { "месяц", "месяца", "месяцев" }},
                    {"year", new string[] { "год", "года", "лет" }}
                };

                if (timeUnits_defaultValues.ContainsKey(type))
                {
                    string strTime = time.ToString();

                    foreach (var unit in timeUnits_defaultValues.Keys)
                        if (unit == type)
                            switch (strTime[^1] == '1' && strTime != "11" ? 1 :
                                    strTime[^1] == '2' || strTime[^1] == '3' || strTime[^1] == '4' ? 2 : 3)
                            {
                                case 1: return $"{strTime} {timeUnits_defaultValues[unit][0]} назад";
                                case 2: return $"{strTime} {timeUnits_defaultValues[unit][1]} назад";
                                case 3: return $"{strTime} {timeUnits_defaultValues[unit][2]} назад";
                            };
                }
                return "";
            }
        }

    }

    public static class Extensions {

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }
    }

}
    /*
    [HttpGet]
    public ActionResult Decrement(int postId, int mainCommentId, bool like)
    {
        //Получаем имя текущего пользователя
        string? currentUserName = User.Identity?.Name;

        Console.WriteLine($"{postId} {mainCommentId} {like}");

        var post = _repo.GetPost(postId);

        if (post != null)
        {

            MainComment? mainComment = post.MainComments.Find(x => x.Id == mainCommentId);

            if (mainComment != null && !currentUserName.IsNullOrEmpty())
            {
                if (A(mainComment, currentUserName, like)) {... };

                //else
                //    mainComment.DislikesCount++;

                _repo.UpdatePost(post);
                //await _repo.SaveChangesAsync();

                if (_repo.SaveChanges())
                    return new JsonResult("true");
                //if (_repo.SaveChangesAsync()) Console.WriteLine(777);
            }
        }
        return new JsonResult("false");
    }*/

    //private bool MatchingWithDbIsSuccess(MainComment mainComment, string userName, bool like)

    //{
    //    // Если нажали на лайк
    //    if (like == true) {

    //        // Если лайк уже был нажат
    //        if (WasLiked(mainComment, userName))
    //            mainComment.LikesCount--;
    //        else
    //            mainComment.LikesCount++;

    //        // Если до этого был нажат дизлайк
    //        if(WasDisliked(mainComment, userName))
    //            mainComment.DislikesCount--;
    //    }

    //    // Если нажали на дизлайк
    //    if (like == false)
    //    {
    //        // Если дизлайк уже был нажат
    //        if (WasDisliked(mainComment, userName))
    //            mainComment.DislikesCount--;
    //        else
    //            mainComment.DislikesCount++;

    //        // Если до этого был нажат лайк
    //        if (WasLiked(mainComment, userName))
    //            mainComment.LikesCount--;
    //    }

    //}


    //private static bool WasLiked(MainComment mainComment, string userName) =>
    //    WasAppreciated(mainComment, userName, "like")[0];

    //private static bool WasDisliked(MainComment mainComment, string userName) =>
    //    WasAppreciated(mainComment, userName, "like")[1];

    ///// <summary>
    ///// Определяет был ли лайкнут и дизлайкнут пост этим пользователем
    ///// </summary>
    ///// <param name="mainComment"></param>
    ///// <param name="userName"></param>
    ///// <param name="reactionName"></param>
    ///// <returns></returns>
    //private static bool[] WasAppreciated(MainComment mainComment, string userName, string reactionName)
    //{
    //    // Под первым элементом массива хранится ответ на вопрос: этот пост был лайкнут?
    //    // Под вторым элементом массива хранится ответ на вопрос: этот пост был дизлайкнут?
    //    var result = new bool[2] { false, false };

    //    foreach (KeyValuePair<string, Dictionary<string, bool>> dict in mainComment.AuthorsAndLikeExistence)
    //        // Если пользователь реагировал на пост
    //        if (dict.Key == userName)
    //            foreach (KeyValuePair<string, bool> pair in dict.Value)
    //            {
    //                if (pair.Key == reactionName)
    //                    result[0] = (pair.Value == true) ? true : false;

    //                else if (pair.Key == reactionName)
    //                    result[1] = (pair.Value == true) ? true : false;
    //            }
    //    return result;
    //}


    /*        string NeedChange(double time, string type)
            {
                //List<string> timeUnits = new List<string> { "minute", "hour", " day", "year" };

                var timeUnits_defaultValues = new Dictionary<string, string[]> {
                    {"minute", new string[] {"минуту", "минуты", "минут" } },
                    {"hour", new string[] {"час", "часа", "часов" } },
                    {"day", new string[] {"день", "дня", "дней" }},
                    {"year", new string[] {"год", "года", "лет" }}
                };

                //string[] unitsOfTime = [""];

                if (timeUnits_defaultValues.ContainsKey(type)) {

                    //foreach(var pai)

                    string strTime = time.ToString();

                    foreach (var unit in timeUnits_defaultValues.Keys)
                        switch ( strTime[^1] == '1' && strTime != "11" ? 1 :
                                 strTime[^1] == '2' || strTime[^1] == '3' || strTime[^1] == '4' ? 2 : 3 )
                        {
                                 //strTime == "11" ? 3 : 4 ) {

                            case 1: return timeUnits_defaultValues[unit][0];
                            case 2: return timeUnits_defaultValues[unit][1];
                            case 3: return timeUnits_defaultValues[unit][2];
                            //case 4: return timeUnits_defaultValues[unit][2];
                        }


                }
            }*/

