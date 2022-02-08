using ForumBL.DTOs;
using ForumBL.Interfaces;
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
    [Route("api/posts")]
    public class PostController: Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {           
            return Ok(await _postService.GetByIdAsync(id));
        }

        [AllowAnonymous]
        [HttpGet]
        //[Route("forumid/{id:int}")]
        public async Task<ActionResult> GetByForumId(int id)
        {         
            return Ok(await _postService.GetByForumId(id));
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateNew([FromBody] PostDTO data)
        {
          
            return Ok(await _postService.AddAsync(data));
        }

        [HttpPatch]
        [Route("{id}/edit")]
        public async Task<ActionResult> Edit(int id, [FromBody] PostDTO data)
        {
            await _postService.UpdateAsync(id, data);

            return StatusCode(204);//CoContent
        }

        [HttpDelete]
        [Route("{id:int}/delete")]
        public async Task<ActionResult> Delete(int id)
        {
            await _postService.DeleteByIdAsync(id);

            return StatusCode(204);
        }
    }
}
