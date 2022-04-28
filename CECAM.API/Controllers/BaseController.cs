using Microsoft.AspNetCore.Mvc;
using CECAM.App.Interfaces;
using CECAM.Domain.Enums;
using CECAM.Domain.Models;
using CECAM.Domain.Requests;
using CECAM.Domain.Responses;

namespace CECAM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity> : ControllerBase where TEntity : Entity
    {
        private readonly IBaseService<TEntity> _service;

        public BaseController(IBaseService<TEntity> service) => _service = service;

        /// <summary>
        /// CommandEnum: | 2 - List | 3 - Count |
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetByQuery([FromQuery] SearchRequest request)
            => Ok(new ApiResponse<TEntity>(await _service.Get(request, null)));

        /// <summary>
        /// CommandEnum: | 1 - Obtain |
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
            => Ok(new ApiResponse<TEntity>(await _service.Get(null, new IdRequest(id, CommandEnum.Obtain))));

        /// <summary>
        /// CommandEnum: | 4 - Create |
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EntityRequest<TEntity> request)
            => Ok(new ApiResponse<TEntity>(await _service.Post(request)));

        /// <summary>
        /// CommandEnum: | 5 - Update | 6 - Delete | 7 - Undelete
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] EntityRequest<TEntity> request)
            => Ok(new ApiResponse<TEntity>(await _service.Put(request)));

        /// <summary>
        /// CommandEnum: | 8 - HardDelete |
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
            => Ok(new ApiResponse<TEntity>(await _service.Delete(new IdRequest(id, CommandEnum.HardDelete))));
    }
}
