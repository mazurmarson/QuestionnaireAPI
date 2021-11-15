using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuestionnaireAPI.Context;
using QuestionnaireAPI.Dtos;
using QuestionnaireAPI.Exceptions;
using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Repos
{
    public class UserRepo : GenRepo, IUserRepo
    {
        private readonly QuestionnaireDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;

        public UserRepo(QuestionnaireDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings):base(context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            
        }


        public async Task Register(RegisterUserDto registerUserDto)
        {
            bool nameExist = await _context.Users.AnyAsync(x => x.Name == registerUserDto.Name);
            if(nameExist)
            {
                throw new ResourceDoesExistException("This name is already in use");
            }
            
            bool mailExist = await _context.Users.AnyAsync(x => x.Mail == registerUserDto.Mail);
            if(mailExist)
            {
                throw new ResourceDoesExistException("This mail is already in use");
            }
            var user = new User()
            {
                Name = registerUserDto.Name,
                Mail = registerUserDto.Mail,
                DateOfBirth = registerUserDto.DateOfBirth
            };
            var password = registerUserDto.Password;
            var hashedPassword = _passwordHasher.HashPassword(user, password);
            user.PasswordHash = hashedPassword;
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

           
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

                public async Task<string> GenerateJwt(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Mail == dto.Mail);

            if(user is null)
            {
                throw new NotFoundException("Inavalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new NotFoundException("Inavalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name}"),
                //Tu bedzie można dodać role
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //     if(user is null)
        //     {
        //         throw new BadReqyuest
        //     }
        // }
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials:cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

    }
}