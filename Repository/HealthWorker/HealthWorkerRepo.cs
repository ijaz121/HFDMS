using CommonServices.ListConverter;
using DAL;
using RequestModel.HealthFacility;
using RequestModel.HealthWorker;
using ResponseModel.BaseResponse;
using ResponseModel.HealthFacility;
using ResponseModel.HealthWorker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.HealthWorker
{
    public class HealthWorkerRepo : IHealthWorkerRepo
    {
        private IListConverter _listConverter;
        private IDbConnectionLogic _dbConnectionLogic;
        public HealthWorkerRepo(IListConverter listConverter, IDbConnectionLogic dbConnectionLogic)
        {
            _listConverter = listConverter;
            _dbConnectionLogic = dbConnectionLogic;
        }

        public async Task<ResponseResult<string>> InsertUpdateDeleteHealthWorker(HealthWorkerRequest request)
        {
            ResponseResult<string> responseResult = new ResponseResult<string>();

            try
            {
                string spName = @"SP_HFDMS_ManageHealthWorker";
                Hashtable Param = new Hashtable
                {
                    { "@ID", request.Id},
                    { "@Name", request.Name},
                    { "@Designation", request.Designation},
                    { "@Email", request.Email},
                    { "@PhoneNumber", request.PhoneNumber},
                    { "@HealthFacilityID", request.HealthFacilityId},
                    { "@Deleted", request.IsDeleted},
                    { "@CreatedBy", request.CreatedBy},
                    { "@ModifiedBy", request.ModifiedBy}
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
                        Data = response
                    };
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new ResponseResult<string>
                {
                    StatusCode = "02",
                    Message = "Internal Server Error",
                    Data = null
                };
                return responseResult;
            }
        }

        public async Task<ResponseResult<List<GetHealthWorkerResponse>>> GetHealthWorkerData(HealthWorkerRequest request)
        {
            ResponseResult<List<GetHealthWorkerResponse>> responseResult = new ResponseResult<List<GetHealthWorkerResponse>>();
            List<GetHealthWorkerResponse> healthWorkerResponse = new List<GetHealthWorkerResponse>();

            try
            {
                string Query = "SP_HFDMS_GetHealthWorkers";
                Hashtable Param = new Hashtable
                {
                    { "@ID", request.Id }
                };


                DataTable dt = await _dbConnectionLogic.ExecuteSelectQueryAsync(Query, Param);
                if (dt.Rows.Count > 0)
                {
                    healthWorkerResponse = _listConverter.ConvertDataTable<GetHealthWorkerResponse>(dt);

                    responseResult = new ResponseResult<List<GetHealthWorkerResponse>>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = healthWorkerResponse
                    };
                }
                else
                {
                    responseResult = new ResponseResult<List<GetHealthWorkerResponse>>
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

                responseResult = new ResponseResult<List<GetHealthWorkerResponse>>
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
