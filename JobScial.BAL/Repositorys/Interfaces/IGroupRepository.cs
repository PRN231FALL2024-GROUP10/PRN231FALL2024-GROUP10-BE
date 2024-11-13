using GenZStyleAPP.BAL.Errors;
using JobScial.BAL.DTOs.Comments;
using JobScial.BAL.DTOs.Group;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.BAL.Repositorys.Interfaces
{
    public interface IGroupRepository
    {
        Task<CommonResponse> AddGroupAsync(AddGroupRequest groupRequest);
        Task<CommonResponse> JoinGroupAsync(JoinGroupRequest joinGroupRequest);
        public Task<CommonResponse> DeleteGroupAsync(int groupId);
        public Task<bool> BanMemberGroup(int groupId, int accountid);
        public Task<bool> ApproveGroupMember(int groupId,int accountid);
    }
}
