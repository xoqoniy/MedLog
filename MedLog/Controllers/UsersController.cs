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


        [HttpGet]
        public async Task<ActionResult> GetAsync(int id)
        {
            var user = await userService.GetAsync(id);
            return ;
        }

        [HttpPost]
        public async Task<UserResultDto> PostAsync(UserCreationDto dto)
        {
            var user = await userService.CreateAsync(dto);
            return user;
        }

        [HttpPut]
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

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var users = await userService.GetAllAsync();
            return Ok(users);
        }
    }
}
