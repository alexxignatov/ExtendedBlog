using ExtendedBlog.Model.Data;
using ExtendedBlog.Model.Repository;
using ExtendedBlog.WebUI.Binder;
using ExtendedBlog.WebUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExtendedBlog.WebUI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private IPostRepository repository;

        public AdminController(IPostRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ContentResult Posts(JqInViewModel jqParams)
        {
            var posts = repository.Posts(jqParams.page, jqParams.rows, jqParams.sidx, jqParams.sord == "asc");

            var totalPosts = repository.TotalPosts(false);


            /* Ответ в формате 
             * 
             * page: current page number,
             * records: total number of records,
             * rows: records,
             * total: number of records returned now
             * 
             * */


            /*return Json(new 
                        {
                            page = jqParams.page,
                            records = totalPosts,
                            rows = posts,
                            total = Math.Ceiling(Convert.ToDouble(totalPosts) / jqParams.rows)

                        }, JsonRequestBehavior.AllowGet);*/

            string str = JsonConvert.SerializeObject(new
            {
                page = jqParams.page,
                records = totalPosts,
                rows = posts,
                total = Math.Ceiling(Convert.ToDouble(totalPosts) / jqParams.rows)
            });
            //,  new CustomDateTimeConverter());

            return Content(str
                    , "application/json");
        }

        public ActionResult GoToPost(int id = 0)
        {
            var post = repository.PostById(id);

            if (post == null)
                throw new HttpException(404, "Статья не найдена");

            return RedirectToAction("FullPost", "Post", new { postUrl = post.Url });
        }

        public ContentResult GetCategoriesHtml()
        {
            var categories = repository.CategoriesByName();

            if (categories == null)
                return null;

            TagBuilder select = new TagBuilder("select");
            foreach (var cat in categories)
            {
                TagBuilder opt = new TagBuilder("option");
                opt.MergeAttribute("value", cat.CategoryId.ToString());
                opt.InnerHtml += cat.Name;

                select.InnerHtml += opt.ToString();
            }

            return Content(select.ToString(), "text/html");
        }

        public ContentResult GetTagsHtml()
        {
            var tags = repository.Tags();

            if (tags == null)
                return null;

            TagBuilder select = new TagBuilder("select");
            select.MergeAttribute("multiple", "multiple");

            foreach (var tag in tags)
            {
                TagBuilder opt = new TagBuilder("option");
                opt.MergeAttribute("value", tag.TagId.ToString());
                opt.InnerHtml += tag.Name;

                select.InnerHtml += opt.ToString();
            }

            return Content(select.ToString(), "text/html");
        }

        [HttpPost, ValidateInput(false)]
        public ContentResult AddPost(Post post)
        {
            string json;

            ModelState.Clear();

            if (TryValidateModel(post))
            {
                int postId = repository.AddPost(post);
                json = JsonConvert.SerializeObject(new
                {
                    id = postId,
                    success = true,
                    message = "Post added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the post."
                });
            }


            return Content(json, "application/json");
        }

        [HttpPost, ValidateInput(false)]
        public ContentResult EditPost(Post post)
        {
            string json;

            ModelState.Clear();

            if (TryValidateModel(post))
            {
                repository.EditPost(post);
                json = JsonConvert.SerializeObject(new
                {
                    id = post.PostId,
                    success = true,
                    message = "Post edited successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to edit the post."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult DeletePost(int id)
        {
            repository.DeletePost(id);

            var json = JsonConvert.SerializeObject(
                new
                {
                    id = 0,
                    success = true,
                    message = "Post delete successfully."
                });
            return Content(json, "application/json");
        }

        public ContentResult Categories()
        {
            var categories = repository.Categories();

            return Content(JsonConvert.SerializeObject(
                new
                {
                    page = 1,
                    records = categories.Count,
                    rows = categories,
                    total = 1
                }),
                "application/json");
        }

        [HttpPost]
        public ContentResult AddCategory([Bind(Exclude = "CategoryId")]Category category)
        {
            string json;

            if (ModelState.IsValid)
            {
                var id = repository.AddCategory(category);
                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Category added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the category."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult EditCategory([ModelBinder(typeof(CategoryModelBinder))]Category category)
        {
            string json;
            if (ModelState.IsValid)
            {
                repository.EditCategory(category);
                json = JsonConvert.SerializeObject(new
                {
                    id = category.CategoryId,
                    success = true,
                    message = "Category modified successfully"
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult DeleteCategory(int id)
        {
            string json;

            if (repository.DeleteCategory(id))
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Category deleted successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to delete category."
                });
            }

            return Content(json, "application/json");
        }

        public ContentResult Tags()
        {
            var tags = repository.Tags();

            return Content(JsonConvert.SerializeObject(new
            {
                page = 1,
                records = tags.Count,
                rows = tags,
                total = 1
            }), "application/json");
        }

        public ContentResult AddTag([Bind(Exclude = "TagId")]Tag tag)
        {
            string json;

            if (ModelState.IsValid)
            {
                int tagId = repository.AddTag(tag);
                json = JsonConvert.SerializeObject(new
                {
                    id = tagId,
                    success = true,
                    message = "Tag added successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to add the tag."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult EditTag([ModelBinder(typeof(TagModelBinder))]Tag tag)
        {
            string json;
            if (ModelState.IsValid)
            {
                repository.EditTag(tag);
                json = JsonConvert.SerializeObject(new
                {
                    id = tag.TagId,
                    success = true,
                    message = "Tag modified successfully"
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to save the changes."
                });
            }

            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult DeleteTag(int id)
        {
            string json;

            if (repository.DeleteTag(id))
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = id,
                    success = true,
                    message = "Tag deleted successfully."
                });
            }
            else
            {
                json = JsonConvert.SerializeObject(new
                {
                    id = 0,
                    success = false,
                    message = "Failed to delete tag."
                });
            }

            return Content(json, "application/json");
        }
    }
}