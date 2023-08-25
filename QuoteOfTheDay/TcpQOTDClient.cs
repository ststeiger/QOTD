
namespace QuoteOfTheDay
{


    class TcpQOTDClient
    {


        private static async System.Threading.Tasks.Task<string> ReceiveQuoteFromServer(System.Net.Sockets.TcpClient client)
        {
            string? quote = null;

            using (System.Net.Sockets.NetworkStream stream = client.GetStream())
            {
                byte[] data = new byte[1024];
                // int bytesRead = stream.Read(data, 0, data.Length);
                int bytesRead = await stream.ReadAsync(data, 0, data.Length);
                // Note: QOTD is ASCII, but UTF8 is backwards-compatible 
                quote = System.Text.Encoding.UTF8.GetString(data, 0, bytesRead);
            } // End Using stream 

            return quote;
        } // End Task ReceiveQuoteFromServer 


        public static async System.Threading.Tasks.Task Test()
        {
            string serverAddress = "127.0.0.1"; // Replace with the server's IP address
            int serverPort = 17; // QOTD port

            try
            {
                using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(serverAddress, serverPort))
                {
                    System.Console.WriteLine($"Connected to TCP-QOTD server at {serverAddress}:{serverPort}");

                    string quote = await ReceiveQuoteFromServer(client);
                    System.Console.WriteLine($"Quote of the Day: {quote}");
                } // End Using client 
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"An error occurred: {ex.Message}");
            }
        } // End Task Test 


    } // End Class TcpQOTDClient 


} // End Namespace 