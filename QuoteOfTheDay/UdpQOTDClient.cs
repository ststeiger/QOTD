
namespace QuoteOfTheDay
{


    class UdpQOTDClient
    {


        public static async System.Threading.Tasks.Task Test()
        {
            string serverAddress = "127.0.0.1"; // Replace with the server's IP address
            int serverPort = 17; // QOTD port

            using (System.Net.Sockets.UdpClient udpClient = new System.Net.Sockets.UdpClient())
            {
                udpClient.Connect(serverAddress, serverPort);
                System.Console.WriteLine($"Connected to UDP-QOTD server at {serverAddress}:{serverPort}");

                byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes("Give me a quote!");
                await udpClient.SendAsync(requestBytes, requestBytes.Length);

                System.Net.Sockets.UdpReceiveResult receiveResult = await udpClient.ReceiveAsync();
                string receivedQuote = System.Text.Encoding.UTF8.GetString(receiveResult.Buffer);

                System.Console.WriteLine($"Quote of the Day: {receivedQuote}");
            } // End Using udpClient 

        } // End Task Test 


    } // End Class UdpQOTDClient 


} // End Namespace 
