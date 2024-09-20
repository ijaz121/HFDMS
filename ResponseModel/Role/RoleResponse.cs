using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseModel.Role
{
    public class RoleResponse
    {
        public string? RoleID { get; set; }
        public string? RoleName { get; set; }
    }

    public class MappedDataResponse
    {
        public string? RoleId {get;set;}
        public string? ActivityId  {get;set;}
        public string? CanView {get;set;}
        public string? CanUpdate   {get;set;}
        public string? CanDelete {get;set;}
        public string? CanCreate { get; set; }

    }
}
