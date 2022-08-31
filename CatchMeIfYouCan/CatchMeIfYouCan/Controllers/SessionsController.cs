using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CatchMeIfYouCan.Models;
using Microsoft.AspNet.Identity;

namespace CatchMeIfYouCan.Controllers
{
    public class SessionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sessions
        public ActionResult Index()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            return View(db.Sessions.ToList());
        }

        // GET: Sessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = User.Identity.GetUserId();
            return View(session);
        }

        // GET: Sessions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Distance,Day,Month,Year")] Session session)
        {
            if (ModelState.IsValid)
            {
                session.UserId = User.Identity.GetUserId();
                session.RunnerName = db.Users.Find(User.Identity.GetUserId()).Name + " " + db.Users.Find(User.Identity.GetUserId()).Surname;
                db.Sessions.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(session);
        }

        // GET: Sessions/Edit/5
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session.UserId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,RunnerName,Distance,Day,Month,Year")] Session session)
        {

            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(session);
        }

        // DELETE: Sessions/Delete/5
        public ActionResult Delete(int id)
        {
            Session session = db.Sessions.Find(id);
            if (session.UserId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Account");
            }
            db.Sessions.Remove(session);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // LEADERBOARD: Sessions/Leaderboard
        public ActionResult Leaderboard()
        {
            List<Session> model = db.Sessions.ToList();
            return View(model);
        }

        // PERSONAL RECORDS: Sessions/PersonalRecords 
        public ActionResult PersonalRecords()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.RunnerName = db.Users.Find(User.Identity.GetUserId()).Name + " " + db.Users.Find(User.Identity.GetUserId()).Surname;
            List<Session> model = db.Sessions.ToList();
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
