using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.API.Data;
using ZwajApp.API.Dtos;
using ZwajApp.API.Models;

namespace zwaj.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _Repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository Repo, IConfiguration config)
        {
            _Repo = Repo;
            _config = config;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _Repo.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            // validation
            userForRegisterDto.userName = userForRegisterDto.userName.ToLower();
            if (await _Repo.UserExists(userForRegisterDto.userName)) return BadRequest("this usre already exists");
            var userToCreate = new User() { userName = userForRegisterDto.userName };
            var userCreated = await _Repo.Register(userToCreate,userForRegisterDto.Password);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(UserForLoginDto userForLoginDto) {
            throw new Exception("Api sayes noooooooo!");
            var user = await _Repo.logIn(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);
            if (user == null) return Unauthorized();
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.userName)
            };
            // hashing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSting:Token").Value));
            // Signing Credentials
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512);
            var tokenDesciptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesciptor);
            return Ok(new {Token = tokenHandler.WriteToken(token)});
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
