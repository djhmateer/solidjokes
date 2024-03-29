﻿using System;
using SolidJokes.Core.Models;
using SolidJokes.Core.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Web.Mvc;

namespace SolidJokes.Web.Controllers
{
    public class HomeController : Controller, IHomeController
    {
        private readonly IJokeViewer viewer;
        private readonly IJokeVoter voter;

        public HomeController(IJokeViewer viewer, IJokeVoter voter)
        {
            this.viewer = viewer;
            this.voter = voter;
        }

        public ActionResult Index(string sortOrder, string message)
        {
            if (sortOrder == null) sortOrder = "ratingDescending";
            List<Joke> jokes = null;
            switch (sortOrder)
            {
                case "ratingDescending":
                    jokes = viewer.ShowAllJokesHighestRatingFirst();
                    break;
                case "dateCreatedDescending":
                    jokes = viewer.ShowAllJokesByDateCreatedDescending();
                    break;
            }
            DisplayMessageToUserIfRequired(message);

            // build done on west coast USA (Appveyor)
            // Server is in Ireland so want it to display UK Time.
            //ViewBag.DateOfCompile = Assembly.GetExecutingAssembly().GetLinkerTime().ToLocalTime();
            return View(jokes);
        }

        private void DisplayMessageToUserIfRequired(string message)
        {
            // Is there a message, and what type to display to user ie green or red
            if (message != null)
            {
                if (message.Contains("Thank you for voting!") && message != "")
                {
                    ViewBag.GreenMessage = true;
                }
                else
                {
                    ViewBag.RedMessage = true;
                }
                ViewBag.Message = message;
            }
        }

        public ActionResult Vote(int? storyID, string sortOrder = "ratingDescending")
        {
            JokeVoterResult result = voter.AddVote(storyID);
            // Display success or fail message of voting
            return RedirectToAction("Index", "Home", new { sortOrder = sortOrder, message = result.Message });
        }

        public ActionResult About()
        {
            // build done on west coast USA (Appveyor)
            // Server is in Ireland so want it to display UK Time.
            //var britishZone = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            //var linkerTime = Assembly.GetExecutingAssembly().GetLinkerTime();
            ////var newDate = TimeZoneInfo.ConvertTime(linkerTime, TimeZoneInfo.Local, britishZone);

            //ViewBag.DateOfCompile = newDate
            //    .ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            return View();
        }
    }
}