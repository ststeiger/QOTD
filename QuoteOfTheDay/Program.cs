
namespace QuoteOfTheDay
{


    public class Program
    {


        public static async System.Threading.Tasks.Task<int> Main(string[] args)
        {
            _ = TcpQOTDServer.StartAsync();
            _ = UdpQOTDServer.StartAsync();

            System.Console.WriteLine(System.Environment.NewLine);

            await TcpQOTDClient.Test();
            await UdpQOTDClient.Test();

            System.Console.WriteLine(" --- Press any key to continue --- ");
            return 0; 
        } // End Task Main 


    } // End Class Program 


} // End Namespace 
