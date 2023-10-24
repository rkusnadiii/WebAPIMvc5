using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using WebAPIMvc5.Models;

namespace WebAPIMvc5.Controllers
{
    public class PostsController : ApiController
    {
        private List<Post> posts = new List<Post>(); // Ganti ini dengan sumber data yang sesuai

        // GET: api/Posts
        public IHttpActionResult Get()
        {
            return Ok(posts);
        }

        // GET: api/Posts/1
        public IHttpActionResult Get(int id)
        {
            var post = posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        // POST: api/Posts
        public IHttpActionResult Post([FromBody] Post post)
        {
            // Logika untuk menambahkan post ke sumber data
            // Kemudian kirim respons Created dengan URL ke post yang baru dibuat
            return Created("api/posts/" + post.Id, post);
        }

        // PUT: api/Posts/1
        public IHttpActionResult Put(int id, [FromBody] Post post)
        {
            // Logika untuk mengupdate post dalam sumber data
            return Ok(post);
        }

        // DELETE: api/Posts/1
        public IHttpActionResult Delete(int id)
        {
            // Logika untuk menghapus post dari sumber data
            return StatusCode(HttpStatusCode.NoContent);
        }
    }

}