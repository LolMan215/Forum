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
    [Authorize(Roles = "Administrator")]
    [RoutePrefix("api/forums")]
    public class ForumController: ApiController
    {
        private readonly IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(int page = 1, int pageSize = 15)
        {         
            return Ok(_forumService.Get(page, pageSize));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("toplevel")]
        public async Task<IHttpActionResult> GetAllTopLevel(int page = 1, int pageSize = 15)
        {           
            return Ok(await _forumService.GetAllTopLevels(page, pageSize));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {           
            return Ok(await _forumService.GetByIdAsync(id));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateNew([FromBody] ForumDTO data)
        {           
            return Ok(_forumService.AddAsync(data));
        }

        [HttpPatch]
        [Route("{id}/edit")]
        public async Task<IHttpActionResult> Edit(int id, [FromBody] ForumDTO data)
        {
            await _forumService.UpdateAsync(id, data);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("{id:int}/delete")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _forumService.DeleteByIdAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
