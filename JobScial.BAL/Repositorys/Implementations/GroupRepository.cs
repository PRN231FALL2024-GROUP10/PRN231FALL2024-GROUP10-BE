using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Group;
using JobScial.BAL.Repositorys.Interfaces;
using JobScial.DAL.Infrastructures;
using JobScial.DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
        public async Task<CommonResponse> DeleteGroupAsync(int groupId)
        {
            string CreateCommentSuccessedMsg = _config["ResponseMessages:CommonMsg:DeleteCommentSuccessedMsg"];

            CommonResponse commonResponse = new CommonResponse();
            try
            {
                var group = await _unitOfWork.groupDAO.GetGroupById(groupId);
                if (group == null)
                {
                    throw new Exception("comment do not exists ");
                }

                await _unitOfWork.groupDAO.DeleteGroup(group);
                await _unitOfWork.CommitAsync();

                commonResponse.Data = group;
                commonResponse.Status = 200;
                commonResponse.Message = CreateCommentSuccessedMsg;
                return commonResponse;
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 405;
            }
            return commonResponse;
        }

        public async Task<bool> BanMemberGroup(int groupId,int accountid)
        {
            var group = await _unitOfWork.groupDAO.GetGroupMemberById(groupId, accountid);

            if (group == null)
            {
                return false; // Return false if account not found
            }

            group.Status = -1;

            try
            {
                await _unitOfWork.groupDAO.BanMemberGroup(group);
                await _unitOfWork.CommitAsync();
                return true; // Successfully banned
            }
            catch
            {
                // Optionally log the error here (if you have a logging mechanism)
                return false; // Return false in case of failure
            }
        }

        public async Task<bool> ApproveGroupMember(int groupId,int accountid)
        {
            var group = await _unitOfWork.groupDAO.GetGroupMemberById(groupId,accountid);

            if (group == null)
            {
                return false; // Return false if account not found
            }

            group.Status = 1;

            try
            {
                await _unitOfWork.groupDAO.BanMemberGroup(group);
                await _unitOfWork.CommitAsync();
                return true; // Successfully banned
            }
            catch
            {
                // Optionally log the error here (if you have a logging mechanism)
                return false; // Return false in case of failure
            }
        }

        public async Task<CommonResponse> JoinGroupAsync(JoinGroupRequest joinGroupRequest)
        {
            string CreatePostSuccessedMsg = _config["ResponseMessages:CommonMsg:CreateGroupSuccessedMsg"];
            CommonResponse commonResponse = new CommonResponse();
            try
            {
                GroupMember group = new GroupMember
                {
                    JoinedOn = DateTime.Now,
                    Status = joinGroupRequest.Status,
                    AccountId = joinGroupRequest.AccountId,
                    GroupId = joinGroupRequest.GroupId,
                    GroupRoleId = joinGroupRequest.GroupRoleId,

                };
                await _unitOfWork.groupDAO.AddNewgroupMember(group);
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
