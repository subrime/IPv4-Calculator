namespace IPv4_Calculator.DAL
{
    public class SubnetMask : Octet
    {
        public SubnetMask(uint subnetMask = 0, uint cidr = 0)
        {
            CIDR = cidr;
            if (subnetMask != 0 && IsContiguous(subnetMask)) Subnetmask = subnetMask;
            else Subnetmask = uint.MaxValue << (int)(32 - CIDR);
        }
        public SubnetMask(string subnetMask = null, string cidr = null)
        {
            if (cidr != null && uint.TryParse(cidr, out var parsedCidr))
            {
                CIDR = parsedCidr;
                Subnetmask = uint.MaxValue << (int)(32 - CIDR);
                return;
            }

            var parsedSubnetMask = ParseOctet(subnetMask);
            if (!IsContiguous(parsedSubnetMask)) return;
            Subnetmask = parsedSubnetMask;
        }

        public uint Subnetmask { get; set; }
        public uint CIDR { get; set; }

        private bool IsContiguous(uint mask)
        {
            CIDR = 0;
            for (; mask != 0; mask <<= 1)
            {
                CIDR++;
                if ((mask & (1 << 31)) == 0)
                    return false; // Highest bit is now zero, but mask is non-zero.
            }
            return true; // Mask was, or became 0.
        }

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
