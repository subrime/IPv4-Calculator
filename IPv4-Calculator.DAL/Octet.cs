namespace IPv4_Calculator.DAL
{
    public class Octet
    {
        private uint _parsedValue;

        public Octet() { }

        internal bool IsParsed { get; set; }
        internal string OriginalValue { get; set; }
        protected uint ParseOctet(string octet)
        {
            IsParsed = true;
            OriginalValue = octet;
            var splitted = octet?.Split('.');
            if (splitted?.Length != 4) return uint.MinValue;
            uint parsedOctet = 0;
            for (var i = 0; i < 4; i++)
            {
                if (!uint.TryParse(splitted[i], out var @byte)) return uint.MinValue;
                parsedOctet = (parsedOctet << 8) + @byte;
            }
            return _parsedValue = parsedOctet;
        }

        public override string ToString()
        {
            return string.Join('.', BitConverter.GetBytes(_parsedValue).Reverse());
        }
    }
}
