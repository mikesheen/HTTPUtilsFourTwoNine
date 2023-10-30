using Funq;
using ServiceStack;
using HTTPUtilsFourTwoNine.ServiceInterface;
using ServiceStack.Web;

[assembly: HostingStartup(typeof(HTTPUtilsFourTwoNine.AppHost))]

namespace HTTPUtilsFourTwoNine;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            // Configure ASP.NET Core IOC Dependencies
        });

    public AppHost() : base("HTTPUtilsFourTwoNine", typeof(MyServices).Assembly) 
    {
        // Add a rate limit request filter									
        this.GlobalRequestFilters.Add((req, res, dto) => { RateLimitRequestFilter(req, res, dto); });
    }

    public override void Configure(Container container)
    {
        // Configure ServiceStack only IOC, Config & Plugins
        SetConfig(new HostConfig {
            UseSameSiteCookies = true,
        });
    }

    public void RateLimitRequestFilter(IRequest req, IResponse res, object dto)
    {
        res.StatusCode = 429;
        res.StatusDescription = $"Your have exceeded your rate limit of X requests per Y seconds. It will be reset in Z seconds.";
        res.AddHeader("Retry-After", "90");
        res.Close();
    }
}
