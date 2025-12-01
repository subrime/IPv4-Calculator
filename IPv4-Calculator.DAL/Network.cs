namespace IPv4_Calculator.DAL
{
    public class Network
    {
        public Network(string ipAddress, string subnetMask)
        {
            IpAddress = new(ipAddress.Split('/')[0]);

            if (!ipAddress.Contains('/')) SubnetMask = new(subnetMask);
            else SubnetMask = new(cidr: ipAddress.Split('/')[1]);

            NetID = new(IpAddress.IPv4_Address & SubnetMask.Subnetmask);
            BroadCast = NetID + ~SubnetMask;

            FirstHost = NetID + 1;
            LastHost = BroadCast - 1;
            TotalHosts = LastHost.IPv4_Address - FirstHost.IPv4_Address;
        }

        public IpAddress IpAddress { get; set; }
        public SubnetMask SubnetMask { get; set; }

        public IpAddress NetID { get; set; }
        public IpAddress BroadCast { get; set; }
        public IpAddress FirstHost { get; set; }
        public IpAddress LastHost { get; set; }

        public uint TotalHosts { get; set; }

        public override string ToString()
        {
            return string.Join('\n', GetType().GetProperties().Select(x => $"{x.Name}: {x.GetValue(this)}"));
        }

        public static IEnumerable<Network> operator /(Network network, uint divideBy)
        {
            var newSubnetMask = network.SubnetMask / divideBy;
            var networks = new List<Network>();
            for (uint i = 1; i <= divideBy; i++)
            {
                var increaseValue = ~newSubnetMask.Subnetmask * i;
                var netId = (network.NetID.IPv4_Address + increaseValue) & newSubnetMask.Subnetmask;
                networks.Add(new(new IpAddress(netId + 1).ToString(), newSubnetMask.ToString()));
            }
            return networks;
        }
    }
}
