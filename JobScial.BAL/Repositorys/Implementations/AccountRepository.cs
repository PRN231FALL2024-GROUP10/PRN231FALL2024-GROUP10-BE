﻿using BMOS.BAL.Exceptions;
using BMOS.BAL.Helpers;
using BMOS.DAL.Enums;
using JobScial.BAL.DTOs.Accounts;
using JobScial.BAL.DTOs.Profile;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.BAL.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenZStyleAPP.BAL.Errors;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using JobScial.BAL.DTOs.Posts;
using JobScial.BAL.DTOs.FireBase;
using System.Collections;
using System.Security.Principal;

namespace JobScial.BAL.Repositorys.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _config;
        private UnitOfWork _unitOfWork;
        //private IMapper _mapper;
        public AccountRepository(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            this._config = configuration;

        }
        private async Task<string> SaveFile(IFormFile file)
        {
            // Đường dẫn tới thư mục Uploads
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            // Kiểm tra và tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Tạo tên file duy nhất cho file tải lên
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // Đường dẫn đầy đủ để lưu file
            var filePath = Path.Combine(uploadPath, fileName);

            // Lưu file vào thư mục
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về đường dẫn tương đối hoặc đường dẫn đầy đủ
            return $"/Uploads/{fileName}";
        }
        private List<AccountCertificateDto> GetAccountCertificate(Account account)
        {
            var certificates = _unitOfWork.AccountCertificateDao.Find(cert => cert.AccountId == account.AccountId)
           .Select(cert => new AccountCertificateDto
           {
               Index = cert.Index,
               Link = cert.Link,
           }).ToList();
            return certificates;
        }
        private List<AccountSkillDto> GetAccountSkill(Account account)
        {
            var skills = _unitOfWork.AccountSkillDao.Find(skill => skill.AccountId == account.AccountId)
            .Select(skill => new AccountSkillDto
            {
                AccountId = skill.AccountId,
                SkillCategoryId = skill.SkillCategoryId,
                SkillLevel = skill.SkillLevel,
                Timespan = skill.Timespan,
                TimespanUnit = skill.TimespanUnit,
                Description = skill.Description,
            }).ToList();

            return skills;
        }
        private List<AccountEducationDto> GetAccountEducation(Account account)
        {
            var educations = _unitOfWork.AccountEducationDao
                .Find(edu => edu.AccountId == account.AccountId)
                .Include(e => e.School)
                .Select(edu => new AccountEducationDto
                {
                    AccountId = edu.AccountId,
                    SchoolId = edu.SchoolId,
                    YearStart = edu.YearStart,
                    Timespan = edu.Timespan,
                    TimespanUnit = edu.TimespanUnit,
                    Description = edu.Description,
                    SchoolName = edu.School.Name
                }).ToList();

            return educations;
        }
        private List<AccountExperienceDto> GetAccountExperience(Account account)
        {
            var experiences = _unitOfWork.AccountExperienceDao.Find(exp => exp.AccountId == account.AccountId)
                .Include(e => e.TimespanUnitNavigation)
                .Include(e => e.Company) 
                .Select(exp => new AccountExperienceDto
                {
                    AccountId = exp.AccountId,
                    CompanyId = exp.CompanyId,
                    YearStart = exp.YearStart,
                    JobTitle = exp.JobTitle,
                    Timespan = exp.Timespan,
                    TimespanUnit = exp.TimespanUnit,
                    Description = exp.Description,
                    TimespanUnitName = exp.TimespanUnitNavigation != null ? exp.TimespanUnitNavigation.Name : null,
                    CompanyName = exp.Company != null ? exp.Company.Name : null
                }).ToList();

            return experiences;
        }
        public async Task<AccountProfileDto> UpdateProfile(string email, UpdateProfileDto profile)
        {
            var account = _unitOfWork.AccountDao.FindOne(o => o.Email == email);

            if (account == null)
            {
                throw new Exception($"Account with Email {email} not found.");
            }

            // Update Account Details
            account.FullName = profile.Account.FullName;
/*            account.Image = profile.Account.Image;
            account.DoB = profile.Account.DoB;
            account.Gender = profile.Account.Gender;*/

            // Update Certificates
            _unitOfWork.AccountCertificateDao.DeleteMany(cert => cert.AccountId == account.AccountId);
            foreach (var cert in profile.Certificates)
            {
                _unitOfWork.AccountCertificateDao.Add(new AccountCertificate
                {
                    AccountId = account.AccountId,
                    Index = cert.Index,
                    Link = cert.Link
                });
            }

            // Update Skills
            _unitOfWork.AccountSkillDao.DeleteMany(skill => skill.AccountId == account.AccountId);
            foreach (var skill in profile.Skills)
            {
                _unitOfWork.AccountSkillDao.Add(new AccountSkill
                {
                    AccountId = account.AccountId,
                    SkillCategoryId = skill.SkillCategoryId,
                    SkillLevel = skill.SkillLevel,
                    Timespan = skill.Timespan,
                    TimespanUnit = skill.TimespanUnit,
                    Description = skill.Description
                });
            }

            // Update Educations
            _unitOfWork.AccountEducationDao.DeleteMany(edu => edu.AccountId == account.AccountId);
            foreach (var edu in profile.Educations)
            {
                _unitOfWork.AccountEducationDao.Add(new AccountEducation
                {
                    AccountId = account.AccountId,
                    SchoolId = edu.SchoolId,
                    YearStart = edu.YearStart,
                    Timespan = edu.Timespan,
                    TimespanUnit = edu.TimespanUnit,
                    Description = edu.Description
                });
            }

            // Update Experiences
            _unitOfWork.AccountExperienceDao.DeleteMany(exp => exp.AccountId == account.AccountId);
            foreach (var exp in profile.Experiences)
            {
                _unitOfWork.AccountExperienceDao.Add(new AccountExperience
                {
                    AccountId = account.AccountId,
                    CompanyId = exp.CompanyId,
                    YearStart = exp.YearStart,
                    JobTitle = exp.JobTitle,
                    Timespan = exp.Timespan,
                    TimespanUnit = exp.TimespanUnit,
                    Description = exp.Description
                });
            }

            // Commit the transaction
            await _unitOfWork.CommitAsync();

            return await GetProfileByEmail(email);
        }
        public async Task<AccountProfileDto> GetProfileByEmail(string email)
        {
            var account = _unitOfWork.AccountDao.FindOne(acc => acc.Email == email);
            if (account == null)
            {
                throw new Exception($"Account with Email {email} not found.");
            }

            var accountDto = new AccountDto
            {
                AccountId = account.AccountId,
                Email = account.Email,
                FullName = account.FullName,
                Username = account.FullNameSearch,
                Image = account.Image,
                FollowerCount = _unitOfWork.followDAO.GetAll().Where(x => x.FollowedAccountId == account.AccountId).Count(),
                FollowingCount = _unitOfWork.followDAO.GetAll().Where(x => x.AccountId == account.AccountId).Count()
            };


            return new AccountProfileDto
            {
                Account = accountDto,
                Certificates = GetAccountCertificate(account),
                Skills = GetAccountSkill(account),
                Educations = GetAccountEducation(account),
                Experiences = GetAccountExperience(account)
            };
        }

        public async Task<AccountProfileDto> GetProfileById(int accountId, HttpContext httpContext)
        {
            string emailFromClaim = "";
            try
            {
                JwtSecurityToken jwtSecurityToken = TokenHelper.ReadToken(httpContext);
                emailFromClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;

            }
            catch (Exception ex)
            {

            }

            var accountStaff = await _unitOfWork.AccountDAO.GetAccountByEmail(emailFromClaim);
            var followList = _unitOfWork.followDAO.GetAll().ToList();
            var account = _unitOfWork.AccountDao.FindOne(acc => acc.AccountId == accountId);
            if (account == null)
            {
                throw new Exception($"Account with ID {accountId} not found.");
            }

            // Chuyển đổi tài khoản sang DTO
            var accountDto = new AccountDto
            {
                AccountId = account.AccountId,
                Email = account.Email,
                FullName = account.FullName,
                Username = account.FullNameSearch,
                Image = account.Image,
                FollowerCount = followList.Where(x => x.FollowedAccountId == account.AccountId).Count(),
                FollowingCount = followList.Where(x => x.AccountId == account.AccountId).Count(),
            };

            if (accountStaff != null)
            {
                accountDto.IsFollowing = followList.Where(x => x.AccountId == account.AccountId && x.FollowedAccountId == accountStaff.AccountId).FirstOrDefault() != null ? true : false;
                accountDto.IsFollowed = followList.Where(x => x.AccountId == accountStaff.AccountId && x.FollowedAccountId == account.AccountId).FirstOrDefault() != null ? true : false;
            }

            // Tạo và trả về DTO
            return new AccountProfileDto
            {
                Account = accountDto,
                Certificates = GetAccountCertificate(account),
                Skills = GetAccountSkill(account),
                Educations = GetAccountEducation(account),
                Experiences = GetAccountExperience(account)
            };
        }

        #region Register
        public async Task<GetAccountResponse> Register(RegisterRequest registerRequest)
        {
            try
            {
                //var role = await _unitOfWork.RoleDAO.GetRoleAsync((int)RoleEnum.Role.CUSTOMER);
                var customerByEmail = await _unitOfWork.AccountDAO.GetAccountByEmail(registerRequest.Email);
                if (customerByEmail != null)
                {
                    throw new BadRequestException("Email already exist in the system.");
                }
                // assign registerRequest to account
                Account account = new Account
                {
                    Email = registerRequest.Email,
                    Password = registerRequest.Password,
                    Role = 3,
                    FullName = registerRequest.FullName,
                    Gender = registerRequest.Gender,
                    DoB = registerRequest.DoB,
                    CreatedOn = DateTime.Now,
                    Image = "https://th.bing.com/th/id/OIP.VwIICGYTWhTPKuBRbnVLdgHaHa?w=980&h=980&rs=1&pid=ImgDetMain",
                    FullNameSearch = registerRequest.FullNameSearch,

                };

                AccountCertificate accountCertificate = new AccountCertificate
                {

                };
                await _unitOfWork.AccountDAO.AddNewAccount(account);

                //Save to Database
                await _unitOfWork.CommitAsync();

                return new GetAccountResponse
                {
                    AccountId = account.AccountId,
                    Email = account.Email,
                    Password = account.Password,
                    Role = account.Role,
                    CreatedOn = DateTime.Now,
                    FullName = account.FullName,
                    FullNameSearch = account.FullNameSearch,
                    DoB = account.DoB,
                    Image = account.Image,
                    Gender = account.Gender,
                    //Account = _mapper.Map<GetAccountResponse>(customer.Account),
                };
            }
            catch (BadRequestException ex)
            {
                string fieldNameError = "";
                if (ex.Message.ToLower().Contains("email"))
                {
                    fieldNameError = "Email";
                }
                else if (ex.Message.ToLower().Contains("phone"))
                {
                    fieldNameError = "Phone";
                }
                string error = ErrorHelper.GetErrorString(fieldNameError, ex.Message);
                throw new BadRequestException(error);
            }
            catch (Exception ex)
            {
                string error = ErrorHelper.GetErrorString(ex.Message);
                throw new Exception(error);
            }
        }
        #endregion

        public async Task DeleteAccount(int accountId)
        {
            var account = _unitOfWork.AccountDao.FindOne(a => a.AccountId == accountId);

            if (account == null)
            {
                throw new Exception($"Account with ID {accountId} not found.");
            }

            account.Role = -1;

            await _unitOfWork.AccountDAO.BanAccount(account);

            _unitOfWork.Commit();
        }

        public async Task<List<AccountDto>> Get(HttpContext httpContext)
        {
            string emailFromClaim = "";
            try
            {
                JwtSecurityToken jwtSecurityToken = TokenHelper.ReadToken(httpContext);
                emailFromClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;

            }
            catch (Exception ex)
            {

            }

            var accountStaff = await _unitOfWork.AccountDAO.GetAccountByEmail(emailFromClaim);
            List<AccountDto> list = new List<AccountDto>();
            var accounts = _unitOfWork.AccountDao.GetAll();
            var followList = _unitOfWork.followDAO.GetAll().ToList();
            foreach (var account in accounts)
            {
                if(account.Role != 0)
                {
                    var accountDto = new AccountDto
                    {
                        AccountId = account.AccountId,
                        Email = account.Email,
                        FullName = account.FullName,
                        Username = account.FullNameSearch,
                        Image = account.Image,
                        Role = (int)account.Role,
                        FollowerCount = followList.Where(x => x.FollowedAccountId == account.AccountId).Count()
                    };

                    if (accountStaff != null)
                    {
                        accountDto.IsFollowing = followList.Where(x => x.AccountId == account.AccountId && x.FollowedAccountId == accountStaff.AccountId).FirstOrDefault() != null ? true : false;
                        accountDto.IsFollowed = followList.Where(x => x.AccountId == accountStaff.AccountId && x.FollowedAccountId == account.AccountId).FirstOrDefault() != null ? true : false;
                    }

                    list.Add(accountDto);
                }
                
            }

            return list;
            
        }

        public async Task<List<AccountDto>> AdminGet()
        {
            List<AccountDto> list = new List<AccountDto>();
            var accounts = _unitOfWork.AccountDao.GetAll().Where(x => x.Role != 2);

            foreach (var account in accounts)
            {

                var accountDto = new AccountDto
                {
                    AccountId = account.AccountId,
                    Email = account.Email,
                    FullName = account.FullName,
                    Username = account.FullNameSearch,
                    Role = (int)account.Role
                };

                list.Add(accountDto);


            }

            return list;

        }

        public async Task<bool> BanAccount(int accountId)
        {
            var account = await _unitOfWork.AccountDAO.GetAccountById(accountId);

            if (account == null)
            {
                return false; // Return false if account not found
            }

            account.Role = 0;

            try
            {
                await _unitOfWork.AccountDAO.BanAccount(account);
                await _unitOfWork.CommitAsync();
                return true; // Successfully banned
            }
            catch
            {
                // Optionally log the error here (if you have a logging mechanism)
                return false; // Return false in case of failure
            }
        }
        public async Task<bool> UnlockAccount(int accountId)
        {
            var account = await _unitOfWork.AccountDAO.GetAccountById(accountId);

            if (account == null)
            {
                return false; // Return false if account not found
            }

            account.Role = 1;

            try
            {
                await _unitOfWork.AccountDAO.BanAccount(account);
                await _unitOfWork.CommitAsync();
                return true; // Successfully banned
            }
            catch
            {
                // Optionally log the error here (if you have a logging mechanism)
                return false; // Return false in case of failure
            }
        }
        public async Task<CommonResponse> ConnectAccount(int id, HttpContext httpContext)
        {
            string loginSuccessMsg = _config["ResponseMessages:AuthenticationMsg:UnauthenticationMsg"];
            string CreatePostSuccessedMsg = _config["ResponseMessages:CommonMsg:CreatePostSuccessedMsg"];
            string NotCreateSuccessMsg = _config["ResponseMessages:RolePermissionMsg:NotCreateSuccessMsg"];
            CommonResponse commonResponse = new CommonResponse();

            try
            {
                JwtSecurityToken jwtSecurityToken = TokenHelper.ReadToken(httpContext);
                string emailFromClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                var accountStaff = await _unitOfWork.AccountDAO.GetAccountByEmail(emailFromClaim);


                var checkExist = _unitOfWork.followDAO.GetAll().Where(x => x.AccountId == accountStaff.AccountId && x.FollowedAccountId == id).FirstOrDefault();
                if (checkExist != null)
                {
                    _unitOfWork.followDAO.Delete(checkExist);
                }
                else
                {
                    Connection follow = new Connection();
                    follow.FollowedAccountId = id;
                    follow.AccountId = accountStaff.AccountId;
                    follow.CreatedOn = DateTime.Now;
                    _unitOfWork.followDAO.Add(follow);
                }

                await _unitOfWork.CommitAsync();

                commonResponse.Status = 200;
                commonResponse.Message = CreatePostSuccessedMsg;
                return commonResponse;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 500; // Internal Server Error
            }
            return commonResponse;
        }

        public async Task<CommonResponse> ChangeImageAsync(CreatePostRequest post, HttpContext httpContext, FireBaseImage fireBaseImage)
        {
            string loginSuccessMsg = _config["ResponseMessages:AuthenticationMsg:UnauthenticationMsg"];
            string CreatePostSuccessedMsg = _config["ResponseMessages:CommonMsg:CreatePostSuccessedMsg"];
            string NotCreateSuccessMsg = _config["ResponseMessages:RolePermissionMsg:NotCreateSuccessMsg"];
            CommonResponse commonResponse = new CommonResponse();

            try
            {
                FileHelper.SetCredentials(fireBaseImage);
                FileStream fileStream = FileHelper.ConvertFormFileToStream(post.Link[0]);
                Tuple<string, string> result = await FileHelper.UploadImage(fileStream, "Post");

                JwtSecurityToken jwtSecurityToken = TokenHelper.ReadToken(httpContext);
                string emailFromClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;
                var account = await _unitOfWork.AccountDAO.GetAccountByEmail(emailFromClaim);

                account.Image = result.Item1;
                
                await _unitOfWork.AccountDAO.UpdateImage(account);

                _unitOfWork.Commit();
                commonResponse.Status = 200;
                commonResponse.Message = CreatePostSuccessedMsg;
                return commonResponse;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 500; // Internal Server Error
            }
            return commonResponse;

        }

        public async Task<CommonResponse> UpdateAccount(int accountId, UpdateAccountRequest updateAccountRequest)
        {
            string UploadImageSuccessedMsg = _config["ResponseMessages:CommonMsg:UploadImageSuccessedMsg"];

            CommonResponse commonResponse = new CommonResponse();
            var account = await _unitOfWork.AccountDAO.GetAccountById(accountId);
            if (account == null)
            {
                throw new NotFoundException("account does not exist in system");
            }

            account.FullName = updateAccountRequest.FullName;
            account.FullNameSearch = updateAccountRequest.FullNameSearch;
            account.Gender = updateAccountRequest.Gender;
            account.Password = updateAccountRequest.Password;
            account.DoB = updateAccountRequest.DoB;
            account.Email = updateAccountRequest.Email;


            await _unitOfWork.AccountDAO.UpdateAccount(account);
            await this._unitOfWork.CommitAsync();

            commonResponse.Data = account;
            commonResponse.Status = 200;
            commonResponse.Message = "Update Account Successfully";
            return commonResponse;
        }

        public async Task<List<AccountDto>> GetByProfileFollow(int accountId, HttpContext httpContext)
        {
            List<int> listAccountPosts = new List<int>();

            var lst = (_unitOfWork.followDAO.GetAll()).Where(x => x.AccountId == accountId);
            //var lst = (await _unitOfWork.PostDAO.GetPosts()).Where(x => x.CreatedBy == accountId);

            listAccountPosts = (from s in lst select s.FollowedAccountId).ToList();

            var lstByHostID = await Get(httpContext);
            var result = lstByHostID.Where(b => listAccountPosts.Any(a => a == b.AccountId)).ToList();

            return result;
        }

        public async Task<bool> ConfirmAccount(int accountId)
        {
            var account = await _unitOfWork.AccountDAO.GetAccountById(accountId);

            if (account == null)
            {
                return false; // Return false if account not found
            }

            account.Role = 1;

            try
            {
                await _unitOfWork.AccountDAO.BanAccount(account);
                await _unitOfWork.CommitAsync();
                return true; // Successfully banned
            }
            catch
            {
                // Optionally log the error here (if you have a logging mechanism)
                return false; // Return false in case of failure
            }
        }
    }
}
