using CommonServices.ListConverter;
using DAL;
using RequestModel.Role;
using RequestModel.User;
using ResponseModel.BaseResponse;
using ResponseModel.Role;
using ResponseModel.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RoleManagement
{
    public class RoleManagementRepo : IRoleManagementRepo
    {
        private IListConverter _listConverter;
        private IDbConnectionLogic _dbConnectionLogic;
        public RoleManagementRepo(IListConverter listConverter, IDbConnectionLogic dbConnectionLogic)
        {
            _listConverter = listConverter;
            _dbConnectionLogic = dbConnectionLogic;
        }

        public async Task<ResponseResult<string>> InsertUpdateDeleteRole(RoleRequest request)
        {
            ResponseResult<string> responseResult = new ResponseResult<string>();

            try
            {
                string spName = @"SP_HFDMS_ManageRole";
                Hashtable Param = new Hashtable
                {
                    { "@RoleID", request.RoleId },
                    { "@RoleName", request.RoleName },
                    { "@Permission", request.Permissions },
                    { "@Deleted", request.IsDeleted},
                    { "@CreatedBy", request.CreatedBy },
                    { "@ModifiedBy", request.ModifiedBy},
                };

                string response = await _dbConnectionLogic.IUD(spName, Param);

                if (response != null && response.ToUpper().Equals("SUCCESS"))
                {
                    responseResult = new ResponseResult<string>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = null
                    };
                }
                else
                {
                    responseResult = new ResponseResult<string>
                    {
                        StatusCode = "01",
                        Message = "Failed",
                        Data = null
                    };
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                return responseResult;
            }
        }

        public async Task<ResponseResult<List<RoleResponse>>> GetRoleData(UserRequest request)
        {
            ResponseResult<List<RoleResponse>> responseResult = new ResponseResult<List<RoleResponse>>();
            List<RoleResponse> healthWorkerResponse = new List<RoleResponse>();

            try
            {
                Hashtable Param = new Hashtable
                {
                    { "@RoleID", request.UserID }
                };

                string Query = "SP_HFDMS_GetRoles";

                DataTable dt = await _dbConnectionLogic.ExecuteSelectQueryAsync(Query, Param);
                if (dt.Rows.Count > 0)
                {
                    healthWorkerResponse = _listConverter.ConvertDataTable<RoleResponse>(dt);

                    responseResult = new ResponseResult<List<RoleResponse>>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = healthWorkerResponse
                    };
                }
                else
                {
                    responseResult = new ResponseResult<List<RoleResponse>>
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
                responseResult = new ResponseResult<List<RoleResponse>>
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
