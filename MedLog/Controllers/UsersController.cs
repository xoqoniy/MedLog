using MedLog.Domain.Entities;
using MedLog.Service.DTOs.UserDTOs;
using MedLog.Service.Interfaces;
using MedLog.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using ZstdSharp.Unsafe;

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
        public async Task<ActionResult<UserResultDto>> PostAsync(UserCreationDto dto)
        {
            var user = await userService.CreateAsync(dto);
            return user;
        }

        [HttpPut("{_id}")]
        public async Task<IActionResult> UpdateUser(string _id)
        {
            var user = await userService.UpdateAsync(_id);
            return Ok(user);
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
            var users = await userService.GetAllAsync();
            return users;
        }
    }
}
