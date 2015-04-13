using ExtendedBlog.Model.Data;
using ExtendedBlog.Model.Repository;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExtendedBlog.Model.Infrastructure;

namespace ExtendedBlog.WebUI.Binder
{
    public class PostModelBinder : DefaultModelBinder
    {
        IKernel kernel;

        public PostModelBinder(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var post = (Post)base.BindModel(controllerContext, bindingContext);

            post.Category = null;
            post.Tags = null;

            var repository = kernel.Get<IPostRepository>();

            if (bindingContext.ValueProvider.GetValue("oper").AttemptedValue.Equals("edit"))
            {
                post.PostId = int.Parse(bindingContext.ValueProvider.GetValue("id").AttemptedValue);
                post.Modified = DateTime.UtcNow.ToSqlDateTime();
            }
            else
            {
                post.PostedOn = DateTime.UtcNow.ToSqlDateTime();
            }

            string categoryIdStr = bindingContext.ValueProvider.GetValue("Category.CategoryId").AttemptedValue;
            int categoryId;

            if (Int32.TryParse(categoryIdStr, out categoryId))
                post.Category = repository.Category(categoryId);

            string tagsId = bindingContext.ValueProvider.GetValue("Tags").AttemptedValue;

            if (tagsId != null)
            {
                var tags = tagsId.Split(',');
                if (tags.Length > 0)
                {
                    post.Tags = new List<Tag>();
                    foreach (var tag in tags)
                    {
                        int id;
                        if (Int32.TryParse(tag, out id))
                            post.Tags.Add(repository.Tag(id));
                    }
                }
            }

            return post;
        }
    }
}