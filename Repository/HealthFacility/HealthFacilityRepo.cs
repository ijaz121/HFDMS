using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ResponseModel.BaseResponse;
using RequestModel.HealthFacility;
using ResponseModel.HealthFacility;
using System.Collections;
using System.Data;
using CommonServices.ListConverter;
using DAL;
using Newtonsoft.Json;
using System.Reflection.Metadata;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Reflection.Emit;

namespace Repository.HealthFacility
{
    public class HealthFacilityRepo : IHealthFacilityRepo
    {
        private IListConverter _listConverter;
        private IDbConnectionLogic _dbConnectionLogic;
        public HealthFacilityRepo(IListConverter listConverter, IDbConnectionLogic dbConnectionLogic)
        {
            _listConverter = listConverter;
            _dbConnectionLogic = dbConnectionLogic;
        }

        public async Task<ResponseResult<string>> InsertUpdateDeleteHealthFacility(HealthFacilityRequest request)
        {
            ResponseResult<string> responseResult = new ResponseResult<string>();

            try
            {
                string spName = @"SP_HFDMS_ManageHealthFacility";
                Hashtable Param = new Hashtable
                {
                    { "@ID", request.Id },
                    { "@Name", request.Name},
                    { "@District", request.District},
                    { "@Region", request.Region },
                    { "@State", request.State },
                    { "@Country", request.country},
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

        public async Task<ResponseResult<List<GetHealthFacilityResponse>>> GetHealthFacilityData(HealthFacilityRequest request)
        {
            ResponseResult<List<GetHealthFacilityResponse>> responseResult = new ResponseResult<List<GetHealthFacilityResponse>>();
            List<GetHealthFacilityResponse> healthFacilityResponse = new List<GetHealthFacilityResponse>();

            try
            {
                Hashtable Param = new Hashtable
                {
                    { "@ID", request.Id }
                };

                string Query = "SP_HFDMS_GetHealthFacilities";

                DataTable dt = await _dbConnectionLogic.ExecuteSelectQueryAsync(Query, Param);
                if (dt.Rows.Count > 0)
                {
                    healthFacilityResponse = _listConverter.ConvertDataTable<GetHealthFacilityResponse>(dt);

                    responseResult = new ResponseResult<List<GetHealthFacilityResponse>>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = healthFacilityResponse
                    };
                }
                else
                {
                    responseResult = new ResponseResult<List<GetHealthFacilityResponse>>
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

                responseResult = new ResponseResult<List<GetHealthFacilityResponse>>
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
