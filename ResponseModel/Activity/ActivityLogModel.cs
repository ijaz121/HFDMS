using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseModel.Activity
{
    public class ActivityLogModel
    {
           public string? LogID {get;set;}
           public string? UserID  {get;set;}
           public string? Action {get;set;}
           public string? Endpoint    {get;set;}
           public string? Method {get;set;}
           public string? RequestData {get;set;}
           public string? ResponseData {get;set;}
           public string? StatusCode  {get;set;}
           public string? Timestamp { get; set; }
    }
}
