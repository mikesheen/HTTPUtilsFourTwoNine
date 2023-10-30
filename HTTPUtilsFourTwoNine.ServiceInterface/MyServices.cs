using ServiceStack;
using HTTPUtilsFourTwoNine.ServiceModel;

namespace HTTPUtilsFourTwoNine.ServiceInterface;

public class MyServices : Service
{
    public object Any(Hello request)
    {       
        return new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}