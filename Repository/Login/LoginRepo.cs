using CommonServices.EncyptionDecryption;
using CommonServices.ListConverter;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RequestModel.HealthWorker;
using RequestModel.Login;
using ResponseModel;
using ResponseModel.BaseResponse;
using ResponseModel.HealthWorker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Login
{
    public class LoginRepo : ILoginRepo
    {
        private IListConverter _listConverter;
        private IDbConnectionLogic _dbConnectionLogic;
        private readonly IConfiguration _configuration;
        private readonly IEncyptDecryptService _encyptDecryptService;
        public LoginRepo(IListConverter listConverter, IDbConnectionLogic dbConnectionLogic, IConfiguration configuration, IEncyptDecryptService encyptDecryptService)
        {
            _listConverter = listConverter;
            _dbConnectionLogic = dbConnectionLogic;
            _configuration = configuration;
            _encyptDecryptService = encyptDecryptService;
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

                    var token = GenerateJwtToken(loginResponse);
                    loginResponse.Token = token;

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

        private string GenerateJwtToken(LoginResponse user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSettings:Key").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, _encyptDecryptService.EncryptPayload(user.UserId)),
                    new Claim(ClaimTypes.Name, _encyptDecryptService.EncryptPayload(user.Name)),
                    new Claim(ClaimTypes.Role, _encyptDecryptService.EncryptPayload(user.RoleName))
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration.GetSection("JwtSettings:Audience").Value, // Add the Audience
                Issuer = _configuration.GetSection("JwtSettings:Issuer").Value // Optionally, add the Issuer as well
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
