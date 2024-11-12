using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonServices.Claims
{
    public interface IClaimValues
    {
        bool ValidateUser(string userId, string token);
    }
}
