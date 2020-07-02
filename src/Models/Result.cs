using System.Collections.Generic;

namespace test.Models{
    class Result{
        public Point Transmitter1{get;set;}
        public Point Transmitter2{get;set;}
        public Point Transmitter3{get;set;}

        public IEnumerable<Coordinate> Coordinates{get;set;}
    }

    struct Coordinate{
        public bool Success{get;set;}
        public Point Payload{get;set;}
    }
}