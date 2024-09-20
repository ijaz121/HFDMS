using CommonServices.ListConverter;
using DAL;
using RequestModel.Patient;
using RequestModel.User;
using ResponseModel.BaseResponse;
using ResponseModel.Patient;
using ResponseModel.User;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UserManagement
{
    public class UserManagementRepo : IUserManagementRepo
    {
        private IListConverter _listConverter;
        private IDbConnectionLogic _dbConnectionLogic;
        public UserManagementRepo(IListConverter listConverter, IDbConnectionLogic dbConnectionLogic)
        {
            _listConverter = listConverter;
            _dbConnectionLogic = dbConnectionLogic;
        }

        public async Task<ResponseResult<string>> InsertUpdateDeleteUser(UserRequest request)
        {
            ResponseResult<string> responseResult = new ResponseResult<string>();

            try
            {
                string spName = @"SP_HFDMS_ManageUser";
                Hashtable Param = new Hashtable
                {
                    { "@UserID", request.UserID },
                    { "@Name", request.Name },
                    { "@Email", request.Email },
                    { "@Address", request.Address },
                    { "@PhoneNumber", request.PhoneNumber},
                    { "@RoleID", request.Role },
                    { "@AssignedHealthFacilityID", request.HealthFacilityId },
                    { "@Deleted", request.IsDeleted },
                    { "@CreatedBy", request.CreatedBy },
                    { "@ModifiedBy", request.ModifiedBy },
                };

                string response = await _dbConnectionLogic.IUD(spName, Param);

                if (response != null && response.ToUpper().Equals("SUCCESS"))
                {
                    responseResult = new ResponseResult<string>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = response
                    };
                }
                else
                {
                    responseResult = new ResponseResult<string>
                    {
                        StatusCode = "01",
                        Message = "Failed",
                        Data = response
                    };
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                return responseResult;
            }
        }

        public async Task<ResponseResult<List<UserResponse>>> GetUserData(UserRequest request)
        {
            ResponseResult<List<UserResponse>> responseResult = new ResponseResult<List<UserResponse>>();
            List<UserResponse> healthWorkerResponse = new List<UserResponse>();

            try
            {
                Hashtable Param = new Hashtable
                {
                    { "@UserID", request.UserID }
                };

                string Query = "SP_HFDMS_GetUsers";

                DataTable dt = await _dbConnectionLogic.ExecuteSelectQueryAsync(Query, Param);
                if (dt.Rows.Count > 0)
                {
                    healthWorkerResponse = _listConverter.ConvertDataTable<UserResponse>(dt);

                    responseResult = new ResponseResult<List<UserResponse>>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = healthWorkerResponse
                    };
                }
                else
                {
                    responseResult = new ResponseResult<List<UserResponse>>
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

                responseResult = new ResponseResult<List<UserResponse>>
                {
                    StatusCode = "02",
                    Message = "Internal Server Error",
                    Data = null
                };

                return responseResult;
            }
        }

        public async Task<ResponseResult<DropdownData>> RoleHealthFacilityDropdownData()
        {
            ResponseResult<DropdownData> responseResult = new ResponseResult<DropdownData>();
            DropdownData dropdownData = new DropdownData();

            try
            {
                Hashtable Param = new Hashtable{};

                string Query = "SP_HFDMS_GetRolesAndHealthFacilities";

                DataSet ds = await _dbConnectionLogic.ExecuteSelectQueryToDataSetAsync(Query, Param);
                if (ds.Tables.Count > 0)
                {
                    dropdownData.roleDropdown = _listConverter.ConvertDataTable<RoleDropdown>(ds.Tables[0]);
                    dropdownData.healthFacilityDropdown = _listConverter.ConvertDataTable<HealthFacilityDropdown>(ds.Tables[1]);

                    responseResult = new ResponseResult<DropdownData>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = dropdownData
                    };
                }
                else
                {
                    responseResult = new ResponseResult<DropdownData>
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

                responseResult = new ResponseResult<DropdownData>
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
