using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolidJokes.Core.Models;
using SolidJokes.Core.Services;

namespace SolidJokes.Web.Controllers {
    public interface IJokeController {
    }

    //[Authorize(Users = "Dave2")]
    public class JokeController : Controller, IJokeController {
        private readonly IJokeViewer viewer;

        public JokeController(IJokeViewer viewer) {
            this.viewer = viewer;
        }

        public ActionResult Index() {
            return View(viewer.ShowAllJokesByDateCreatedDescending());
        }

       

        //public ActionResult Create() {
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(StoryApplication app) {
        //    if (ModelState.IsValid) {
        //        // Call our StoryCreator service
        //        var sc = new StoryCreator();
        //        StoryCreatorResult result = sc.CreateOrEditStory(app);
        //        if (result.StoryApplication.IsValid()) {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View(app);
        //}

        //public ActionResult Details(int? id) {
        //    if (id == null) {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var joke = viewer.GetJokeByID((int)id);
        //    if (joke == null) {
        //        return HttpNotFound();
        //    }
        //    return View(joke);
        //}

        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var joke = viewer.GetJokeByID((int)id);
            if (joke == null) {
                return HttpNotFound();
            }

            var app = new JokeApplication
            {
                JokeID = joke.ID,
                Title = joke.Title,
                Content = joke.Content,
                Rating = joke.Rating,
                JokeType = joke.JokeType,
                ImageURL = joke.ImageURL,
                VideoURL = joke.VideoURL,
                CreatedAt = joke.CreatedAt
            };

            return View(app);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(StoryApplication app) {
        //    if (ModelState.IsValid) {
        //        // Call our StoryCreator service
        //        var sc = new StoryCreator();
        //        StoryCreatorResult result = sc.CreateOrEditStory(app);
        //        if (result.StoryApplication.IsValid()) {
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    return View(app);
        //}

        //// GET: /Story/Delete/5
        //public ActionResult Delete(int? id) {
        //    if (id == null) {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Story story = db.Stories.Find(id);
        //    if (story == null) {
        //        return HttpNotFound();
        //    }

        //    return View(story);
        //}

        //// POST: /Story/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id) {
        //    Story story = db.Stories.Find(id);
        //    db.Stories.Remove(story);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing) {
        //    if (disposing) {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }


}
