namespace IPv4_Calculator.DAL
{
    public class SubnetMask : IpAddress
    {
        public SubnetMask(uint subnetMask = 0, uint cidr = 0) : base(subnetMask)
        {
            if (subnetMask != 0 && subnetMask.IsContiguous()) Subnetmask = subnetMask;
            else Subnetmask = uint.MaxValue << (int)(32 - CIDR);
            CIDR = cidr == 0 ? Subnetmask.GetCidr() : cidr;
        }
        public SubnetMask(string subnetMask = null, string cidr = null) : base(subnetMask)
        {
            if (cidr != null && uint.TryParse(cidr, out var parsedCidr))
            {
                CIDR = parsedCidr;
                Subnetmask = uint.MaxValue << (int)(32 - CIDR);
                return;
            }

            var parsedSubnetMask = ParseOctet(subnetMask);
            if (!parsedSubnetMask.IsContiguous()) return;
            Subnetmask = parsedSubnetMask;
            CIDR = Subnetmask.GetCidr();
        }

        public uint Subnetmask { get; set; }
        public uint CIDR { get; set; }

        public static SubnetMask operator /(SubnetMask mask, uint divideBy)
        {
            return new(~(~mask.Subnetmask / divideBy));
        }

        public override string ToString()
        {
            return string.Join('.', BitConverter.GetBytes(Subnetmask).Reverse());
        }
    }
}
