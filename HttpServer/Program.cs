using System; 
using System.IO; 
using System.Net; 
using System.Net.Sockets; 
public class SimpleWebServer 
{ 
    public void StartListening(int port) 
    { 
        IPEndPoint LocalEndPoint = new IPEndPoint(IPAddress.Any, 8080); 
        TcpListener server = new TcpListener(LocalEndPoint); 
        server.Start(); 
        while (true) { 
              TcpClient client = server.AcceptTcpClient();
              processClientRequest(client); 
        } 
    }
    private void processClientRequest(TcpClient client) 
    { 
        Console.WriteLine("May khach su dung cong: " + (client.Client.RemoteEndPoint as IPEndPoint).Port + "; Dia chi IP may khach: " + client.Client.RemoteEndPoint.ToString() + "\n"); 
        Console.WriteLine("Yeu cau: "); 
        readRequest(client); 
        sendResponse(client); 
        client.Close(); 
     } 

    private void readRequest(TcpClient client) 
    { 
        StreamReader request = new StreamReader(client.GetStream()); 
        String nextLine; 
        while (null != (nextLine = request.ReadLine())) 
        {
              if (nextLine.Equals("")) 
              { 
                    break; 
              } 
              else 
              { 
                    Console.WriteLine(nextLine); 
              } 
        } 
        Console.WriteLine("--------------------------------------------"); 
    } 
    private void sendResponse(TcpClient client) { 
        StreamWriter response = new StreamWriter(client.GetStream()); 
        response.Write(_mainPage); 
        response.Flush(); 
    }

    private const String _mainPage = 
        "HTTP/1.0 200 OK\n" + 
        "Contentrtype: text/html\n" + 
        "Connection: close\n" + 
        "\n<HTML>\n" + 
        "<HEAD>\n" + 
        "<TITLE>Hello World</TITLE>\n" + 
        "</HEAD>\n" + 
        "<BODY> <a href = \"localhost\"> Trinh Vinh Phuc 3119410318 </a></BODY>\n" + 
        "</HTML> \n\n"; 
    } 
    class TestWebServer { 
        public static void Main(String[] args) { 
            SimpleWebServer webServer1 = new SimpleWebServer(); 
            webServer1.StartListening(8080); 
        } 
    } 