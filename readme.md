# gRPC in C#

# What is GRPC?
gRPC (gRPC Remote Procedure Calls) is an open-source remote procedure call (RPC) framework developed by Google. It enables communication between applications across different languages and environments in a more efficient and straightforward way compared to traditional methods.

> https://grpc.io/

## gRPC VS REST (Some comparisons)

### Protocol and Data Format
- gRPC: Uses HTTP/2 as the transport protocol and Protocol Buffers (protobufs) for serializing structured data. This combination provides efficient communication with smaller message sizes and faster processing.

- REST: Typically uses HTTP/1.1 as the transport protocol and JSON or XML for data format. JSON is human-readable but can be larger and slower to parse compared to protobufs.

### Performance

- gRPC: Generally faster due to HTTP/2 features like multiplexing, header compression, and binary serialization with protobufs. This makes gRPC well-suited for low-latency, high-throughput scenarios.

- REST: Can be slower due to text-based serialization (e.g., JSON), lack of HTTP/2 features (if using HTTP/1.1), and larger message sizes.

### Development Complexity

- gRPC: Can be more complex to set up due to the need for generating client and server code from .proto files. However, once set up, development can be straightforward with strong typing and tooling support.

- REST: Generally easier to set up and start with, as it relies on plain HTTP and standard libraries available in most languages. However, maintaining consistent API documentation and handling schema evolution can add complexity over time.

## When should I use gRPC?
- gRPC:
  - Microservices communication where performance and type safety are critical.
  - Real-time applications requiring streaming capabilities (e.g., chat applications, IoT).
  - Inter-service communication in environments where all services can be controlled and updated together.

- REST:
  - Public APIs where interoperability and ease of use are priorities.
  - Applications where human readability and debuggability of messages are important.
  - Simpler CRUD operations and applications with less stringent performance requirements.

---

# Hands On
> The entire code example will be available on this git repository: https://github.com/renatolcarvalho/grpc-poc

## Microsoft Tutorial
> https://learn.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-8.0&tabs=visual-studio

## What is Proto file?
The proto file is basically the Contract of our API.

## And how about Proto?
> Proto is a Protocol buffer language to structure your protocol buffer data.

## Microsoft definition:
> gRPC uses a contract-first approach to API development. Services and messages are defined in .proto files.

### Definitions
- Field Types
- Message Type
- Field Labels

## Proto Documentation
This link explain the syntax and the Proto capability.

> https://protobuf.dev/programming-guides/proto3/

## Default proto file

```
syntax = "proto3";

option csharp_namespace = "GrpcServer";

package greet;

// The greeting service definition.
service Greeter {
  // Sends a greeting
  rpc SayHello (HelloRequest) returns (HelloReply);
}

// The request message containing the user's name.
message HelloRequest {
  string name = 1;
}

// The response message containing the greetings.
message HelloReply {
  string message = 1;
}
```

## ASP NET Core Git Hub (GRPC Examples)
> https://github.com/dotnet/AspNetCore.Docs/tree/main/aspnetcore/tutorials/grpc

# Lets create a custom service
> https://learn.microsoft.com/en-us/aspnet/core/grpc/services?view=aspnetcore-8.0

## Server Side

First, lets define a custom proto file:

```
syntax = "proto3";

option csharp_namespace = "GrpcServer";

package custom;

// The greeting service definition.
service Custom {
  // Sends a greeting
  rpc SayYourName (SayYourNameRequest) returns (SayYourNameReply);
}

// The request message containing the user's name.
message SayYourNameRequest {
  string firstName = 1;
  string lastName = 2;
}

// The response message containing the greetings.
message SayYourNameReply {
  string message = 1;
}
```
In this case we will have two different parameters. First name and Last name.

> IMPORTANT: After created the proto file, ensure to added the proto reference as Protobuf in the .csproj. Using the correct nuget packages, after building the solution, all the base classes for this new proto file will be created. We will use these classes for implement and consume this new contract. As we can see bellow:

```
<ItemGroup>
  <Protobuf Include="Protos\custom.proto" GrpcServices="Server" />
</ItemGroup>
```

So, after building and having the new proto added, we will be able to implement the services, as we can see bellow:

```
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
```

### Nuget Packages

```
<PackageReference Include="Grpc.AspNetCore" Version="2.57.0" />
```

### Notes.:

1. It is possible now to implement the Custom.CustomBase file.
2. It is necessary override the method defined with the custom logic.

### Mapping the new Proto (Contract)

It is necessary map into the program file the new service available, as we can see below:

```
app.MapGrpcService<CustomService>();
```

## Client Side

The only file that should be shared between the Server and Client project is the proto.
The proto has all the necessary information to perform the invocations.

So, first step is include the same file into the client project and remember to include the Protobuf reference.

```
<ItemGroup>
  <Protobuf Include="Protos\custom.proto" GrpcServices="Server" />
</ItemGroup>
```

After that, ensure that you are using the necessary nuget packages and build the solution to have the new base files created.

### Nuget Packages

```
<PackageReference Include="Google.Protobuf" Version="3.27.3" />
<PackageReference Include="Grpc.AspNetCore" Version="2.65.0" />
<PackageReference Include="Grpc.Net.Client" Version="2.65.0" />
<PackageReference Include="Grpc.Tools" Version="2.65.0">
```

After that is just about to execute the request, as we can see bellow:

```
// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7043");
var client = new Custom.CustomClient(channel);
var reply = await client.SayYourNameAsync(new SayYourNameRequest { FirstName = "Renato", LastName = "Carvalho" });
```

## Thanks
Thanks for have reached the end of this document.