using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackAChild.Helpers;

namespace TrackAChild.Models
{
    public class StringWrapper : Observable
    {
        private string stringContent;
        public string StringContent
        {
            get { return stringContent; }
            set { Set(ref stringContent, value); }
        }

        private decimal tempLatitude;
        public decimal TempLatitude
        {
            get { return tempLatitude; }
            set { Set(ref tempLatitude, value); }
        }

        private decimal tempLongitude;
        public decimal TempLongitude
        {
            get { return tempLongitude; }
            set { Set(ref tempLongitude, value); }
        }
    }
}
