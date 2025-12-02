using IPv4_Calculator.DAL;
using System.Threading.Channels;

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

            Console.Write("\n\nHow to Split (Hosts[h] | Subnets[s]): ");
            var splitType = Console.ReadLine();

            Console.WriteLine("\nSplit value: ");
            if (!uint.TryParse(Console.ReadLine(), out var split))
            {
                Console.WriteLine("Couldn't convert the value!");
                return;
            }

            if (splitType == "s")
            {
                Console.WriteLine("\n------ Networks ------");
                Console.WriteLine(string.Join("\n\n", network / split));
            }
            else if (splitType == "h")
            {
                Console.WriteLine("\n------ Networks ------");
                Console.WriteLine(string.Join("\n\n", network * split));
            }
            else
            {
                Console.WriteLine("Couln't find how to split the subnet!");
            }
        }
    }
}
