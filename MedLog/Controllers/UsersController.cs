using MedLog.Service.DTOs.UserDTOs;
using MedLog.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MedLog.Controllers
{
  
    public class UsersController : RestFulSense
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet("id")]
        public async Task<ActionResult<UserResultDto>> GetAsync(int id)
        {
            var user = await userService.GetAsync(id);
            return user;
        }

        [HttpPost("id")]
        public async Task<UserResultDto> PostAsync(UserCreationDto dto)
        {
            var user = await userService.CreateAsync(dto);
            return user;
        }

        [HttpPut("id")]
        public async Task<UserResultDto> PutAsync(UserUpdateDto dto)
        {
            var user = await userService.UpdateAsync(dto);
            return user;
        }

        [HttpDelete("id")]
        public async Task<ActionResult<bool>> DeleteAsync(int id)
        {
            
                var user = await userService.DeleteAsync(id);
                return user;

        }

        //GET /users
        [HttpGet]
        public async Task<IEnumerable<UserResultDto>> GetAllAsync()
        {
            var users = await userService.GetAllAsync();
            return users;
        }
    }
}
