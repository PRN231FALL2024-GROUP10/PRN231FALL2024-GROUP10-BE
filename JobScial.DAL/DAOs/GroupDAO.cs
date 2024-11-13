using JobScial.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScial.DAL.DAOs
{
    public class GroupDAO
    {
        private JobSocialContext _dbContext;

        public GroupDAO(JobSocialContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddNewGroup(Group group)
        {
            try
            {
                await _dbContext.AddAsync(group);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task AddNewgroupMember(GroupMember groupMember)
        {
            try
            {
                await _dbContext.AddAsync(groupMember);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Group> GetGroupById(int id)
        {
            try
            {

                Group groups = await _dbContext.Groups
                    .SingleOrDefaultAsync(c => c.GroupId == id);
                return groups;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<GroupMember> GetGroupMemberById(int groupid, int accountid)
        {
            try
            {
                // Tìm thành viên nhóm dựa trên GroupId và AccountId
                GroupMember groupMember = await _dbContext.GroupMembers
                    .SingleOrDefaultAsync(gm => gm.GroupId == groupid && gm.AccountId == accountid);
                return groupMember;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving group member: " + ex.Message);
            }
        }

        #region DeletePost
        public async Task DeleteGroup(Group group)
        {
            try
            {
                this._dbContext.Groups.Remove(group);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Ban MemberGroup
        public async Task BanMemberGroup(GroupMember group)
        {
            try
            {
                _dbContext.Entry<GroupMember>(group).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
