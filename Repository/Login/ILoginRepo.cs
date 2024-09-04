using RequestModel.Login;
using ResponseModel.BaseResponse;
using ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Login
{
    public interface ILoginRepo
    {
        Task<ResponseResult<LoginResponse>> Login(LoginRequest request);
    }
}
