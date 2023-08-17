using UnityEngine;

namespace PtsImporter
{
    public class Point
    {
        public Vector3 potition;
        public float intensity; //Max 1.0f
        public Color color;
        
        public Point(Vector3 potition, float intensity, Color color)
        {
            this.potition = potition;
            this.intensity = intensity;
            this.color = color;
        }
    }

    public class Pts
    {
        public int pointCount;
        public Point[] points;
        
        public Pts(int pointCount, Point[] points)
        {
            this.pointCount = pointCount;
            this.points = points;
        }
    }
    
    public class Ptx
    {
        public int rows;
        public int columns;
        public Point[] points;
        
        public Ptx(int rows, int columns, Point[] points)
        {
            this.rows = rows;
            this.columns = columns;
            this.points = points;
        }
    }
}
