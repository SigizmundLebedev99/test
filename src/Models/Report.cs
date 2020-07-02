using System.Collections.Generic;

namespace test.Models{
    class Report{
        public Point Transmitter1{get;set;}
        public Point Transmitter2{get;set;}
        public Point Transmitter3{get;set;}

        public IEnumerable<(double, double, double)> Dimensions{get;set;}

    }
}