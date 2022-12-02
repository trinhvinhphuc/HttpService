using System.Net.Sockets;

namespace HttpClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            if (addressBox.Text.StartsWith("http://", StringComparison.OrdinalIgnoreCase) == false)
            {
                addressBox.Text = "http://" + addressBox.Text;
            }
            TcpClient client = new TcpClient();

            try
            {
                client.Connect(getHost(addressBox.Text), getPort(addressBox.Text));
            }
            catch
            {
                bodyBox.Text = "Can not connect to the Server.";
                return;
            }
            
            StreamWriter OutStream = new StreamWriter(client.GetStream());
            StreamReader InpStream = new StreamReader(client.GetStream()); 

            OutStream.WriteLine("GET " + addressBox.Text + " HTTP/1.0"); 
            OutStream.WriteLine("ContentrType:text/html"); 
            OutStream.WriteLine("ContentrLanguage:en"); 
            OutStream.WriteLine(); 
            OutStream.Flush(); 

            headerBox.Text = ""; 
            string s; 
            while (null != (s = InpStream.ReadLine())){ 
                if (s.Equals("")) break;
                headerBox.Text += s; 
            } 
            bodyBox.Text = ""; 
            int c; 
            while (true){ 
                c = InpStream.Read(); 
                if (c == -1) break; 
                bodyBox.Text += (char)c; 
            } 
            client.Close(); 
        }

        internal String getHost(String url)
        {
            url = url.ToLower();
            Uri uri = new Uri(url);
            return uri.Host;
        }

        internal int getPort(String url)
        {
            Uri uri = new Uri(url);
            return (int)uri.Port;
        }

        private void addressBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                goButton.PerformClick();
            }
        }
    }
}