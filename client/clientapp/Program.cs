#pragma warning disable CS4014 
using System.Net.WebSockets;
using System.Text;

 
string server = "localhost:8080";
if (Environment.GetEnvironmentVariable("SERVER") != null && Environment.GetEnvironmentVariable("SERVER").ToString() != "")
{
    server = Environment.GetEnvironmentVariable("SERVER").ToString();
}
Console.WriteLine("server:" + server);
        ClientWebSocket socket = new ClientWebSocket();
        Uri uri = new Uri("ws://" + server + "/ws");
        var cToken = new CancellationTokenSource();
        await socket.ConnectAsync(uri, cToken.Token);

        Console.WriteLine(socket.State);

        Task.Factory.StartNew(
            async () =>
            {
                var rcvBytes = new byte[128];
                var rcvBuffer = new ArraySegment<byte>(rcvBytes);
                while (true)
                {
                    WebSocketReceiveResult rcvResult = await socket.ReceiveAsync(rcvBuffer, cToken.Token);
                    byte[] msgBytes = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray();
                    string rcvMsg = Encoding.UTF8.GetString(msgBytes);
                    if (rcvMsg != "")
                    {
                        Console.Write(rcvMsg);
                    }
                }
            }, cToken.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

        while (true)
        {
            var message = Console.ReadLine();
            if (message == null) message = "";
            if (message == "Bye")
            {
                cToken.Cancel();
                return;
            }
            byte[] sendBytes = Encoding.UTF8.GetBytes(message);
            var sendBuffer = new ArraySegment<byte>(sendBytes);
            await
                socket.SendAsync(sendBuffer, WebSocketMessageType.Text, endOfMessage: true,
                                    cancellationToken: cToken.Token);
        }
  

