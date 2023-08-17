using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace PtsImporter
{
    public class Importer
    {
        public static Pts ReadPtsFile(string path)
        {
            var lines = File.ReadAllLines(path);
            var points = new List<Point>();
            var pointCount = 0;
            var lineCount = 0;
            foreach (var line in lines)
            {
                lineCount++;
                var splitted = line.Split(' ');
                if (lineCount == 1)
                {
                    pointCount = int.Parse(splitted[0], CultureInfo.InvariantCulture);
                }
                if (splitted.Length < 7) 
                {
                    continue;
                }
                var x = float.Parse(splitted[0], CultureInfo.InvariantCulture);
                var y = float.Parse(splitted[1], CultureInfo.InvariantCulture);
                var z = float.Parse(splitted[2], CultureInfo.InvariantCulture);
                var intensity = int.Parse(splitted[3], CultureInfo.InvariantCulture);
                var r = int.Parse(splitted[4], CultureInfo.InvariantCulture);
                var g = int.Parse(splitted[5], CultureInfo.InvariantCulture);
                var b = int.Parse(splitted[6], CultureInfo.InvariantCulture);
                points.Add(new Point(new Vector3(x, z, y), (float)intensity / 255, new Color((float)r / 255, (float)g / 255, (float)b / 255)));
            }
            return new Pts(pointCount, points.ToArray());
        }
        
        public static Ptx ReadPtxFile(string path)
        {
            var lines = File.ReadAllLines(path);
            var points = new List<Point>();
            var rows = 0;
            var columns = 0;
            var lineCount = 0;
            foreach (var line in lines)
            {
                lineCount++;
                var splitted = line.Split(' ');
                if (lineCount == 1)
                {
                    rows = int.Parse(splitted[0], CultureInfo.InvariantCulture);
                }
                if (lineCount == 2)
                {
                    columns = int.Parse(splitted[0], CultureInfo.InvariantCulture);
                }
                if (splitted.Length < 7) 
                {
                    continue;
                }
                var x = float.Parse(splitted[0], CultureInfo.InvariantCulture);
                var y = float.Parse(splitted[1], CultureInfo.InvariantCulture);
                var z = float.Parse(splitted[2], CultureInfo.InvariantCulture);
                var intensity = float.Parse(splitted[3], CultureInfo.InvariantCulture);
                var r = int.Parse(splitted[4], CultureInfo.InvariantCulture);
                var g = int.Parse(splitted[5], CultureInfo.InvariantCulture);
                var b = int.Parse(splitted[6], CultureInfo.InvariantCulture);
                points.Add(new Point(new Vector3(x, z, y), intensity, new Color((float)r / 255, (float)g / 255, (float)b / 255)));
            }
            return new Ptx(rows, columns, points.ToArray());
        }
    }
}
