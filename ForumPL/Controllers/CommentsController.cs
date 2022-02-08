
using ForumBL.DTOs;
using ForumBL.Interfaces;
using ForumBL.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace ForumPL.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("postid/{id:int}")]
        public async Task<ActionResult> GetByPostId(int id, int page = 1, int pageSize = 15)
        {         
            return Ok(await _commentService.GetByPostId(id));
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateNew([FromBody] CommentDTO data)
        {            
            return Ok(_commentService.AddAsync(data));
        }

        [HttpPatch]
        [Route("{id}/edit")]
        public async Task<ActionResult> Edit(int id, [FromBody] CommentDTO data)
        {
            await _commentService.UpdateAsync(id, data);
            return StatusCode(204); //NoContent
        }

        [HttpDelete]
        [Route("{id:int}/delete")]
        public async Task<ActionResult> Delete(int id)
        {
            await _commentService.DeleteByIdAsync(id);
            return StatusCode(204); //NoContent
        }
    }


}
