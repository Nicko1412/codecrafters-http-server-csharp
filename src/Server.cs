using System.Net;
using System.Net.Sockets;
using System.Text;

// You can use print statements as follows for debugging, they'll be visible when running tests.
Console.WriteLine("Logs from your program will appear here!");

// Uncomment this block to pass the first stage
TcpListener server = new TcpListener(IPAddress.Any, 4221);
server.Start();
var socket = server.AcceptSocket();

var buffer = new byte[1024];
socket.Receive(buffer);

var lines = ASCIIEncoding.ASCII.GetString(buffer).Split("\r\n");
string requestLine = lines[0];
string url = requestLine.Split(' ')[1];


if (url == "/")
{
    socket.Send(Encoding.UTF8.GetBytes("HTTP/1.1 200 OK\r\n\r\n"));   
} 
else if(url.StartsWith("/echo/"))
{
    string echoWord = url.Substring(6);
    socket.Send(Encoding.UTF8.GetBytes($"HTTP/1.1 200 OK\r\nContent-Type: text/plain\r\nContent-Length: {echoWord.Length}\r\n\r\n{echoWord}"));
} 
else
{
    socket.Send(Encoding.UTF8.GetBytes("HTTP/1.1 404 Not Found\r\n\r\n"));
}
