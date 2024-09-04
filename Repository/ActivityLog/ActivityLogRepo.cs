using CommonServices.ListConverter;
using DAL;
using ResponseModel.Activity;
using ResponseModel.BaseResponse;
using ResponseModel.Dashboard;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ActivityLog
{
    public class ActivityLogRepo : IActivityLogRepo
    {
        private IListConverter _listConverter;
        private IDbConnectionLogic _dbConnectionLogic;
        public ActivityLogRepo(IListConverter listConverter, IDbConnectionLogic dbConnectionLogic)
        {
            _listConverter = listConverter;
            _dbConnectionLogic = dbConnectionLogic;
        }

        public async Task<ResponseResult<List<ActivityLogModel>>> GetActivityLogData(string RoleId)
        {
            ResponseResult<List<ActivityLogModel>> responseResult = new ResponseResult<List<ActivityLogModel>>();
            List<ActivityLogModel> healthWorkersPerReagion = new List<ActivityLogModel>();

            try
            {
                string Query = "SP_GetActivityLogData";
                Hashtable Param = new Hashtable 
                {
                    { "@RoleId", RoleId}
                };

                DataTable dt = await _dbConnectionLogic.ExecuteSelectQueryAsync(Query, Param);
                if (dt.Rows.Count > 0)
                {
                    healthWorkersPerReagion = _listConverter.ConvertDataTable<ActivityLogModel>(dt);

                    responseResult = new ResponseResult<List<ActivityLogModel>>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = healthWorkersPerReagion
                    };
                }
                else
                {
                    responseResult = new ResponseResult<List<ActivityLogModel>>
                    {
                        StatusCode = "09",
                        Message = "Unauthorized Access",
                        Data = null
                    };
                }

                return responseResult;
            }
            catch (Exception ex)
            {

                responseResult = new ResponseResult<List<ActivityLogModel>>
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
