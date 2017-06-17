using ExtendedBlog.Model.Context;
using ExtendedBlog.Model.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.Model.Repository
{
    public class PostRepository : IPostRepository
    {
        private EFDbContext context;

        public PostRepository(EFDbContext context)
        {
            this.context = context;
        }

        public ICollection<Post> AllPosts()
        {
            return AllPosts(true);
        }

        public IList<Post> AllPosts(bool onlyPublished)
        {
            return context.Posts.OrderBy(z => z.PostedOn).Where(z => onlyPublished || z.Published).ToList();
        }

        public int TotalPosts(bool onlyPublished = true)
        {
            return context.Posts.Count(z => !onlyPublished || z.Published == true);
        }

        public int AddPost(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();

            return post.PostId;
        }

        public void EditPost(Post post)
        {
            context.Entry(post).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var post = context.Posts.Find(id);
            if (post != null)
            {
                context.Posts.Remove(post);
                context.SaveChanges();
            }
        }

        public Post PostByUrl(string urlSeo)
        {
            return context.Posts.Where(z => z.Url.Equals(urlSeo)).FirstOrDefault();
        }

        public IList<Post> Posts(int pageNumber, int postsPerPage)
        {
            return context.Posts
                .OrderBy(z => z.PostedOn)
                .Skip((pageNumber - 1) * postsPerPage)
                .Take(postsPerPage)
                .ToList();
        }

        public IList<Post> PostsByTag(string urlSeo, int curPageNumber, int pageSize)
        {
            return context.Posts.Where(z => z.Published
                                        && z.Tags.Any(t => t.Url.Equals(urlSeo)))
                                .OrderBy(z => z.PostedOn)
                                .Skip((curPageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();
        }

        public int TotalPostsByTag(string tagUrl)
        {
            return context.Posts.Count(z => z.Published && z.Tags.Any(t => t.Url.Equals(tagUrl)));
        }

        public IList<Post> Posts(int pageNumber, int postsPerPage, string sortColumn, bool ascending)
        {
            var query = context.Posts.Include(z => z.Category).Include(z => z.Tags);

            if (ascending)
            {
                switch (sortColumn)
                {
                    case "Header":
                        query = query.OrderBy(p => p.Header);
                        break;
                    case "Published":
                        query = query.OrderBy(p => p.Published);
                        break;
                    case "PostedOn":
                        query = query.OrderBy(p => p.PostedOn);
                        break;
                    case "Modified":
                        query = query.OrderBy(p => p.Modified);
                        break;
                    case "Category":
                        query = query.OrderBy(p => p.Category.Name);
                        break;
                    default:
                        query = query.OrderBy(p => p.PostedOn);
                        break;
                }
            }
            else
            {
                switch (sortColumn)
                {
                    case "Header":
                        query = query.OrderByDescending(p => p.Header);
                        break;
                    case "Published":
                        query = query.OrderByDescending(p => p.Published);
                        break;
                    case "PostedOn":
                        query = query.OrderByDescending(p => p.PostedOn);
                        break;
                    case "Modified":
                        query = query.OrderByDescending(p => p.Modified);
                        break;
                    case "Category":
                        query = query.OrderByDescending(p => p.Category.Name);
                        break;
                    default:
                        query = query.OrderByDescending(p => p.PostedOn);
                        break;
                }
            }

            return query.Skip((pageNumber - 1) * postsPerPage).Take(postsPerPage).ToList();
        }

        public Post PostById(int postId)
        {
            return context.Posts.FirstOrDefault(z => z.PostId == postId);
        }

        public IList<Category> CategoriesByName()
        {
            return context.Categories.OrderBy(z => z.Name).ToList();
        }

        public IList<Tag> Tags()
        {
            return context.Tags.OrderBy(z => z.Name).ToList();
        }

        public Category Category(int categoryId)
        {
            return context.Categories.FirstOrDefault(z => z.CategoryId == categoryId);
        }

        public Tag Tag(int tagId)
        {
            return context.Tags.FirstOrDefault(z => z.TagId == tagId);
        }

        public IList<Category> Categories()
        {
            return context.Categories.OrderBy(z => z.Name).ToList();
        }

        public int AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();

            return category.CategoryId;
        }

        public void EditCategory(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
        }

        public bool DeleteCategory(int categoryId)
        {
            Category category = context.Categories.Include(z => z.Posts).FirstOrDefault(p => p.CategoryId == categoryId);

            if (category == null)
                return false;

            context.Posts.RemoveRange(category.Posts.ToList());

            context.Categories.Remove(category);
            context.SaveChanges();

            return true;

            /*var category = context.Categories.Find(categoryId);
            if(category != null)
            {
                

                context.Categories.Remove(category);
                context.SaveChanges();
            }*/
        }


        public int AddTag(Tag tag)
        {
            context.Tags.Add(tag);
            context.SaveChanges();

            return tag.TagId;
        }

        public void EditTag(Tag tag)
        {
            context.Entry(tag).State = EntityState.Modified;
            context.SaveChanges();
        }

        public bool DeleteTag(int tagId)
        {
            var tag = context.Tags.Find(tagId);
            if (tag == null)
                return false;

            context.Tags.Remove(tag);
            context.SaveChanges();
            return true;
        }
    }
}
