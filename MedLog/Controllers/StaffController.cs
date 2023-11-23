using MedLog.Service.DTOs.StaffDTOs;
using MedLog.Service.DTOs.UserDTOs;
using MedLog.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedLog.Controllers
{
    public class StaffController : RestFulSense
    {
        private readonly IStaffService userService;
        public StaffController(IStaffService userService)
        {
            this.userService = userService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<StaffResultDto>> GetAsync(string id)
        {
            var user = await userService.GetAsync(id);
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<StaffResultDto>> PostAsync(StaffCreationDto dto)
        {
            var user = await userService.CreateAsync(dto);
            return user;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StaffResultDto>> PutAsync(string id, StaffUpdateDto dto)
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
        public async Task<ActionResult<IEnumerable<StaffResultDto>>> GetAllAsync()
        {
            var users = await userService.GetAllAsync();
            return users;
        }
    }
}
