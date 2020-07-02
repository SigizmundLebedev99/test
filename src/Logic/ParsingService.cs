using test.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace test.Logic{
    class ParsingService{
        public async Task<Report> Parse(Stream stream){
            var streamReader = new StreamReader(stream);
            var data = await streamReader.ReadToEndAsync();
            var rows = data.Split(Environment.NewLine).Where(str=>!String.IsNullOrWhiteSpace(str));
            if(rows.Count() == 0)
                throw new InvalidDataException("Файл пустой");
            var (p1,p2,p3) = GetTransmitters(rows.First());
            var dimensions = GetDimensions(rows.Skip(1));
            var result = new Report(){
                Transmitter1 = p1,
                Transmitter2 = p2,
                Transmitter3 = p3,
                Dimensions = dimensions
            };
            return result;
        }

        (Point, Point, Point) GetTransmitters(string str){
            double Map(Group g) => Double.Parse(g.Value.Replace('.',','));
            var regexStrings = 
                Enumerable.Range(1,3)
                .Select(i=>$@"(?<x{i}>[0-9]*\.?[0-9]*),(?<y{i}>[0-9]*\.?[0-9]*)");

            var regexString = String.Join(',', regexStrings.ToArray());
            var match = Regex.Match(str, regexString);

            if(!match.Success)
                throw new InvalidDataException();

            var p1 = (Map(match.Groups["x1"]), Map(match.Groups["y1"]));
            var p2 = (Map(match.Groups["x2"]), Map(match.Groups["y2"]));
            var p3 = (Map(match.Groups["x3"]), Map(match.Groups["y3"]));
            return (p1,p2, p3);
        }

        IEnumerable<(double, double, double)> GetDimensions(IEnumerable<string> rows){
            double Map(string str) => Double.Parse(str.Replace('.',','));
            foreach(var row in rows){
                var values = row.Split(',');
                if(values.Length != 3)
                    throw new InvalidDataException();
                yield return (Map(values[0]),Map(values[1]),Map(values[2]));
            }
        }
    }
}