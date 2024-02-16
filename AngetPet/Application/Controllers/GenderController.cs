using AngetPet.Application.Dtos;
using AngetPet.Application.Services;
using AngetPet.Shared.Helpers;
using AngetPet.Shared.Paginator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AngetPet.Application.Controllers
{
    [ApiController]
    [Route("/genders")]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService service;

        public GenderController(IGenderService service)
        {
            this.service = service;
        }

        [HttpPost("pageable")]
        [Authorize]
        public async Task<IActionResult> Pageable([FromBody] Page page)
        {
            var pageable = await service.Pageable(User, page);
            return Ok(pageable);
        }

        [HttpPost("selector")]
        [Authorize]
        public async Task<IActionResult> Selector()
        {
            var selector = await service.Selector(User);
            return Ok(selector);
        }

        [HttpPost]
        [Authorize(Roles = ConstantHelper.Role.ADMIN)]
        public async Task<IActionResult> Create([FromBody] GenderRequest request)
        {
            var pageable = await service.Create(User, request);
            return Ok(pageable);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await service.FindById(User, id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = ConstantHelper.Role.ADMIN)]
        public async Task<IActionResult> Update([FromBody] GenderRequest request, [FromRoute] int id)
        {
            var pageable = await service.Update(User, id, request);
            return Ok(pageable);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ConstantHelper.Role.ADMIN)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var pageable = await service.Remove(User, id);
            return Ok(pageable);
        }
    }
}
