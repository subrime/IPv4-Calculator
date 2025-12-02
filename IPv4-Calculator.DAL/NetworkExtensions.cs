namespace IPv4_Calculator.DAL
{
    public static class NetworkExtensions
    {
        public static bool IsContiguous(this uint mask)
        {
            for (; mask != 0; mask <<= 1)
            {
                if ((mask & (1 << 31)) == 0)
                    return false; // Highest bit is now zero, but mask is non-zero.
            }
            return true; // Mask was, or became 0.
        }

        public static uint GetCidr(this uint mask)
        {
            var cidr = uint.MinValue;
            for (; mask != 0; mask <<= 1)
            {
                cidr++;
            }
            return cidr;
        }
    }
}
