using Grpc.Core;

namespace GrpcServer.Bases
{
    public abstract partial class CustomBase
    {
        public virtual Task<SayYourNameReply> SayYourName(SayYourNameRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }
    }

    public class SayYourNameRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }

    public class SayYourNameReply
    {
        public required string Message { get; set; }
    }
}
