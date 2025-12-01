namespace IPv4_Calculator.DAL
{
    public class IpAddress : Octet
    {
        public IpAddress(string ipAddress)
        {
            IPv4_Address = ParseOctet(ipAddress);
        }
        public IpAddress(uint ipAddress)
        {
            IPv4_Address = ipAddress;
        }

        public uint IPv4_Address { get; set; }

        public static IpAddress operator +(IpAddress ipAddress, uint number)
        {
            return new IpAddress(ipAddress.IPv4_Address + number);
        }

        public static IpAddress operator -(IpAddress ipAddress, uint number)
        {
            return new IpAddress(ipAddress.IPv4_Address - number);
        }

        public override string ToString()
        {
            return string.Join('.', BitConverter.GetBytes(IPv4_Address).Reverse());
        }
    }
}
