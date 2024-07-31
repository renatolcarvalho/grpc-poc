using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomService : Custom.CustomBase
    {
        private readonly ILogger<CustomService> _logger;
        public CustomService(ILogger<CustomService> logger)
        {
            _logger = logger;
        }

        public override Task<SayYourNameReply> SayYourName(SayYourNameRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SayYourNameReply
            {
                Message = $"Hey {request.FirstName} {request.LastName}"
            });
        }
    }
}
