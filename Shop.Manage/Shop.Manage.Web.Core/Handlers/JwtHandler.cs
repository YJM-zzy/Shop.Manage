using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Shop.Manage.Web.Core
{
    public class JwtHandler : AppAuthorizeHandler
    {
        public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            // 这里写您的授权判断逻辑，授权通过返回 true，否则返回 false
            var type = httpContext.User.FindFirst("type");
            var router = httpContext.Request.Path.Value;
            string[] arr = router.Split('/');
            if(type.Value == "app")
            {
                if (arr[2] != "app")
                {
                    return Task.FromResult(false);
                }
            }
            else
            {
                if (arr[2] == "app")
                {
                    return Task.FromResult(false);
                }
            }
            return Task.FromResult(true);
        }
    }

} 