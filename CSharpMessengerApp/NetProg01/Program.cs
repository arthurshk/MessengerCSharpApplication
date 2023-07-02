using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using CommonLibrary;
using CommonLibrary.Requests;

//Client

Console.WriteLine("Client");


var service = new Service("127.0.0.1", 5000);

Console.Write("Login: ");
var login = Console.ReadLine();

var messageCount = service.GetMessageCount(login);

Console.WriteLine("Message count: {0}", messageCount);

if (messageCount > 0)
{
    service.GetMessages(login).ForEach(m =>
    {
        Console.WriteLine("From: {0}", m.From.Login);
        Console.WriteLine("To: {0}", m.To.Login);
        Console.WriteLine(m.CreatedAt.ToString("G"));
        Console.WriteLine(m.Text);
        Console.WriteLine();
    });
}

Console.Write("Message to: ");
var toLogin = Console.ReadLine();
Console.Write("Text: ");
var text = Console.ReadLine();

var message = new Message
{
    From = new CommonLibrary.Client { Login = login },
    To = new CommonLibrary.Client { Login = toLogin },
    Text = text,
    CreatedAt = DateTime.Now,
};
if (service.SendMessage(message))
{
    Console.WriteLine("Send succes");
}



