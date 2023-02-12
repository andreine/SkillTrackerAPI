using Domain.Entities.Identity;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillTracker.Dtos;

namespace SkillTracker.Controllers
{
    public class AccountController: BaseApiController
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(400);

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(400);

            return new UserDto
            {
                Email = user.Email,
                Token =  await _tokenService.CreateToken(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }



        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return BadRequest(400);
            }

            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(400);

            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = await _tokenService.CreateToken(user),
                Email = user.Email
            };
        }


        [HttpPost("createaccountforuser")]
        public async Task<ActionResult<UserDto>> CreateAccountForUser(NewUserAccountDto newUserDto)
        {
            if (CheckEmailExistsAsync(newUserDto.Email).Result.Value)
            {
                return BadRequest(400);
            }

            var user = new AppUser
            {
                FirstName = newUserDto.FirstName,
                LastName = newUserDto.LastName,
                Email = newUserDto.Email,
                UserName = newUserDto.Email
            };

            var result = await _userManager.CreateAsync(user, newUserDto.Password);

            if (!result.Succeeded) return BadRequest(400);

            var roleToAssign = _roleManager.FindByNameAsync(newUserDto.Role).Result;

            if(roleToAssign != null)
            {
                await _userManager.AddToRoleAsync(user, roleToAssign.Name);
            }
            else
            {
                await _userManager.DeleteAsync(user);
                return BadRequest("No such role");
            }

            return Ok(new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }

        [HttpGet("getallusers")]
        public async Task<IList<AppUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }


        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }



    }
}
