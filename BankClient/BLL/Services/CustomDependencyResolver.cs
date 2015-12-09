using System.Web.Http.Dependencies;

namespace BLL.Services
{
    public static class CustomDependencyResolver
    {
        public static IDependencyResolver Resolver { get; set; }
    }
}