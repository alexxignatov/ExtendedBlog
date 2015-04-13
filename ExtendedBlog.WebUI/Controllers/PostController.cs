using ExtendedBlog.Model.Repository;
using ExtendedBlog.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtendedBlog.WebUI.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository repository;

        public PostController(IPostRepository repo)
        {
            repository = repo;
        }

        //
        // GET: /Post/

        public ActionResult List(int pageNumber = 1)
        {
            PostsListViewModel model = new PostsListViewModel(repository, pageNumber);
            return View(model);
        }

        public ViewResult FullPost(string postUrl)
        {
            var post = repository.PostByUrl(postUrl);

            if (post == null)
                throw new HttpException(404, "Статья не найдена");

            //if (post.Published == false && User.Identity.IsAuthenticated == false)
            //    throw new HttpException(401, "Статья не опубликована");

            return View(post);
        }

        public ViewResult PostsForTag(string tagUrl, int pageNumber = 1)
        {
            PostsListViewModel model = new PostsListViewModel(repository, tagUrl, pageNumber);
            return View(model);
        }
    }
}