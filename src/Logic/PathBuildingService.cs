using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using test.Models;
using static System.Math;

namespace test.Logic{
    class PathBuildingService{
        //скорость 1000 км/сек
        const double SIGNAL_SPEED = 1000;
        const double ACCURACY = 0.05;
        public Result BuildPath(Report report){
            var result = report.Dimensions
            .Select(a=>{
                var (d1,d2,d3) = a;
                var c1 = (report.Transmitter1.X, report.Transmitter1.Y, TimeToRadius(d1));
                var c2 = (report.Transmitter2.X, report.Transmitter2.Y, TimeToRadius(d2));
                var c3 = (report.Transmitter3.X, report.Transmitter3.Y, TimeToRadius(d3));
                if(GetPosition(c1,c2,c3, out Point? pos))
                    return new Coordinate(){
                        Payload = (Round(pos.Value.X, 4), Round(pos.Value.Y, 4)),
                        Success = true
                    };
                else
                    return new Coordinate(){
                        Success = false
                    };
            });
            return new Result(){
                Transmitter1 = report.Transmitter1,
                Transmitter2 = report.Transmitter2,
                Transmitter3 = report.Transmitter3,
                Coordinates = result
            };
        }

        double TimeToRadius(double time){
            //время в миллисекундах
            var radius = time / 1000 * SIGNAL_SPEED;
            return radius;
        }

        bool GetPosition(Circle c1, Circle c2, Circle c3, out Point? result){
            result = null;
            var success = Intersection(c1,c2, out Point? p1, out Point? p2);
            if(!success)
                return false;
            var dist1 = Distance(p1.Value, c3);
            var dist2 = Distance(p2.Value, c3);
            if(Abs(dist1-c3.Radius) < ACCURACY){
                result = p1.Value;
                return true;
            } 
            if(Abs(dist2 - c3.Radius) < ACCURACY){
                result = p2.Value;
                return true;
            }
                
            return false;
        }

        bool Intersection(Circle c1, Circle c2, out Point? p1, out Point? p2){
            p1 = p2 = null;
            var dx = c2.X - c1.X;
            var dy = c2.Y - c1.Y;
            var dist = Sqrt((dx*dx) + (dy*dy));
            if(dist > (c1.Radius + c2.Radius))
                return false;
            if(dist < Abs(c1.Radius - c2.Radius))
                return false;
            var a = ((c1.Radius * c1.Radius) - (c2.Radius * c2.Radius) + (dist*dist))/(2.0*dist);
            var x2 = c1.X + (dx * a/dist);
            var y2 = c1.Y + (dy * a/dist);
            var h = Sqrt(Abs(c1.Radius * c1.Radius - a*a));
            var rx = -dy * (h/dist);
            var ry = dx * (h/dist);
            p1 = (x2 + rx, y2 + ry);
            p2 = (x2 - rx, y2 - ry);
            return true;
        }

        double Distance(Point p1, Point p2){
            var dx = p1.X-p2.X;
            var dy = p1.Y - p2.Y;
            return Sqrt(dx*dx + dy*dy);
        }
    }
}