using Microsoft.Extensions.Logging;

namespace ApplicationServices.Services;

public abstract class Service
{
    protected Service(ILogger<Service> logger)
    {
        Logger = logger;
    }

    protected ILogger<Service> Logger { get; }
}