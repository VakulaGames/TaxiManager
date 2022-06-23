namespace TaxiManager
{
    public struct Driver
    {
        public string Callsign { get; private set; }
        public string Name { get; private set; }
        public string Automobile { get; private set; }
        public float Reputation()
        {
            if(_reviewCount != 0)
            {
                return _starCount * 5 / _reviewCount;
            }
            else
            {
                return 0;
            }
        }

        private int _reviewCount;
        private int _starCount;

        public Driver(string callsign,string name, string automobile)
        {
            Callsign = callsign;
            Name = name;
            Automobile = automobile;
            _reviewCount = 0;
            _starCount = 0;
        }

        public void EditDriver(string callsign, string name, string automobile)
        {
            Callsign = callsign;
            Name = name;
            Automobile = automobile;
        }

        public void AddReview(int starCount)
        {
            _reviewCount++;
            _starCount += starCount;
        }
    }
}
