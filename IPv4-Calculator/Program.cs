namespace IPv4_Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("IP-Address: ");
            var ipAddress = Console.ReadLine();
            Console.Write("Subnetmask: ");
            var snm = Console.ReadLine();
            
            var IpCalc = new BitIpAddressCalc(ipAddress, snm);
            Console.WriteLine(Environment.NewLine + IpCalc.ToString());
        }
    }
}
