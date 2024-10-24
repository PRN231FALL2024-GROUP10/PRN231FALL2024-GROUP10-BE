using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Group;
using JobScial.BAL.Repositorys.Interfaces;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Implementations
{
    public class GroupRepository : IGroupRepository
    {
        private UnitOfWork _unitOfWork;
        //private IMapper _mapper;
        private readonly IConfiguration _config;

        public GroupRepository(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this._unitOfWork = (UnitOfWork)unitOfWork;
            //this._mapper = mapper;
            this._config = configuration;
        }
        public async Task<CommonResponse> AddGroupAsync(AddGroupRequest groupRequest)
        {
            string CreatePostSuccessedMsg = _config["ResponseMessages:CommonMsg:CreateGroupSuccessedMsg"];
            CommonResponse commonResponse = new CommonResponse();
            try 
            {
                Group group = new Group
                {
                    CreatedOn = DateTime.Now,
                    Status = groupRequest.Status,
                    CompanyId = groupRequest.CompanyId,
                    GroupName = groupRequest.GroupName,
                    IsVerified = groupRequest.IsVerified,

                };
                await _unitOfWork.groupDAO.AddNewGroup(group);
                await _unitOfWork.CommitAsync();

                commonResponse.Data = group;
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
    }
}
