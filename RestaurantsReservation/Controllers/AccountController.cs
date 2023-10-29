using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using RestaurantsReservation.DTOs.AccountDtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.RegularExpressions;

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
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (await UserExists(registerDto.Email)) return BadRequest("Account Already Exists");

        registerDto.Email = registerDto.Email.ToLower();

        if (!IsValidEmail(registerDto.Email)) return BadRequest("Invalid Email");

        var user = _mapper.Map<AppUser>(registerDto);
           
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
        if (result.Succeeded)
            return new UserDto
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = await _tokenRepo.CreateToken(user)
            };
        ModelState.AddModelError("Login", result.IsLockedOut ? "You are locked out." : "Failed to login.");

        return BadRequest(ModelState);

    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out successfully");
    }

    /// <summary>
    /// Check if user existed in the database
    /// </summary>
    /// <param name="email"></param>
    /// <returns>Boolean</returns>
    private async Task<bool> UserExists(string email)
    {
        return await _userManager.Users.AnyAsync(user => user.Email == email.ToLower());
    }

    /// <summary>
    /// Validate email address using regular expression
    /// </summary>
    /// <param name="email"></param>
    /// <returns>Boolean</returns>
    private static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(emailPattern);
        return regex.IsMatch(email);
    }
    

}
