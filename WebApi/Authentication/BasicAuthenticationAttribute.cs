using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Application.Authentication.BaseAuth;
using Application.Authentication.CodeAuth;

namespace WebApi.Authentication
{
    public class BasicAuthenticationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _code;

        public BasicAuthenticationAttribute(string code = null)
        {
            _code = code;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                var username = decodedUsernamePassword.Split(':', 2)[0];
                var password = decodedUsernamePassword.Split(':', 2)[1];

                bool isAuthenticated = false;
                if (string.IsNullOrEmpty(_code))
                {
                    var query = new AuthUserQuery()
                    {
                        Login = username,
                        Password = password
                    };

                    var mediator = context.HttpContext.RequestServices.GetService<IMediator>();
                    isAuthenticated = mediator.Send(query).Result;
                    
                }
                else
                {
                    var query = new AuthUserByCodeQuery()
                    {
                        Login = username,
                        Password = password,
                        GroupCode = _code
                    };

                    var mediator = context.HttpContext.RequestServices.GetService<IMediator>();
                    isAuthenticated = mediator.Send(query).Result;
                }

                if (!isAuthenticated)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
            else
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
