using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApointMvcApp.Models;
using Domain;
using System.Text.RegularExpressions;

namespace ApointMvcApp.Controllers
{
    [Authorize]
    public class BlogController : BaseController
    {
        public ActionResult BlogWork()
        {
            return View(core.PostRepository.ReadAll().Where(p=>!p.IsDeleted).OrderByDescending(p=>p.ID).ToList());
        }

        public ActionResult CreatePost()
        {
            ViewBag.Title = "Создать пост";
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(PostModel model)
        {
            if (ModelState.IsValid)
            {
                core.PostRepository.Create(model.Title, model.Content);
                core.Submit();
                this.SetTagsToPost(model.Tags, core.PostRepository.Find(model.Title).ID);
                return RedirectToRoute("BlogWork");
            }
            ViewBag.Title = "Создать пост";
            ViewBag.HtmlContent = model.Content;
            return View(model);
        }

        public ActionResult ViewPost(string id)
        {
            Guid _id = Guid.Empty;
            try
            {
                Guid.TryParse(id, out _id);                    
                var post = core.PostRepository.Read(_id);
                return View(post);
            }
            catch {return RedirectToRoute("BlogWork");}
        }

        public ActionResult DeletePost(string id)
        {
            Guid _id = Guid.Empty;
            try
            {
                Guid.TryParse(id, out _id);
                core.PostRepository.Delete(_id);
                core.Submit();
                return RedirectToRoute("BlogWork");
            }
            catch { return RedirectToRoute("ViewPost", new { id = id }); }
        }

        public ActionResult EditPost(string id)
        {
            Guid _id = Guid.Empty;
            string tags = "";
            try
            {
                Guid.TryParse(id, out _id);
                ViewBag.Title = "Редактировать пост";
                var post = core.PostRepository.Read(_id);
                if(post.Tags != null)
                    for(int i = 0; i < post.Tags.Count; i++)
                        if(i == post.Tags.Count-1) tags += String.Format("{0}, ", post.Tags.ElementAt(i).Name); 
                        else tags += String.Format("{0}, ", post.Tags.ElementAt(i).Name); 
                var model = new PostModel(){
                    Title = post.Title,
                    Content = post.Content,
                    Tags = tags,
                };
                ViewBag.HtmlContent = model.Content;
                return View("CreatePost", model);
            }
            catch { return RedirectToRoute("BlogWork"); }
        }

        [HttpPost]
        public ActionResult EditPost(PostModel model, string id)
        {
            Guid _id = Guid.Empty;
            if (ModelState.IsValid)
            {
                try
                {
                    Guid.TryParse(id, out _id);
                    var old_post = core.PostRepository.Read(_id);
                    var new_post = new Post(model.Title, model.Content);
                    new_post.ID = old_post.ID;
                    this.SetTagsToPost(model.Tags, old_post.ID);
                    core.PostRepository.Update(new_post);
                    core.Submit();
                    return RedirectToRoute("ViewPost", new { id = _id, title = new_post.Title });
                }
                catch { ModelState.AddModelError("", "Произошла ошибка при изменении поста"); }
            }
            ViewBag.Title = "Редактировать пост";
            ViewBag.HtmlContent = model.Content;
            return View("CreatePost", model);
        }

        private void SetTagsToPost(string TagsLine, Guid postID)
        {
            var post = core.PostRepository.Read(postID);
            string pattern = "(, )|(,)|( , )";
            string[] tags = Regex.Split(TagsLine, pattern, RegexOptions.IgnoreCase);
            try
            {
                foreach (string match in tags)
                    if ((String.Compare(match, ",") != 0) && (String.Compare(match, ", ") != 0) && (String.Compare(match, " , ") != 0))
                    {
                        core.TagRepository.Create(match);
                        core.Submit();
                        var tag = core.TagRepository.Find(match);
                        if (tag.Posts == null)
                        {
                            tag.Posts = new List<Post>();
                            tag.Posts.Add(post);
                        }
                        else if (!tag.Posts.Contains(post)) tag.Posts.Add(post);
                        core.Submit();
                    }
            }
            catch { }
        }

    }
}
