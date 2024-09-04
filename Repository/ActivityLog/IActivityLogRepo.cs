using ResponseModel.Activity;
using ResponseModel.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ActivityLog
{
    public interface IActivityLogRepo
    {
        Task<ResponseResult<List<ActivityLogModel>>> GetActivityLogData(string RoleId);
    }
}
