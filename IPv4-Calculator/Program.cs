using IPv4_Calculator.DAL;

namespace IPv4_Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("IP-Address: ");
            var ipAddress = Console.ReadLine();

            var subnetmask = string.Empty;
            if (!ipAddress.Contains('/'))
            {
                Console.Write("Subnetmask: ");
                subnetmask = Console.ReadLine();
            }
            Console.WriteLine();

            var network = new Network(ipAddress, subnetmask);
            if (network.IsValid().Any())
            {
                foreach (var error in network.IsValid())
                {
                    Console.WriteLine(error);
                }
                return;
            }

            Console.WriteLine("------ Network ------");
            Console.WriteLine(network);

            Console.Write("\n\nSplit: ");
            if (uint.TryParse(Console.ReadLine(), out var split))
            {
                Console.WriteLine("\n------ Networks ------");
                Console.WriteLine(string.Join("\n\n", network / split));
            }
        }
    }
}
