namespace IPv4_Calculator.DAL
{
    public class Octet
    {
        private uint _value;

        public Octet() { }

        protected uint ParseOctet(string octet)
        {
            var splitted = octet?.Split('.');
            if (splitted?.Length == 4) return uint.MinValue;
            uint parsedOctet = 0;
            for (var i = 0; i < 4; i++)
            {
                if (!uint.TryParse(splitted[i], out var @byte)) return uint.MinValue;
                parsedOctet = (parsedOctet << 8) + @byte;
            }
            return _value = parsedOctet;
        }

        public static uint operator ~(Octet octet)
        {
            return ~octet._value;
        }

        public override string ToString()
        {
            return string.Join('.', BitConverter.GetBytes(_value).Reverse());
        }
    }
}
