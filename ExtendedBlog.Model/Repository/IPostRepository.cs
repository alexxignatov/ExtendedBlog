using ExtendedBlog.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.Model.Repository
{
    public interface IPostRepository
    {
        ICollection<Post> AllPosts();
        IList<Post> AllPosts(bool onlyPublished);
        int TotalPosts(bool onlyPublished = true);
        int AddPost(Post post);
        void EditPost(Post post);
        void DeletePost(int id);
        Post PostByUrl(string urlSeo);
        Post PostById(int postId);
        IList<Post> Posts(int pageNumber, int postsPerPage);
        IList<Post> Posts(int pageNumber, int postsPerPage, string sortColumn, bool ascending);

        IList<Post> PostsByTag(string tagUrl, int curPageNumber, int pageSize);
        int TotalPostsByTag(string tagUrl);

        IList<Category> CategoriesByName();
        Category Category(int categoryId);
        IList<Category> Categories();
        int AddCategory(Category category);
        void EditCategory(Category category);
        bool DeleteCategory(int categoryId);

        /// <summary>
        /// Возвращает список Тегов, отсортированных по имени
        /// </summary>
        IList<Tag> Tags();
        Tag Tag(int tagId);
        int AddTag(Tag tag);
        void EditTag(Tag tag);
        bool DeleteTag(int tagId);
    }
}
