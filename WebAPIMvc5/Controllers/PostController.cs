using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using WebAPIMvc5.Models;
using System.Data.Entity; 

namespace WebAPIMvc5.Controllers
{
    public class PostsController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Posts
        [Authorize]
        public IHttpActionResult Get()
        {
            var posts = db.Posts.ToList(); 
            return Ok(posts);
        }

        // GET: api/Posts/1
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var post = db.Posts.FirstOrDefault(p => p.Id == id); 
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        // POST: api/Posts
        [Authorize]
        public IHttpActionResult Post([FromBody] Post post)
        {
            post.CreatedAt = DateTime.Now;

            try
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return Created("api/posts/" + post.Id, post);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while adding the post: " + ex.Message);
            }
        }

        // PUT: api/Posts/1
        [Authorize]
        public IHttpActionResult Put(int id, [FromBody] Post post)
        {
            var existingPost = db.Posts.FirstOrDefault(p => p.Id == id);
            if (existingPost == null)
                return NotFound();

            existingPost.Title = post.Title; 
            existingPost.Body = post.Body; 

            db.SaveChanges();
            return Ok(existingPost);
        }

        // DELETE: api/Posts/1
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var post = db.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return NotFound();

            db.Posts.Remove(post);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
