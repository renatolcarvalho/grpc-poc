using Grpc.Core;
using GrpcServer.Bases;

namespace GrpcServer.Services
{
    public class CustomService : CustomBase
    {
        private readonly ILogger<CustomService> _logger;
        public CustomService(ILogger<CustomService> logger)
        {
            _logger = logger;
        }

        public Task<SayYourNameReply> SayYourName(SayYourNameRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SayYourNameReply
            {
                Message = $"Hey {request.FirstName} {request.LastName}"
            });
        }
    }
}
