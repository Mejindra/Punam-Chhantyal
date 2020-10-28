using System.Collections.Generic;
using System.Linq;
using Domain;
using Persistence;

namespace Persistance
{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (context.Posts.Count() == 0)
            {
                List<Post> seedPosts = new List<Post>
                {
                    new Post() { Title = "First Post", Body = "This is the body of my first post. It will be check for the database."},
                    new Post() { Title = "Second Post", Body = "This is the body of my second post. It will be check for the database."},
                    new Post() { Title = "Third Post", Body = "This is the body of my third post. It will be check for the database."},
                };

                context.Posts.AddRange(seedPosts);

                context.SaveChanges();               
            }
        }
    }
}

