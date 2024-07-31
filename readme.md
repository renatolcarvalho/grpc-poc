# C# GRPC

## What is GRPC?
TBD

## GRPC VS REST
TBD

## Microsoft Tutorial
> https://learn.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-8.0&tabs=visual-studio

## What is Proto file?
The proto file is basicly the Contract of our API.

## And how about Proto?
> Proto is a Protocol buffer language to structure your protocol buffer data.

## Microsoft definition:
> gRPC uses a contract-first approach to API development. Services and messages are defined in .proto files.

### Definitions
- Field Types
- Message Type
- Field Labels

## Proto Documentation
This link explain the syntax and the Proto capatibiliets.

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