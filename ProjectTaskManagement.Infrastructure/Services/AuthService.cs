using Microsoft.AspNetCore.Identity;
using ProjectTaskManagement.Core.Domain.Entities.Identity;
using ProjectTaskManagement.Core.DTOs.Auth;
using ProjectTaskManagement.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTaskManagement.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

     

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var email = registerDto.Email.Trim().ToLower();

            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser is not null)
            {
                throw new InvalidOperationException("Email is already in use");
            }

            var user = new ApplicationUser
            {
                FullName = registerDto.FullName.Trim(),
                Email = email,
                UserName = email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException(errors);
            }

            var expiration = DateTime.UtcNow.AddHours(1);

            var token = await _jwtService.GenerateTokenAsync(user, expiration);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = expiration,
                Email = user.Email!,
                FullName = user.FullName
            };
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var email = loginDto.Email.Trim().ToLower();

            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var expiration = DateTime.UtcNow.AddHours(1);

            var token = await _jwtService.GenerateTokenAsync(user, expiration);

            return new AuthResponseDto
            {
                Token = token,
                Expiration = expiration,
                Email = user.Email!,
                FullName = user.FullName
            };
        }
    }
}
