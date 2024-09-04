using CommonServices.ListConverter;
using DAL;
using RequestModel.HealthWorker;
using ResponseModel.BaseResponse;
using ResponseModel.Dashboard;
using ResponseModel.HealthWorker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Dashboard
{
    public class DashboardRepo : IDashboardRepo
    {
        private IListConverter _listConverter;
        private IDbConnectionLogic _dbConnectionLogic;
        public DashboardRepo(IListConverter listConverter, IDbConnectionLogic dbConnectionLogic)
        {
            _listConverter = listConverter;
            _dbConnectionLogic = dbConnectionLogic;
        }

        public async Task<ResponseResult<List<PatientPerHealthFacility>>> PatientPerHealthFacility()
        {
            ResponseResult<List<PatientPerHealthFacility>> responseResult = new ResponseResult<List<PatientPerHealthFacility>>();
            List<PatientPerHealthFacility> patientPerHealthFacility = new List<PatientPerHealthFacility>();

            try
            {
                string Query = "SP_HFDMS_GetPatientsPerHealthFacility";
                Hashtable Param = new Hashtable{};


                DataTable dt = await _dbConnectionLogic.ExecuteSelectQueryAsync(Query, Param);
                if (dt.Rows.Count > 0)
                {
                    patientPerHealthFacility = _listConverter.ConvertDataTable<PatientPerHealthFacility>(dt);

                    responseResult = new ResponseResult<List<PatientPerHealthFacility>>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = patientPerHealthFacility
                    };
                }
                else
                {
                    responseResult = new ResponseResult<List<PatientPerHealthFacility>>
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

                responseResult = new ResponseResult<List<PatientPerHealthFacility>>
                {
                    StatusCode = "02",
                    Message = "Internal Server Error",
                    Data = null
                };

                return responseResult;
            }
        }

        public async Task<ResponseResult<List<HealthWorkersPerRegion>>> HealthWorkersPerRegion()
        {
            ResponseResult<List<HealthWorkersPerRegion>> responseResult = new ResponseResult<List<HealthWorkersPerRegion>>();
            List<HealthWorkersPerRegion> healthWorkersPerReagion = new List<HealthWorkersPerRegion>();

            try
            {
                string Query = "SP_HFDMS_GetHealthWorkersPerRegion";
                Hashtable Param = new Hashtable{};

                DataTable dt = await _dbConnectionLogic.ExecuteSelectQueryAsync(Query, Param);
                if (dt.Rows.Count > 0)
                {
                    healthWorkersPerReagion = _listConverter.ConvertDataTable<HealthWorkersPerRegion>(dt);

                    responseResult = new ResponseResult<List<HealthWorkersPerRegion>>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = healthWorkersPerReagion
                    };
                }
                else
                {
                    responseResult = new ResponseResult<List<HealthWorkersPerRegion>>
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

                responseResult = new ResponseResult<List<HealthWorkersPerRegion>>
                {
                    StatusCode = "02",
                    Message = "Internal Server Error",
                    Data = null
                };

                return responseResult;
            }
        }

        public async Task<ResponseResult<List<GenderDistribution>>> GenderDistribution()
        {
            ResponseResult<List<GenderDistribution>> responseResult = new ResponseResult<List<GenderDistribution>>();
            List<GenderDistribution> healthWorkersPerReagion = new List<GenderDistribution>();

            try
            {
                string Query = "SP_HFDMS_GetGenderDistributionOfPatients";
                Hashtable Param = new Hashtable { };

                DataTable dt = await _dbConnectionLogic.ExecuteSelectQueryAsync(Query, Param);
                if (dt.Rows.Count > 0)
                {
                    healthWorkersPerReagion = _listConverter.ConvertDataTable<GenderDistribution>(dt);

                    responseResult = new ResponseResult<List<GenderDistribution>>
                    {
                        StatusCode = "00",
                        Message = "Success",
                        Data = healthWorkersPerReagion
                    };
                }
                else
                {
                    responseResult = new ResponseResult<List<GenderDistribution>>
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

                responseResult = new ResponseResult<List<GenderDistribution>>
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
