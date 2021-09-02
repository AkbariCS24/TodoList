using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoList.BusinessLayer.Contracts;
using TodoList.BusinessLayer.DTOs;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private IConfiguration _config;

        public AccountController(IRepositoryWrapper repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            IActionResult response = Unauthorized();
            var user = _repository.Account.AuthenticateUser(loginDTO);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public IActionResult Register([FromBody] UserDTO userDTO)
        {
            IActionResult response = Unauthorized();
            var user = _repository.Account.RegisterUser(userDTO);

            if (user != null)
            {
                response = Ok(new { user = userDTO });
            }

            return response;
        }

        private string GenerateJSONWebToken(UserDTO userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
        new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
