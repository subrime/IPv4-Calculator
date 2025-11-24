using System.Net.WebSockets;

namespace IPv4_Calculator
{
    internal class BitIpAddressCalc
    {
        public BitIpAddressCalc(string ipAddress, int cidr)
        {
            IpAddress = GetIp(ipAddress);
            SubnetMask = uint.MaxValue << (32 - cidr);
            Calculate();
        }

        public BitIpAddressCalc(string ipAddress, string snm)
        {
            IpAddress = GetIp(ipAddress);
            SubnetMask = GetIp(snm);
            Calculate();
        }

        public uint IpAddress { get; set; }
        public uint SubnetMask { get; set; }

        public uint NetzID { get; set; }
        public uint BroadCast { get; set; }
        public uint FirstHost { get; set; }
        public uint LastHost { get; set; }

        public uint TotalHosts { get; set; }

        public override string ToString()
        {
            var rv = nameof(NetzID) + ": " + GetString(NetzID) + '\n';
            rv += nameof(BroadCast) + ": " + GetString(BroadCast) + '\n';
            rv += nameof(FirstHost) + ": " + GetString(FirstHost) + '\n';
            rv += nameof(LastHost) + ": " + GetString(LastHost) + '\n';
            rv += nameof(TotalHosts) + ": " + TotalHosts;
            return rv;
        }

        private void Calculate()
        {
            NetzID = IpAddress & SubnetMask;
            BroadCast = NetzID + ~SubnetMask;

            FirstHost = NetzID + 1;
            LastHost = BroadCast - 1;
            TotalHosts = LastHost - FirstHost;
        }

        private string GetString(uint ip)
        {
            return string.Join('.', BitConverter.GetBytes(ip).Reverse());
        }

        private uint GetIp(string ipAddress)
        {
            var splitted = ipAddress.Split('.');
            uint ip = 0;
            for (var i = 0; i <= 3; i++)
            {
                if (!uint.TryParse(splitted[i], out var @byte)) return uint.MinValue;
                ip = (ip << 8) + @byte;
            }
            return ip;
        }
    }
}
