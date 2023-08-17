using System;
using System.Collections.Generic;
using UnityEngine;

namespace PtsImporter
{
    public enum MeshShape
    {
        Quad,
        Tetrahedron
    }
    public class Generator
    {
        public static void CreateGameObject(Point[] points, MeshShape meshShape, float pointSize, int maxPointCount)
        {
            var meshes = new List<Mesh>();
            foreach (var point in points)
            {
                switch (meshShape)
                {
                    case MeshShape.Quad:
                        meshes.Add(CreateQuad(point.potition, point.color, pointSize));
                        break;
                    case MeshShape.Tetrahedron:
                        meshes.Add(CreateTetrahedron(point.potition, point.color, pointSize));
                        break;
                }
            }
            var reducedMeshes = CombineMeshes(meshes.ToArray());
            var rootGameObject = new GameObject("Cloud Points");
            var count = 0;
            var material = new Material(Shader.Find("Unlit/VertexColor"));
            foreach (var mesh in reducedMeshes)
            {
                var gameObject = new GameObject($"Mesh {count}");
                var meshFilter = gameObject.AddComponent<MeshFilter>();
                meshFilter.mesh = mesh;
                var meshRenderer = gameObject.AddComponent<MeshRenderer>();
                meshRenderer.material = material;
                gameObject.transform.parent = rootGameObject.transform;
                count++;
            }
        }

        static Mesh[] CombineMeshes(Mesh[] meshes)
        {
            var reducedMeshes = new List<Mesh>();
            for (int i = 0; i < meshes.Length; i += 8000)//only allows up to 65535 vertices per mesh
            {
                var meshCount = Math.Min(meshes.Length - i, 8000);
                var combine = new CombineInstance[meshCount];
                var mesh = new Mesh();
                for (int j = 0; j < meshCount; j++)
                {
                    combine[j].mesh = meshes[i + j];
                }
                mesh.CombineMeshes(combine, true, false);
                reducedMeshes.Add(mesh);
            }
            return reducedMeshes.ToArray();
        }

        //点を間引く機能もいる
        
        static Mesh CreateQuad( Vector3 position, Color vertexColor, float size )
        {
            var halfSize  = size / 2;

            var vertices = new[]
            {
                position + new Vector3( -halfSize, -halfSize ),
                position + new Vector3( -halfSize, +halfSize ),
                position + new Vector3( +halfSize, +halfSize ),
                position + new Vector3( +halfSize, -halfSize ),
            };

            var uv = new[]
            {
                new Vector2( 0, 0 ),
                new Vector2( 0, 1 ),
                new Vector2( 1, 1 ),
                new Vector2( 1, 0 ),
            };

            var triangles = new[]
            {
                0, 1, 3,
                1, 2, 3,
            };
            
            var colors = new[]
            {
                vertexColor,
                vertexColor,
                vertexColor,
                vertexColor,
            };

            var mesh = new Mesh
            {
                vertices  = vertices,
                uv        = uv,
                triangles = triangles,
                colors = colors,
            };

            return mesh;
        }
        
        static Mesh CreateTetrahedron( Vector3 position, Color vertexColor, float size )
        {
            var halfSize  = size / 2;
            var sqrtSuze = -halfSize/Mathf.Sqrt(3);

            var vertices = new[]
            {
                position + new Vector3( 0, sqrtSuze, halfSize ),
                position + new Vector3( -halfSize, sqrtSuze, sqrtSuze ),
                position + new Vector3( +halfSize, sqrtSuze, sqrtSuze ),
                position + new Vector3( 0, halfSize, 0 ),
            };
            
            var halfSqrtSize = -0.5f/Mathf.Sqrt(3);
            var uv = new[]
            {
                new Vector2( 0.5f, 0 ),
                new Vector2( 0, halfSqrtSize ),
                new Vector2( 1, halfSqrtSize ),
                new Vector2( 0.5f, halfSqrtSize ),
            };

            var triangles = new[]
            {
                0, 3, 1,
                1, 3, 2,
                0, 1, 2,
                0, 2, 3,
            };
            
            var colors = new[]
            {
                vertexColor,
                vertexColor,
                vertexColor,
                vertexColor,
            };

            var mesh = new Mesh
            {
                vertices  = vertices,
                uv        = uv,
                triangles = triangles,
                colors = colors,
            };

            return mesh;
        }
    }
}
