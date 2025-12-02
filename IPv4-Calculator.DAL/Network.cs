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
            BroadCast = NetID + ~SubnetMask.Subnetmask;

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

        public IEnumerable<string> IsValid()
        {
            return GetType().GetProperties().Where(x => x.GetValue(this) is Octet)
                                            .Select(x =>
                                            {
                                                if (x.GetValue(this) is not Octet octet) return null;

                                                var ip = uint.MinValue;
                                                if (octet is IpAddress ipAddress)
                                                {
                                                    ip = ipAddress.IPv4_Address;
                                                }
                                                else if (octet is SubnetMask subnetmask)
                                                {
                                                    ip = subnetmask.Subnetmask;
                                                }

                                                if (ip != 0) return null;
                                                return octet.IsParsed ? $"Couldn't parse the value '{octet.OriginalValue}' in {x.Name}!"
                                                                      : $"The value '{ip}' in {x.Name} is not valid!"; ;
                                            })
                                            .Where(x => x != null).Cast<string>();
        }

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
