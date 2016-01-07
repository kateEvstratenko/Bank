using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BankServerApi.DataObjects.Responses;
using BLL;
using BLL.Helpers;
using Core;
using Core.Enums;
using Microsoft.AspNet.Identity;

namespace BankServerApi
{
    public interface IOrderedFilter : IFilter
    {
        int Order { get; set; }
    }

     public class OrderedFilterProvider : IFilterProvider
    {
        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            // controller-specific
            IEnumerable<FilterInfo> controllerSpecificFilters = OrderFilters(actionDescriptor.ControllerDescriptor.GetFilters(), FilterScope.Controller);

            // action-specific
            IEnumerable<FilterInfo> actionSpecificFilters = OrderFilters(actionDescriptor.GetFilters(), FilterScope.Action);

            return controllerSpecificFilters.Concat(actionSpecificFilters).Distinct();
        }

        private IEnumerable<FilterInfo> OrderFilters(IEnumerable<IFilter> filters, FilterScope scope)
        {
            return filters.OfType<IOrderedFilter>()
                            .OrderBy(filter => filter.Order)
                            .Select(instance => new FilterInfo(instance, scope));
        }
    }

    public class CheckRoleAttribute : ActionFilterAttribute, IOrderedFilter
    {
        public int Order { get; set; }
        public AppRoles[] Roles { get; set; }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var userManager = Startup.UserManagerFactory();
                var tokenObj = new ParsedTokenHelper().GetParsedToken(actionContext.Request.Properties);
                var userId = tokenObj.UserId;
                if (Roles.Any(role => userManager.IsInRole(userId, role.ToString())))
                {
                    return;
                }
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            catch (TokenExpiredException)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ResponseBase.TokenExpired());
            }
            catch (BankClientException ex)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ResponseBase.Unsuccessful(ex));
            }

            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, ResponseBase.Unsuccessful(ex));
            }
        }
    }
}