using RequestModel.User;
using ResponseModel.BaseResponse;
using ResponseModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserManagement
{
    public interface IUserManagementRepo
    {
        Task<ResponseResult<string>> InsertUpdateDeleteUser(UserRequest request);
        
        Task<ResponseResult<List<UserResponse>>> GetUserData(UserRequest request);

        Task<ResponseResult<DropdownData>> RoleHealthFacilityDropdownData();
    }
}
