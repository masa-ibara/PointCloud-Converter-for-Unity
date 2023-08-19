using UnityEngine;

namespace PointCloudConverter
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
        public Vector3 scannerRegisteredPosition;
        public Vector3 scannerRegisteredAxisX;
        public Vector3 scannerRegisteredAxisY;
        public Vector3 scannerRegisteredAxisZ;
        public Matrix4x4 scannerTransform;
        public Point[] points;
        
        public Ptx(int rows, int columns, Vector3 scannerRegisteredPosition, Vector3 scannerRegisteredAxisX, Vector3 scannerRegisteredAxisY, Vector3 scannerRegisteredAxisZ, Matrix4x4 scannerTransform, Point[] points)
        {
            this.rows = rows;
            this.columns = columns;
            this.points = points;
            this.scannerRegisteredPosition = scannerRegisteredPosition;
            this.scannerRegisteredAxisX = scannerRegisteredAxisX;
            this.scannerRegisteredAxisY = scannerRegisteredAxisY;
            this.scannerRegisteredAxisZ = scannerRegisteredAxisZ;
            this.scannerTransform = scannerTransform;
        }
    }
}
