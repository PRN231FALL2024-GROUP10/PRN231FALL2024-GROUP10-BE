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
    }
}
