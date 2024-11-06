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
        }        public async Task<Account> GetAccountById(int id)
        {

            try
            {
                return await this._dbContext.Accounts
                    .SingleOrDefaultAsync(a => a.AccountId.Equals(id));
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
        public async Task BanAccount(Account account)
        {
            try
            {
                _dbContext.Accounts.Attach(account);

                _dbContext.Entry(account).Property(a => a.Role).IsModified = true;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #region Update Account
        public async Task UpdateAccount(Account account)
        {
            try
            {
                _dbContext.Entry<Account>(account).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        public async Task UpdateImage(Account account)
        {
            try
            {
                _dbContext.Entry(account).Property(a => a.Image).IsModified = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


}