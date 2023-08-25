
namespace QuoteOfTheDay
{


    public class TcpQOTDServer
    {


        [System.ThreadStatic()]
        private static readonly System.Random random = new System.Random();


        // The RFC (865) recommends that: 
        //   There is no specific syntax for the quote.
        //   It is recommended that itbe limited to the ASCII printing characters,
        //   space, carriage return, and line feed.
        //   The quote may be just one or up to several lines,
        //   but it should be less than 512 characters.
        // Protocol is TCP/UPD on port 17
        // https://www.rfc-editor.org/rfc/rfc865.txt
        private static readonly string[] Quotes =
        {
            "Life is what happens when you're busy making other plans. - John Lennon",
            "The only limit to our realization of tomorrow will be our doubts of today. - Franklin D. Roosevelt",
            "The way to get started is to quit talking and begin doing. - Walt Disney",
            "In three words I can sum up everything I've learned about life: it goes on. - Robert Frost",
            "The future belongs to those who believe in the beauty of their dreams. - Eleanor Roosevelt",
            "It's not whether you get knocked down, it's whether you get up. - Vince Lombardi"
        };


        public static async System.Threading.Tasks.Task StartAsync()
        {
            int port = 17; // QOTD port

            System.Net.Sockets.TcpListener listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Any, port);
            listener.Start();
            System.Console.WriteLine($"TCP-QOTD server listening on port {port}");

            while (true)
            {
                using (System.Net.Sockets.TcpClient client = await listener.AcceptTcpClientAsync())
                {
                    System.Console.WriteLine("TCP-Client connected!");
                    await SendQuoteToClient(client);
                } // End Using client 

            } // Whend 

        } // End Task StartAsync


        private static string GetRandomQuote()
        {
            int index = random.Next(Quotes.Length);
            return Quotes[index];
        } // End Function GetRandomQuote 


        private static async System.Threading.Tasks.Task SendQuoteToClient(System.Net.Sockets.TcpClient client)
        {
            string quote = GetRandomQuote() + "\n";

            using (System.Net.Sockets.NetworkStream stream = client.GetStream())
            {
                // Note: QOTD is ASCII, but UTF8 is backwards-compatible 
                byte[] data = System.Text.Encoding.UTF8.GetBytes(quote);
                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync();
            } // End Using stream 

        } // End Task SendQuoteToClient 


    } // End Class TcpQOTDServer 


} // End Namespace QuoteOfTheDay 
