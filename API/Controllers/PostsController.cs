using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Posts;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Persistence;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class PostsController : ControllerBase
    {
        private readonly IMediator mediator;
        public PostsController(IMediator mediator) => this.mediator = mediator; 

        public async Task<ActionResult<List<Post>>> List() 
        {
            return await this.mediator.Send(new List.Query());
        }
        /// <summary>
        /// GET api/posts
        /// </summary>
        /// <returns>A list of posts</returns>
        [HttpGet]
        public ActionResult<List<Post>> Get()
        {
            return this.context.Posts.ToList();

        }

        /// <summary>
        /// Get api/posts/[id]
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>A single post</returns>
        [HttpGet("{id}")]
        public ActionResult<Post> GetById(Guid id)
        {
            return this.context.Posts.Find(id);
        }
         /// <summary>
        /// POST api/post
        /// </summary>
        /// <param name="request">JSON request containing post fields</param>
        /// <returns>A new post</returns>
        [HttpPost]
        public ActionResult<Post> Create([FromBody]Post request)
        {
            var post = new Post
            {
                Id = request.Id,
                Title = request.Title,
                Body = request.Body,
                Date = request.Date,
            };

            context.Posts.Add(post);
            var success = context.SaveChanges() > 0;

            if (success)
            {
                return post;
            }

            throw new Exception("Error creating post");
        }
        /// <summary>
        /// PUT api/put
        /// </summary>
        /// <param name="request">JSON request containing one or more updated post fields</param>
        /// <returns>An update post</returns>
        [HttpPut]
        public ActionResult<Post> Update([FromBody]Post request)
        {
            var post = context.Posts.Find(request.Id);

            if (post == null)
            {
                throw new Exception("Could not find post");
            }

            post.Title = request.Title != null ? request.Title : post.Title;
            post.Body = request.Body != null ? request.Body : post.Body;
            post.Date = request.Date != null ? request.Date : post.Date;

            var success = context.SaveChanges() > 0;

            if (success)
            {
                return post;
            }

            throw new Exception("Error updating post");
        }
        /// <summary>
        /// DELETE api/post/[id]
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>True, if successful</returns>
        [HttpDelete("{id}")]

        public ActionResult<bool> Delete(Guid id)
        {
            var post = context.Posts.Find(id);

            if (post == null)
            {
                throw new Exception("Could not find post");
            }

            context.Remove(post);

            var success = context.SaveChanges() > 0;

            if (success)
            {
                return true;
            }

            throw new Exception("Error deleting post");
        }
    }
}