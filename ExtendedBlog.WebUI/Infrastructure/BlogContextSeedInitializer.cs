using ExtendedBlog.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ExtendedBlog.Model.Infrastructure;
using WebMatrix.WebData;

namespace ExtendedBlog.Model.Context
{
    public class BlogContextSeedInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    //DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            base.Seed(context);

            SeedMembership();

            Tag musicTag = new Tag
            {
                Name = "music",
                Description = "my music tag",
                Url = "music"
            };

            Category teach = new Category()
            {
                Name = "Образование",
                Description = "Раздел об образовании",
                Url = "teach"
            };

            for (int i = 1; i < 20; i++)
            {

                Post post1 = new Post()
                {
                    //PostId = i + 100,
                    Header = "Музыкальный пост " + i.ToString(),
                    Body = "body body" + i.ToString(),
                    BodyShort = "short body",
                    PostedOn = DateTime.Now.ToSqlDateTime(),
                    Title = "title for SEO" + i.ToString(),
                    Url = "music_post" + i.ToString(),
                    Published = true
                };

                post1.Tags.Add(musicTag);
                teach.Posts.Add(post1);
            }

            context.Categories.Add(teach);
            context.SaveChanges();
        }

        private void SeedMembership()
        {
            //WebSecurity.InitializeDatabaseConnection("EFDbContext",
            //                                            "UserProfile",
            //                                            "UserId",
            //                                            "UserName",
            //                                            autoCreateTables: true);

        }
    }
}
