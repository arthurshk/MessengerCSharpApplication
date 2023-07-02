using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommonLibrary;
using System.Text.Json;
using CommonLibrary.Requests;
using CommonLibrary.Responses;

namespace CommonLibrary;

public class Service
{
    public Service(string host, int port)
    {
        _ep = new IPEndPoint(IPAddress.Parse(host), port);
    }

    private IPEndPoint _ep;

    private Data Send(Data request)
    {
        Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        s.Connect(_ep);
        s.Send(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request)));

        var buffer = new byte[4096];
        int read = s.Receive(buffer);

        string incoming = Encoding.UTF8.GetString(buffer, 0, read);
        return JsonSerializer.Deserialize<Data>(incoming);
    }

    private T ToResponse<T>(Data response) where T : class
    {
        return
            JsonSerializer.Deserialize<T>(response.Content);
    }

    private void IsError(Data rawResponse)
    {
        if (rawResponse.Type == DataType.ErrorResponse)
        {
            throw new Exception(ToResponse<ErrorResponse>(rawResponse).Error);
        }
    }

    public int GetMessageCount(Client me)
    {
        var rawResponse = Send(Data.Create(new LoginRequest
        {
            Me = me
        }));
        IsError(rawResponse);
        var response = ToResponse<LoginResponse>(rawResponse);
        return response.MessagesCount;
    }

    public List<Message> GetMessages(Client me)
    {
        var rawResponse = Send(Data.Create(new GetMessagesRequest
        {
            Me = me
        }));
        IsError(rawResponse);
        var response = ToResponse<GetMessagesResponse>(rawResponse);
        return response.Messages;
    }

    public bool SendMessage(Message message)
    {
        var rawResponse = Send(Data.Create(new SendMessageRequest
        {
            Message = message
        }));
        IsError(rawResponse);
        var response = ToResponse<SendMessageResponse>(rawResponse);
        return response.Success;
    }
}
