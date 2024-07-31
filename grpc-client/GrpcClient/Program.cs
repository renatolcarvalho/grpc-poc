using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7043");
var client = new Custom.CustomClient(channel);
var reply = await client.SayYourNameAsync(new SayYourNameRequest { FirstName = "Renato", LastName = "Carvalho" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();