using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public async Task<User> logIn(string userName, string password)
        {
            var user = await _context.users.FirstOrDefaultAsync(x => x.userName == userName);
            if (user == null) return null;
            if (!VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash)) return null;
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < ComputeHash.Length; i++)
                {
                    if (ComputeHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }


        public async Task<bool> UserExists(string userName)
        {
            var user = await _context.users.AnyAsync(x => x.userName == userName);
            return user;
        }
        private void CreatePassHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<IEnumerable<User>> GetUsers(){
            var users = await _context.users.ToListAsync();
            return users;
        }
    }
}