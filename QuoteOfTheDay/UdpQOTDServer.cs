
namespace QuoteOfTheDay
{


    class UdpQOTDServer
    {


        private static readonly string[] Quotes =
        {
            "Life is what happens when you're busy making other plans. - John Lennon",
            "The only limit to our realization of tomorrow will be our doubts of today. - Franklin D. Roosevelt",
            "The way to get started is to quit talking and begin doing. - Walt Disney",
            "In three words I can sum up everything I've learned about life: it goes on. - Robert Frost",
            "The future belongs to those who believe in the beauty of their dreams. - Eleanor Roosevelt",
            "It's not whether you get knocked down, it's whether you get up. - Vince Lombardi"
        };


        [System.ThreadStatic()]
        private static readonly System.Random random = new System.Random();


        private static string GetRandomQuote()
        {
            int index = random.Next(Quotes.Length);
            return Quotes[index];
        } // End Function GetRandomQuote 


        public static async System.Threading.Tasks.Task StartAsync()
        {
            int port = 17; // QOTD port

            using (System.Net.Sockets.UdpClient udpServer = new System.Net.Sockets.UdpClient(port))
            {
                System.Console.WriteLine($"UDP-QOTD server listening on port {port}");

                while (true)
                {
                    System.Net.Sockets.UdpReceiveResult receiveResult = await udpServer.ReceiveAsync();
                    System.Console.WriteLine("UDP-Client connected!");

                    System.Net.IPEndPoint clientEndPoint = receiveResult.RemoteEndPoint;
                    string quote = GetRandomQuote() + "\n"; 
                    byte[] sendBytes = System.Text.Encoding.UTF8.GetBytes(quote); 
                    await udpServer.SendAsync(sendBytes, sendBytes.Length, clientEndPoint);
                } // Whend 

            } // End Using udpServer 

        } // End Task Test 


    } // End Class UdpQOTDServer 


} // End Namespace 
