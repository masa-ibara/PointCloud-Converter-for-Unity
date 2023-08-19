using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace PointCloudConverter
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
                    continue;
                }
                if (splitted.Length < 7) 
                {
                    continue;
                }
                var intensity = int.Parse(splitted[3], CultureInfo.InvariantCulture);
                var r = int.Parse(splitted[4], CultureInfo.InvariantCulture);
                var g = int.Parse(splitted[5], CultureInfo.InvariantCulture);
                var b = int.Parse(splitted[6], CultureInfo.InvariantCulture);
                points.Add(new Point(ParseVector3(splitted), (float)intensity / 255, new Color((float)r / 255, (float)g / 255, (float)b / 255)));
            }
            return new Pts(pointCount, points.ToArray());
        }
        
        public static Ptx ReadPtxFile(string path)
        {
            var lines = File.ReadAllLines(path);
            var points = new List<Point>();
            var rows = 0;
            var columns = 0;
            var scannerPosition = new Vector3();
            var scannerAxisX = new Vector3();
            var scannerAxisY = new Vector3();
            var scannerAxisZ = new Vector3();
            var scannerTransform = new Matrix4x4();
            var matrix4x4 = new Vector4[4]; 
            var lineCount = 0;
            
            foreach (var line in lines)
            {
                lineCount++;
                var splitted = line.Split(' ');
                if (lineCount == 1)
                {
                    rows = int.Parse(splitted[0], CultureInfo.InvariantCulture);
                    continue;
                }
                if (lineCount == 2)
                {
                    columns = int.Parse(splitted[0], CultureInfo.InvariantCulture);
                    continue;
                }
                if (lineCount == 3)
                {
                    scannerPosition = ParseVector3(splitted);
                    continue;
                }
                if (lineCount == 4)
                {
                    scannerAxisX = ParseVector3(splitted);
                    continue;
                }
                if (lineCount == 5)
                {
                    scannerAxisY = ParseVector3(splitted);
                    continue;
                }
                if (lineCount == 6)
                {
                    scannerAxisZ = ParseVector3(splitted);
                    continue;
                }
                if (lineCount == 7)
                {
                    matrix4x4[0] = ParseVector4(splitted);
                    continue;
                }
                if (lineCount == 8)
                {
                    matrix4x4[1] = ParseVector4(splitted);
                    continue;
                }
                if (lineCount == 9)
                {
                    matrix4x4[2] = ParseVector4(splitted);
                    continue;
                }
                if (lineCount == 10)
                {
                    matrix4x4[3] = ParseVector4(splitted);
                    continue;
                }
                if (splitted.Length < 7) 
                {
                    continue;
                }
                var intensity = float.Parse(splitted[3], CultureInfo.InvariantCulture);
                var r = int.Parse(splitted[4], CultureInfo.InvariantCulture);
                var g = int.Parse(splitted[5], CultureInfo.InvariantCulture);
                var b = int.Parse(splitted[6], CultureInfo.InvariantCulture);
                points.Add(new Point(ParseVector3(splitted), intensity, new Color((float)r / 255, (float)g / 255, (float)b / 255)));
            }
            scannerTransform = new Matrix4x4(matrix4x4[0], matrix4x4[1], matrix4x4[2], matrix4x4[3]);
            return new Ptx(rows, columns, scannerPosition, scannerAxisX, scannerAxisY, scannerAxisZ, scannerTransform, points.ToArray());
        }
        
        static Vector3 ParseVector3(string[] splitted)
        {
            // x z y
            return new Vector3(float.Parse(splitted[0], CultureInfo.InvariantCulture), float.Parse(splitted[2], CultureInfo.InvariantCulture), float.Parse(splitted[1], CultureInfo.InvariantCulture));
        }
        static Vector4 ParseVector4(string[] splitted)
        {
            return new Vector4(float.Parse(splitted[0], CultureInfo.InvariantCulture), float.Parse(splitted[1], CultureInfo.InvariantCulture), float.Parse(splitted[2], CultureInfo.InvariantCulture), float.Parse(splitted[3], CultureInfo.InvariantCulture));
        }
    }
}
