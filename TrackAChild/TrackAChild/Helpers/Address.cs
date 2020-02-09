using System;

namespace TrackAChild.Helpers
{
    [Serializable]
    public class Address
    {
        public string road { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }
        public string state_district { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
    }
}
