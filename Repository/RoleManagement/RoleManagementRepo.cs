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

        public async Task<ResponseResult<string>> MapRole(MapRoleRequest request)
        {
            ResponseResult<string> responseResult = new ResponseResult<string>();

            try
            {
                string spName = @"SP_HFDMS_AddRoleWithMapping";

                // Convert RoleMappings list to DataTable
                DataTable roleMappingsTable = GetRoleMappingsTable(request.roleMappings);

                // Prepare the parameters
                Hashtable Param = new Hashtable
                {
                    { "@RoleID", request.RoleId},
                    { "@RoleName", request.RoleName},
                    { "@Deleted", request.IsDeleted},
                    { "@UserName", request.UserName },
                    { "@RoleMappings", roleMappingsTable }
                };

                // Call the stored procedure
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
                // Handle any exceptions
                return new ResponseResult<string>
                {
                    StatusCode = "99",
                    Message = $"Error: {ex.Message}",
                    Data = null
                };
            }
        }

        
        public async Task<ResponseResult<List<MappedDataResponse>>> GetMappedData(MappedDataRequest request)
        {
            ResponseResult<List<MappedDataResponse>> responseResult = new ResponseResult<List<MappedDataResponse>>();
            List<MappedDataResponse> healthWorkerResponse = new List<MappedDataResponse>();
            try
            {
                string spName = @"SP_HFDMS_GetRoleActivities";

                // Prepare the parameters
                Hashtable Param = new Hashtable
                {
                    { "@RoleID", request.RoleId},
                    { "@UserId", request.UserId}
                };

                DataTable dt = await _dbConnectionLogic.ExecuteSelectQueryAsync(spName, Param);
                if (dt.Rows.Count > 0)
                {
                    healthWorkerResponse = _listConverter.ConvertDataTable<MappedDataResponse>(dt);

                    responseResult = new ResponseResult<List<MappedDataResponse>>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = healthWorkerResponse
                    };
                }
                else
                {
                    responseResult = new ResponseResult<List<MappedDataResponse>>
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
                responseResult = new ResponseResult<List<MappedDataResponse>>
                {
                    StatusCode = "02",
                    Message = "Internal Server Error",
                    Data = null
                };

                return responseResult;
            }
        }

        // Helper method to convert List<RoleMapping> to DataTable
        private DataTable GetRoleMappingsTable(List<RoleMapping> roleMappings)
        {
            // Create a new DataTable to store role mappings
            DataTable table = new DataTable();
            table.Columns.Add("ActivityId", typeof(int));
            table.Columns.Add("CanView", typeof(bool));
            table.Columns.Add("CanUpdate", typeof(bool));
            table.Columns.Add("CanDelete", typeof(bool));
            table.Columns.Add("CanCreate", typeof(bool));

            // Add the role mappings to the DataTable
            foreach (var mapping in roleMappings)
            {
                table.Rows.Add(mapping.ActivityId, mapping.CanView, mapping.CanUpdate, mapping.CanDelete, mapping.CanCreate);
            }

            return table;
        }


    }
}
