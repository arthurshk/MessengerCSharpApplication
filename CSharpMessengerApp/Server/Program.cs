
using CommonLibrary;
using Server;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

//Server

Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
IPAddress ip = IPAddress.Parse("127.0.0.1");
IPEndPoint ep = new IPEndPoint(ip, 5000); // 5000 - port

s.Bind(ep);
s.Listen(1000);

Console.WriteLine("Server");

var core = new Core();

Action<Socket> worker = (ns) =>
{
    var buffer = new byte[4096];
    int read = ns.Receive(buffer);

    string incoming = Encoding.UTF8.GetString(buffer, 0, read);
    Console.WriteLine(incoming);
    var request = JsonSerializer.Deserialize<Data>(incoming);
    var response = JsonSerializer.Serialize(core.Handle(request));
    Console.WriteLine(response);
    ns.Send(Encoding.UTF8.GetBytes(response));
    ns.Shutdown(SocketShutdown.Both);
    ns.Close();
};



while (true)
{
    Task.Run(() => worker(s.Accept()));
}
