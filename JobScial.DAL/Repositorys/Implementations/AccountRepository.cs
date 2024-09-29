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

                /*var customerPhone = await _unitOfWork.CustomerDAO.GetCustomerByPhoneAsync(registerRequest.Phone);
                if (customerPhone != null)
                {
                    throw new BadRequestException("Phone already exist in the system.");
                }*/

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
