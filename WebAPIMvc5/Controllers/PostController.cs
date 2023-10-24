using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using WebAPIMvc5.Models;
using System.Data.Entity; // Import namespace Entity

namespace WebAPIMvc5.Controllers
{
    public class PostsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext(); // Membuat instance DbContext

        // GET: api/Posts
        public IHttpActionResult Get()
        {
            var posts = db.Posts.ToList(); // Mengambil semua entitas Posts dari database
            return Ok(posts);
        }

        // GET: api/Posts/1
        public IHttpActionResult Get(int id)
        {
            var post = db.Posts.FirstOrDefault(p => p.Id == id); // Mengambil entitas Post berdasarkan ID
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        // POST: api/Posts
        public IHttpActionResult Post([FromBody] Post post)
        {
            // Logika untuk menambahkan post ke sumber data
            db.Posts.Add(post);
            db.SaveChanges();

            // Kemudian kirim respons Created dengan URL ke post yang baru dibuat
            return Created("api/posts/" + post.Id, post);
        }

        // PUT: api/Posts/1
        public IHttpActionResult Put(int id, [FromBody] Post post)
        {
            // Logika untuk mengupdate post dalam sumber data
            var existingPost = db.Posts.FirstOrDefault(p => p.Id == id);
            if (existingPost == null)
                return NotFound();

            existingPost.Title = post.Title; 
            existingPost.Body = post.Body; 

            db.SaveChanges();
            return Ok(existingPost);
        }

        // DELETE: api/Posts/1
        public IHttpActionResult Delete(int id)
        {
            var post = db.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return NotFound();

            db.Posts.Remove(post);
            db.SaveChanges();

            // Logika untuk menghapus post dari sumber data
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
