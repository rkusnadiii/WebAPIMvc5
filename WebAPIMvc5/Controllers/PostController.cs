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

        // Helper function to get status messages based on status code
        private string GetStatusMessage(HttpStatusCode statusCode)
        {
            string _message;
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    _message = "Success (OK)";
                    break;
                case HttpStatusCode.BadRequest:
                    _message = "(Bad Request)";
                    break;
                case HttpStatusCode.NotFound:
                    _message = "(Not Found)";
                    break;
                case HttpStatusCode.Created:
                    _message = "Success (Created)";
                    break;
                default:
                    _message = "Pesan default untuk status code tidak dikenal";
                    break;
            }
            return _message;
        }

        // GET: api/Posts
        [Authorize]
        public IHttpActionResult Get()
        {
            var posts = db.Posts.ToList();
            return Ok(new
            {
                message = GetStatusMessage(HttpStatusCode.OK),
                status = HttpStatusCode.OK,
                data = posts
            });
        }

        // GET: api/Posts/1
        [Authorize]
        public IHttpActionResult Get(int id)
        {
            var post = db.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    message = GetStatusMessage(HttpStatusCode.NotFound),
                    status = HttpStatusCode.NotFound,
                    data = (object)null
                });

            return Ok(new
            {
                message = GetStatusMessage(HttpStatusCode.OK),
                status = HttpStatusCode.OK,
                data = post
            });
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
                return Created("api/posts/" + post.Id, new
                {
                    message = GetStatusMessage(HttpStatusCode.Created),
                    status = HttpStatusCode.Created,
                    data = post
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "An error occurred while adding the post: " + ex.Message,
                    status = HttpStatusCode.BadRequest,
                    data = (object)null
                });
            }
        }

        private IHttpActionResult BadRequest(object value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Posts/1
        [Authorize]
        public IHttpActionResult Put(int id, [FromBody] Post post)
        {
            var existingPost = db.Posts.FirstOrDefault(p => p.Id == id);
            if (existingPost == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    message = GetStatusMessage(HttpStatusCode.NotFound),
                    status = HttpStatusCode.NotFound,
                    data = (object)null
                });

            existingPost.Title = post.Title;
            existingPost.Body = post.Body;

            try
            {
                db.SaveChanges();
                return Ok(new
                {
                    message = GetStatusMessage(HttpStatusCode.OK),
                    status = HttpStatusCode.OK,
                    data = existingPost
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "An error occurred while updating the post: " + ex.Message,
                    status = HttpStatusCode.BadRequest,
                    data = (object)null
                });
            }
        }

        // DELETE: api/Posts/1
        [Authorize]
        public IHttpActionResult Delete(int id)
        {
            var post = db.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return Content(HttpStatusCode.NotFound, new
                {
                    message = GetStatusMessage(HttpStatusCode.NotFound),
                    status = HttpStatusCode.NotFound,
                    data = (object)null
                });

            db.Posts.Remove(post);
            try
            {
                db.SaveChanges();
                return Content(HttpStatusCode.NoContent, new
                {
                    message = GetStatusMessage(HttpStatusCode.NoContent),
                    status = HttpStatusCode.NoContent,
                    data = (object)null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "An error occurred while deleting the post: " + ex.Message,
                    status = HttpStatusCode.BadRequest,
                    data = (object)null
                });
            }
        }
    }
}
