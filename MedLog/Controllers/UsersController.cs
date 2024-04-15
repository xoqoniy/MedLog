using MedLog.Service.DTOs.UserDTOs;
using MedLog.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;

namespace MedLog.Controllers
{

    public class UsersController : RestFulSense
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<UserResultDto>> GetAsync(string id)
        {
            var user = await userService.GetAsync(id);
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<UserResultDto>> PostAsync(string selectedHospitalId, [FromBody] UserCreationDto dto)
        {
            var user = await userService.CreateAsync(dto, selectedHospitalId);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResultDto>> PutAsync([FromRoute] string id, [FromBody] UserUpdateDto dto)
        {
            var user = await userService.UpdateAsync(id, dto);
            return user;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(string id)
        {
             var user = await userService.DeleteAsync(id);
             return user;
        }

        //GET/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResultDto>>> GetAllAsync()
        {
            return await userService.GetAllAsync();
        }
    }
}
