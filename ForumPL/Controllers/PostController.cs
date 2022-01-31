using ForumBL.DTOs;
using ForumBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ForumPL.Controllers
{
    [Authorize]
    [RoutePrefix("api/posts")]
    public class PostController: ApiController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {           
            return Ok(await _postService.GetByIdAsync(id));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("forumid/{id:int}")]
        public async Task<IHttpActionResult> GetByForumId(int id, int page = 1, int pageSize = 15)
        {         
            return Ok(await _postService.GetByForumId(id, page, pageSize));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateNew([FromBody] PostDTO data)
        {
          
            return Ok(await _postService.AddAsync(data));
        }

        [HttpPatch]
        [Route("{id}/edit")]
        public async Task<IHttpActionResult> Edit(int id, [FromBody] PostDTO data)
        {
            await _postService.UpdateAsync(id, data);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id:int}/delete")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _postService.DeleteByIdAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
