using CommonServices.ListConverter;
using DAL;
using RequestModel.HealthFacility;
using RequestModel.HealthWorker;
using RequestModel.Patient;
using ResponseModel.BaseResponse;
using ResponseModel.HealthWorker;
using ResponseModel.Patient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Patient
{
    public class PatientRepo : IPatientRepo
    {
        private IListConverter _listConverter;
        private IDbConnectionLogic _dbConnectionLogic;
        public PatientRepo(IListConverter listConverter, IDbConnectionLogic dbConnectionLogic)
        {
            _listConverter = listConverter;
            _dbConnectionLogic = dbConnectionLogic;
        }

        public async Task<ResponseResult<string>> InsertUpdateDeletePatient(PatientRequest request)
        {
            ResponseResult<string> responseResult = new ResponseResult<string>();

            try
            {
                string spName = @"SP_HFDMS_ManagePatient";
                Hashtable Param = new Hashtable
                {
                    { "@ID", request.ID },
                    { "@Name", request.Name },
                    { "@Gender", request.Gender },
                    { "@Address", request.Address },
                    { "@HealthFacilityID", request.HealthFacilityId },
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

        public async Task<ResponseResult<List<PatientResponse>>> GetPatientData(PatientRequest request)
        {
            ResponseResult<List<PatientResponse>> responseResult = new ResponseResult<List<PatientResponse>>();
            List<PatientResponse> healthWorkerResponse = new List<PatientResponse>();

            try
            {
                Hashtable Param = new Hashtable
                {
                    { "@ID", request.ID }
                };

                string Query = "SP_HFDMS_GetPatients";

                DataTable dt = await _dbConnectionLogic.ExecuteSelectQueryAsync(Query, Param);
                if (dt.Rows.Count > 0)
                {
                    healthWorkerResponse = _listConverter.ConvertDataTable<PatientResponse>(dt);

                    responseResult = new ResponseResult<List<PatientResponse>>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = healthWorkerResponse
                    };
                }
                else
                {
                    responseResult = new ResponseResult<List<PatientResponse>>
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

                responseResult = new ResponseResult<List<PatientResponse>>
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
