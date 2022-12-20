using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectCRUD.DTOs;
using ProjectCRUD.Services.AuthService;

namespace ProjectCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController: ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthenticateController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Login(UserDTO request)
        {
            var id = await _authRepo.Login(request.Username, request.Password);
            if (id == null)
            {
                //* A generic error message is better than saying the password is incorrect for security reasons.
                return Unauthorized("The username or password is incorrect.");
            }
            return Ok(id);
        }

        //! Test
        [HttpPost("Register")]
        public async Task<ActionResult<int>> Register(UserDTO request)
        {
            var id = await _authRepo.Register(new User {Username = request.Username}, request.Password);
            if (id == null)
            {
                return BadRequest();
            }
            return Ok(id);
        }
    }
}