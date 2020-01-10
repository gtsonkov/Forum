using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Forum.Data;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;

namespace Forum.Controllers
{
    public class TopicController : Controller
    {
        private readonly ForumDbContext context;

        public TopicController(ForumDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Topic topic = context.Topics
                .Include(t => t.Autor)
                .Where(t => t.Id == id)
                .SingleOrDefault();

            if (topic == null)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View(topic);
        }

        /// <summary>
        /// Creating
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        //Add Postmethod Create Topic
        [HttpPost]
        [Authorize]
        public IActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                topic.CreateDate = DateTime.Now;
                topic.LastUpdatedDate = DateTime.Now;

                string autorId = context.Users
                    .Where(u => u.UserName == User.Identity.Name)
                    .First().Id;

                topic.AutorId = autorId;
                context.Topics.Add(topic);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(topic);
        }

        /// <summary>
        /// Deleding
        /// </summary>
        /// <param name="id">Topic ID</param>
        /// <returns></returns>
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var topic = context.Topics.Include(t => t.Autor).SingleOrDefault(m => m.Id == id);

            if (topic == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(topic);
        }

        //Add Postmethod Delete Topic
        [HttpPost]
        [Authorize]
        public IActionResult Delete(int id)
        {
            Topic topic = context.Topics.Include(t => t.Autor).SingleOrDefault(m => m.Id == id);
            if (!(topic == null))
            {
                context.Topics.Remove(topic);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Editing
        /// </summary>
        /// <param name="id">Topic ID</param>
        /// <returns></returns>
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var topic = context.Topics.Include(t => t.Autor).SingleOrDefault(m => m.Id == id);

            if (topic == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(topic);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Topic currToppic)
        {
            Topic topic = context.Topics.Include(t => t.Autor).SingleOrDefault(m => m.Id == currToppic.Id);
            if (!(topic == null))
            {
                return RedirectToAction("Index", "Home");
            }
            topic.Description = currToppic.Description;
            topic.Title = topic.Title;
            topic.LastUpdatedDate = DateTime.Now;
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}