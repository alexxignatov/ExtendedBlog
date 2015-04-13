using ExtendedBlog.Model.Data;
using ExtendedBlog.Model.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExtendedBlog.WebUI.Models
{
    public class PostsListViewModel
    {
        private int perPage = 4;
        public PageInfo PageInfo { get; private set; }
        public IList<Post> Posts { get; private set; }
        public string CurrentTagUrl { get; private set; }

        public PostsListViewModel(IPostRepository repository, int pageNumber)
        {
            Posts = repository.Posts(pageNumber, perPage);

            PageInfo = new PageInfo 
            {
                CurrentPageNumber = pageNumber,
                PostPerPage = perPage,
                TotalPosts = repository.TotalPosts()
            };
        }

        public PostsListViewModel(IPostRepository repository, string tagUrl, int pageNumber)
        {
            Posts = repository.PostsByTag(tagUrl, pageNumber, perPage);

            PageInfo = new PageInfo
            {
                CurrentPageNumber = pageNumber,
                PostPerPage = perPage,
                TotalPosts = repository.TotalPostsByTag(tagUrl)
            };

            CurrentTagUrl = tagUrl;
        }
    }
}