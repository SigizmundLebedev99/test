namespace test.Models{
    struct Circle{
        public double X;
        public double Y;
        public double Radius;
        public Circle(double x, double y, double r){
            this.X = x;
            this.Y = y;
            this.Radius = r;
        }

        public static implicit operator Circle ((double, double, double) stat){
            return new Circle(stat.Item1, stat.Item2, stat.Item3);
        }

        public static implicit operator Point (Circle circle){
            return new Point(circle.X, circle.Y);
        }
    }

    struct Point{
        public double X;
        public double Y;
        public Point(double x, double y){
            this.X = x;
            this.Y = y;
        }

        public static implicit operator Point ((double, double) stat){
            return new Point(stat.Item1, stat.Item2);
        }
    }
}