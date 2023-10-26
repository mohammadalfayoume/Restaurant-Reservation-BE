using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using RestaurantsReservation.DTOs.AccountDto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RestaurantsReservation.Helpers;

namespace RestaurantsReservation.Controllers;

[Route("api/accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly ITokenRepository _tokenRepo;

    public AccountController(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager, IMapper mapper,
        ITokenRepository tokenRepo
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _tokenRepo = tokenRepo;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (ModelState.IsValid)
        {
            if (await UserExists(registerDto.Email)) return BadRequest("Account Already Exists");

            var user = _mapper.Map<AppUser>(registerDto);
            user.Email = registerDto.Email.ToLower();
            if (!Validations.IsValidEmail(registerDto.Email))
            {
                return BadRequest("Invalid Email");
            }
            
            user.Created = DateTime.UtcNow;
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "User");
            if (!roleResult.Succeeded) return BadRequest(result.Errors);
            return new UserDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = await _tokenRepo.CreateToken(user)
            };
        }
        return BadRequest(ModelState);

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

        if (user is null) return Unauthorized("Invalid UserName");

        var result = await _signInManager.PasswordSignInAsync(
               loginDto.UserName,
               loginDto.Password,
               false, //loginDto.RememberMe
               false
            );
        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("Login", "You are locked out.");
            }
            else
            {
                ModelState.AddModelError("Login", "Failed to login.");
            }
            return BadRequest(ModelState);
        }

        return new UserDto
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = await _tokenRepo.CreateToken(user)
        };

    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out successfully");
    }
    // Method to check if user exist
    private async Task<bool> UserExists(string email)
    {
        return await _userManager.Users.AnyAsync(user => user.Email == email.ToLower());
    }

}
