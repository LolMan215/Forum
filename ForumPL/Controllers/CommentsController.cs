
using ForumBL.DTOs;
using ForumBL.Interfaces;
using ForumBL.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace ForumPL.Controllers
{
    [Authorize]
    [RoutePrefix("api/comments")]
    public class CommentsController : ApiController
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("postid/{id:int}")]
        public async Task<IHttpActionResult> GetByPostId(int id, int page = 1, int pageSize = 15)
        {         
            return Ok(await _commentService.GetByPostId(id, page, pageSize));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateNew([FromBody] CommentDTO data)
        {            
            return Ok(_commentService.AddAsync(data));
        }

        [HttpPatch]
        [Route("{id}/edit")]
        public async Task<IHttpActionResult> Edit(int id, [FromBody] CommentDTO data)
        {
            await _commentService.UpdateAsync(id, data);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id:int}/delete")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _commentService.DeleteByIdAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }


}
