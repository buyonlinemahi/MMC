using MMCApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using MMCApp.Infrastructure.Base;
using MMCApp.Domain.Models.UserModel;

namespace MMCApp.Infrastructure.ApplicationFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthorizedUserCheckAttribute : AuthorizeAttribute
    {
     //   private readonly bool _authorize;
          private readonly string[] allowedroles;

          public AuthorizedUserCheckAttribute(params string[] roles)
        {
            this.allowedroles = roles;  
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
          {
              bool _authorize = false;
              if (httpContext.Session[GlobalConst.SessionKeys.USER] != null)
              {                 
                  User usr = new User();
                  usr = (User)(httpContext.Session[GlobalConst.SessionKeys.USER]);
                  foreach (var role in allowedroles)
                  {
                      if (role == "AllowManagement")
                      {
                          if (usr.UserManagementPermission == true)
                          {
                              _authorize = true;
                          }
                          else
                          {
                              _authorize = false;
                          }
                      }
                      else
                      {
                          _authorize = true;
                      }
                  }
                  if (allowedroles.Count() == 0)
                  {
                      _authorize = true;
                  }
              }

              return _authorize;
        }
  

    }
}
