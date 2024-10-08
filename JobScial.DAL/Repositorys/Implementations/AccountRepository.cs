using BMOS.BAL.Exceptions;
using BMOS.BAL.Helpers;
using BMOS.DAL.Enums;
using JobScial.BAL.DTOs.Accounts;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using JobScial.DAL.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.Repositorys.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private UnitOfWork _unitOfWork;
        //private IMapper _mapper;
        public AccountRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;

        }

        public async Task<AccountProfileDto> GetProfileById(int accountId)
        {
            // Lấy tài khoản
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
                // Thêm các thuộc tính khác
            };
/*
            // Lấy và chuyển đổi chứng chỉ
            var certificates = _unitOfWork.AccountCertificateDao.Find(cert => cert.AccountId == accountId)
                .Select(cert => new AccountCertificateDto
                {
                    CertificateName = cert.CertificateName,
                    // Thêm các thuộc tính khác
                });

            // Lấy và chuyển đổi thông tin giáo dục
            var educations = _unitOfWork.AccountEducationDao.Find(edu => edu.AccountId == accountId)
                .Select(edu => new AccountEducationDto
                {
                    EducationId = edu.EducationId,
                    InstitutionName = edu.InstitutionName,
                    Degree = edu.Degree,
                    // Thêm các thuộc tính khác
                });

            // Lấy và chuyển đổi kinh nghiệm
            var experiences = _unitOfWork.AccountExperienceDao.Find(exp => exp.AccountId == accountId)
                .Select(exp => new AccountExperienceDto
                {
                    ExperienceId = exp.ExperienceId,
                    JobTitle = exp.JobTitle,
                    CompanyName = exp.CompanyName,
                    // Thêm các thuộc tính khác
                });

            // Lấy và chuyển đổi kỹ năng
            var skills = _unitOfWork.AccountSkillDao.Find(skill => skill.AccountId == accountId)
                .Select(skill => new AccountSkillDto
                {
                    SkillId = skill.SkillId,
                    SkillName = skill.SkillName,
                    // Thêm các thuộc tính khác
                });*/

            // Tạo và trả về DTO
            return new AccountProfileDto
            {
                Account = accountDto,
       /*         Certificates = certificates,
                Educations = educations,
                Experiences = experiences,
                Skills = skills*/
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
                    Image = registerRequest.Image,
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
    }
}
