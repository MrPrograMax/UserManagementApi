using System;
using System.Web.Http.Controllers;

namespace WebApi.Authentication
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CodeAuthenticationAttribute : BasicAuthenticationAttribute
    {
        

    }
}
