﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Security.JWT;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using T_HosCase.Context;
using T_HosCase.Entities;
using T_HosCase.Models;

namespace T_HosCase.Controllers
{
    public class UserController : Controller
    {
        private readonly Case_DbContext _context;
        private readonly ITokenService _tokenService;
		private readonly PasswordHasher<string> _passwordHasher;

		public UserController(Case_DbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDto model)
        {
			var userControl = _context.Users.Where(x=>x.Status==true && x.Email==model.Email).FirstOrDefault();
			if (userControl == null)
			{
				var user = new User();
				user.Status = true;
				user.Email = model.Email;
				user.Password = model.Password;
				user.FirstName = model.FirstName;
				user.LastName = model.LastName;
				user.HashPassword = HashPassword(model.Password);
				_context.Add(user);
				_context.SaveChanges();
                return Redirect("/User/Login");
            }
			return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDto model)
        {
			var user = _context.Users.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
			
			if (user is not null)
            {
				object userv = null;
				bool isPasswordValid = VerifyPassword(user.HashPassword, model.Password);
				if (isPasswordValid)
				{
					JwtModel jwtModel = new JwtModel();
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, user.FirstName),
						new Claim(ClaimTypes.Email, user.Email),
						new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
					};
					var accessToken = _tokenService.GenerateAccessToken(claims);
					var refreshToken = _tokenService.GenerateRefreshToken();
					jwtModel.AccessToken = accessToken;
					JwtSecurityTokenHandler tokenHandler = new();
					JwtSecurityToken? token = tokenHandler.ReadJwtToken(accessToken);
					if (token != null)
					{
						ClaimsIdentity identity = new ClaimsIdentity(token.Claims, JwtBearerDefaults.AuthenticationScheme);
						var authProps = new AuthenticationProperties
						{
							IsPersistent = true
						};
						HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProps);
						CookieOptions options = new CookieOptions
						{
							Expires = DateTime.Now.AddDays(2),
							MaxAge = TimeSpan.FromDays(2),
							HttpOnly = true
						};
						Response.Cookies.Append("security-token", accessToken.ToString(), options);
						return Redirect("/admin/Home/Index");
					}
				}
				return Redirect("/admin/User/Login");
			}
            return Redirect("/admin/User/Login");
        }
		public string HashPassword(string password)
		{

            var passwordHasher = new PasswordHasher<object>();

            // Kullanıcı nesnesi (anonim nesne veya kullanıcı sınıfı)
            object user = null; // Basit bir nesne oluşturabilirsin
            // Şifreyi hashlemek
            string hpassword = password;
            string hashedPassword = passwordHasher.HashPassword(user, password);
			return hashedPassword;

        }
		public bool VerifyPassword(string hashedPassword, string password)
		{
            var passwordHasher = new PasswordHasher<object>();
            object user = null;
            var result = passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
			return result == PasswordVerificationResult.Success;
		}
	}
}
