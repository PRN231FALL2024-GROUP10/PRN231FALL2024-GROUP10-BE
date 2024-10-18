using JobScial.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.DAOs
{
    public class AccountDAO
    {
        private JobSocialContext _dbContext;

        public AccountDAO(JobSocialContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Account> GetAccountByEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                return await _dbContext.Accounts
                                       .SingleOrDefaultAsync(a => a.Email.Equals(email.Trim().ToLower())
                                                               && a.Password.Equals(password));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<Account> GetAccountByEmail(string email)
        {

            try
            {
                return await this._dbContext.Accounts
                    .SingleOrDefaultAsync(a => a.Email.Equals(email));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Account> GetAccountByName(string name)
        {

            try
            {
                return await this._dbContext.Accounts
                    .FirstOrDefaultAsync(a => a.FullName.Contains(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddNewAccount(Account account)
        {
            try
            {
                await _dbContext.AddAsync(account);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}

