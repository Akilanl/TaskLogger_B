using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SQLite;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using TaskLogger_B.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskLogger_B
{
    [Route("api/[controller]")]
    public class Token : Controller
    {
        public IConfiguration _config;
        SQLite.SQLiteConnection db;

        public Token(IConfiguration config)
        {
            _config = config;
            var databasePath = Path.GetFullPath("C:\\sqliteadmin\\test.s3db");
            db = new SQLiteConnection(databasePath);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        public string BuildToken(Authorization user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddYears(1),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Authorization Authenticate(LoginModel login)
        {
            Authorization user = null;
            var loginDetails = db.Query<Authorization>("select * from Authorization where UserName = ?", login.UserName);
            if (loginDetails != null)
                if (login.Password == loginDetails.FirstOrDefault().Password)
                    user = loginDetails.FirstOrDefault();
            return user;
        }
    }
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
