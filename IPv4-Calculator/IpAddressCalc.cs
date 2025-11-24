namespace IPv4_Calculator
{
    internal class IpAddressCalc
    {
        public IpAddressCalc(string ipAddress, string snm)
        {
            IpAddress = GetBytes(ipAddress);
            Subnetmask = GetBytes(snm);
            if (IpAddress == Array.Empty<byte>() || Subnetmask == Array.Empty<byte>()) return;

            NetzID = new byte[4];
            BroadCast = new byte[4];

            Calculate();
        }

        public byte[] IpAddress { get; set; }
        public byte[] Subnetmask { get; set; }

        public byte[] NetzID { get; set; } = [];
        public byte[] BroadCast { get; set; } = [];
        public byte[] FirstHost { get; set; } = [];
        public byte[] LastHost { get; set; } = [];

        public int TotalHosts { get; set; }

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
            for (int i = 0; i <= 3; i++)
            {
                NetzID[i] = (byte)(IpAddress[i] & Subnetmask[i]);
                BroadCast[i] = (byte)(IpAddress[i] | (Subnetmask[i] ^ byte.MaxValue));
            }
            FirstHost = NetzID.ToArray();
            FirstHost[3] = (byte)(FirstHost[3] + 1);
            LastHost = BroadCast.ToArray();
            LastHost[3] = (byte)(LastHost[3] - 1);
            TotalHosts = LastHost[3] - FirstHost[3];
        }

        private byte[] GetBytes(string text)
        {
            var splitted = text.Split('.');
            var rv = new byte[splitted.Length];
            for (int i = 0; i < splitted.Length; i++)
            {
                if (!byte.TryParse(splitted[i], out rv[i])) return [];
            }
            return rv;
        }

        private string GetString(byte[] address)
        {
            return string.Join('.', address);
        }
    }
}
