using CommonServices.ListConverter;
using DAL;
using RequestModel.HealthWorker;
using RequestModel.Login;
using ResponseModel;
using ResponseModel.BaseResponse;
using ResponseModel.HealthWorker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Login
{
    public class LoginRepo : ILoginRepo
    {
        private IListConverter _listConverter;
        private IDbConnectionLogic _dbConnectionLogic;
        public LoginRepo(IListConverter listConverter, IDbConnectionLogic dbConnectionLogic)
        {
            _listConverter = listConverter;
            _dbConnectionLogic = dbConnectionLogic;
        }
        
        public async Task<ResponseResult<LoginResponse>> Login(LoginRequest request)
        {
            ResponseResult<LoginResponse> responseResult = new ResponseResult<LoginResponse>();
            LoginResponse? loginResponse = new LoginResponse();

            try
            {
                string query = "SP_HFDMS_UserLogin";
                Hashtable param = new Hashtable
                {
                    { "@Email", request.Email },
                    { "@Password", request.Password }
                };

                DataSet ds = await _dbConnectionLogic.ExecuteSelectQueryToDataSetAsync(query, param);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        loginResponse = _listConverter.ConvertDataTable<LoginResponse>(ds.Tables[0]).FirstOrDefault();
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        loginResponse.Permission = _listConverter.ConvertDataTable<Permissions>(ds.Tables[1]);
                    }

                    responseResult = new ResponseResult<LoginResponse>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = loginResponse
                    };
                }
                else
                {
                    responseResult = new ResponseResult<LoginResponse>
                    {
                        StatusCode = "04",
                        Message = "No Record Found",
                        Data = null
                    };
                }

                return responseResult;
            }
            catch (Exception ex)
            {
                // Log the exception here if necessary

                responseResult = new ResponseResult<LoginResponse>
                {
                    StatusCode = "02",
                    Message = "Internal Server Error",
                    Data = null
                };

                return responseResult;
            }
        }


    }
}
