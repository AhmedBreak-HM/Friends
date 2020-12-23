using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ZwajApp.API.Models;

namespace ZwajApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public Task<User> logIn(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] PasswordHash, PasswordSalt;
            CreatePassHash(password, out PasswordHash, out PasswordSalt);
            user.PasswordSalt = PasswordSalt;
            user.PasswordHash = PasswordHash;
           await _context.users.AddAsync(user);
           await _context.SaveChangesAsync();
           return user;
        }
        public Task<bool> UserExists(string userName)
        {
            throw new System.NotImplementedException();
        }
        private void CreatePassHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}