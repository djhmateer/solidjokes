﻿using SolidJokes.Core.Models;
using SolidJokes.Core.Services;
using System.Net;
using System.Web.Mvc;

namespace SolidJokes.Web.Controllers {
    public interface IJokeController {
    }

    [Authorize(Users = "davemateer@gmail.com")]
    public class JokeController : Controller, IJokeController {
        private readonly IJokeViewer viewer;
        private readonly IJokeCreator creator;

        public JokeController(IJokeViewer viewer, IJokeCreator creator) {
            this.viewer = viewer;
            this.creator = creator;
        }

        public ActionResult Index() {
            return View(viewer.ShowAllJokesByDateCreatedDescending());
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JokeApplication app) {
            if (ModelState.IsValid) {
                JokeCreatorResult result = creator.CreateOrEditJoke(app);
                if (result.JokeApplication.IsValid()) {
                    return RedirectToAction("Index");
                }
            }
            return View(app);
        }

        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var joke = creator.GetJokeByID((int)id);
            if (joke == null) {
                return HttpNotFound();
            }

            var app = new JokeApplication {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JokeApplication app) {
            if (ModelState.IsValid) {
                JokeCreatorResult result = creator.CreateOrEditJoke(app);
                if (result.JokeApplication.IsValid()) {
                    return RedirectToAction("Index");
                }
            }
            return View(app);
        }

        // GET: /Story/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Joke joke = creator.GetJokeByID((int)id);
            if (joke == null) {
                return HttpNotFound();
            }

            return View(joke);
        }

        // POST: /Story/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            creator.DeleteJoke(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing) {
        //    if (disposing) {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
