using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        IMapper mapper)
        : BaseApiController
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await userManager.FindByEmailFromClaimsPrinciple(User);
            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpGet("email-exists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await userManager.FindByEmailAsync(email) != null;
        }

        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await userManager.FindByEmailWithAddressAsync(User);
            return mapper.Map<Address, AddressDto>(user.Address);
        }

        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
        {
            var user = await userManager.FindByEmailWithAddressAsync(User);

            user.Address = mapper.Map<AddressDto, Address>(address);

            var result = await userManager.UpdateAsync(user);
            
            if (result.Succeeded) return Ok(mapper.Map<Address, AddressDto>(user.Address));

            return BadRequest("Problem updating the user");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ApiResponse(401));
            }

            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterDto(RegisterDto registerDto)
        {
            if(CheckEmailExistsAsync(registerDto.Email).Result.Value){
                return new BadRequestObjectResult(new ApiValidationErrorResponse{Errors = new []{"Email address is in use"}});
            }
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = tokenService.CreateToken(user),
                Email = user.Email
            };
        }
    }
}