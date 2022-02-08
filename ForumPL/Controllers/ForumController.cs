using ForumBL.DTOs;
using ForumBL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;


namespace ForumPL.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("api/forums")]
    public class ForumController: Controller
    {
        private readonly IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> GetAll()
        {           
            return Ok(await _forumService.GetAllTopLevels());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {           
            return Ok(await _forumService.GetByIdAsync(id));
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> CreateNew([FromBody] ForumDTO data)
        {           
            return Ok(_forumService.AddAsync(data));
        }

        [HttpPatch]
        [Route("{id}/edit")]
        public async Task<ActionResult> Edit(int id, [FromBody] ForumDTO data)
        {
            await _forumService.UpdateAsync(id, data);

            return StatusCode(204); // NoContent
        }

        [HttpDelete]
        [Route("{id:int}/delete")]
        public async Task<ActionResult> Delete(int id)
        {
            await _forumService.DeleteByIdAsync(id);

            return StatusCode(204); // NoContent
        }
    }
}
