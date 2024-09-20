using RequestModel.Role;
using RequestModel.User;
using ResponseModel.BaseResponse;
using ResponseModel.Role;
using ResponseModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RoleManagement
{
    public interface IRoleManagementRepo
    {
        Task<ResponseResult<string>> InsertUpdateDeleteRole(RoleRequest request);

        Task<ResponseResult<List<RoleResponse>>> GetRoleData(UserRequest request);

        Task<ResponseResult<string>> MapRole(MapRoleRequest request);

        Task<ResponseResult<List<MappedDataResponse>>> GetMappedData(MappedDataRequest request);
    }
}
