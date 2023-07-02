using CommonLibrary;
using CommonLibrary.Requests;
using CommonLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Server;

public class Core
{
    public Core()
    {

    }

    private List<Client> _clients = new List<Client>();

    private List<Message> _messages = new List<Message>();

    private T Deserialize<T>(Data data) where T : class
    {
        return JsonSerializer.Deserialize<T>(data.Content);
    }

    private bool authUser(Client client)
    {
        var existClient = _clients.FirstOrDefault(x => x.Login == client.Login);
        if (existClient == null)
        {
            return false;
        }
        return client.Password == existClient.Password;
    }

    private Data HandleLoginRequest(Data data)
    {
        
        var request = Deserialize<LoginRequest>(data);

        var client = _clients.FirstOrDefault(x => x.Login == request.Me.Login);
        if (client == null)
        {
            //New client 
            _clients.Add(request.Me);
            return Data.Create(new LoginResponse
            {
                Success = true,
                MessagesCount = 0,
            });
        }
        
        // Client exists
        if (!authUser(request.Me)) {
            return Data.Create(new ErrorResponse
            {
                Error = "Invalid password"
            });
        }
        // Valid password

        return Data.Create(new LoginResponse
        {
            Success = true,
            MessagesCount = _messages.Where(x => x.To.Login == request.Me.Login).Count(),
        });
    }

    private Data HandleSendMessageRequest(Data data)
    {
        var request = Deserialize<SendMessageRequest>(data);

        if (!authUser(request.Message.From))
        {
            return Data.Create(new ErrorResponse
            {
                Error = "Not authorized"
            });
        }

        _messages.Add(request.Message);
        return Data.Create(new SendMessageResponse { Success = true });
    }

    private Data HandleGetMessagesRequest(Data data)
    {
        var request = Deserialize<GetMessagesRequest>(data);

        if (!authUser(request.Me))
        {
            return Data.Create(new ErrorResponse
            {
                Error = "Not authorized"
            });
        }

        var clientMessages = _messages.Where(x => x.To.Login == request.Me.Login).ToList();
        _messages.RemoveAll(x => x.To.Login == request.Me.Login);
        
        return Data.Create(new GetMessagesResponse
        {
            Messages = clientMessages
        });
    }

    public Data Handle(Data request)
    {
        switch (request.Type)
        {
            case DataType.LoginRequest:
                return HandleLoginRequest(request);
            case DataType.SendMessageRequest:
                return HandleSendMessageRequest(request);
            case DataType.GetMessageRequest:
                return HandleGetMessagesRequest(request);
            default:
                return Data.Create(new ErrorResponse
                {
                    Error = "Unknown request"
                });
        }
    }
}
