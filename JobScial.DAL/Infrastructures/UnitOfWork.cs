﻿using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.DAOs;
using JobScial.DAL.DAOs.Implements;
using JobScial.DAL.DAOs.Interfaces;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private JobSocialContext _dbContext;
        private AccountDAO _accountDAO;
        private PostDAO _postDAO;
        private CommentDAO _commentDAO;
        private AccountDao _accountDao;

        public UnitOfWork(IDbFactory dbFactory)
        {
            _dbContext = dbFactory.InitDbContext();
        }

        public AccountDao AccountDao
        {
            get
            {
                if (_accountDao == null)
                {
                    _accountDao = new AccountDao(_dbContext);
                }
                return _accountDao;
            }
        }
        public AccountDAO AccountDAO
        {
            get
            {
                if (_accountDAO == null)
                {
                    _accountDAO = new AccountDAO(_dbContext);
                }
                return _accountDAO;
            }
        }
        public CommentDAO CommentDAO
        {
            get
            {
                if (_commentDAO == null)
                {
                    _commentDAO = new CommentDAO(_dbContext);
                }
                return _commentDAO;
            }
        }
        public PostDAO PostDAO
        {
            get
            {
                if (_postDAO == null)
                {
                    _postDAO = new PostDAO(_dbContext);
                }
                return _postDAO;
            }
        }


        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
