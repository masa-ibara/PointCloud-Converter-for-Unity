using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace PtsImporter
{
    public class Window : EditorWindow
    {
        int _maxPointsCount = 100000;
        MeshShape _meshShape = MeshShape.Tetrahedron;
        float _pointSize = 0.01f;
        
        [MenuItem("Assets/PTS PTX Importer")]
        public static void ShowWindow()
        {
            GetWindow<Window>("PTS/PTX Importer");
        }
        
        void OnEnable()
        {
            var root = rootVisualElement;

            var maxPointCountField = new IntegerField();
            maxPointCountField.label = "Max Point Count";
            maxPointCountField.tooltip = "0 means no limit";
            maxPointCountField.value = _maxPointsCount;
            maxPointCountField.RegisterValueChangedCallback(evt =>
            {
                _maxPointsCount = evt.newValue;
            });
            root.Add(maxPointCountField);
            
            var meshShapeField = new EnumField(MeshShape.Tetrahedron);
            meshShapeField.label = "Point Shape";
            meshShapeField.RegisterValueChangedCallback(evt =>
            {
                _meshShape = (MeshShape)evt.newValue;
            });
            root.Add(meshShapeField);
            
            var pointSizeField = new FloatField();
            pointSizeField.label = "Point Size";
            pointSizeField.value = _pointSize;
            pointSizeField.RegisterValueChangedCallback(evt =>
            {
                _pointSize = evt.newValue;
            });
            root.Add(pointSizeField);

            var readButton = new Button();
            readButton.text = "Read PTS/PTX File";
            readButton.style.height = 30;
            readButton.clicked += () =>
            {
                var path = EditorUtility.OpenFilePanelWithFilters("Open PTS/PTX File", "", new string[]
                {
                    "Point Cloud",
                    "pts,ptx"
                });
                if (path.EndsWith("pts"))
                {
                    var pts = Importer.ReadPtsFile(path);
                    Generator.CreateGameObject(pts.points, pts.pointCount, _meshShape, _pointSize, _maxPointsCount);
                }
                else if (path.EndsWith("ptx"))
                {
                    var ptx = Importer.ReadPtxFile(path);
                    Generator.CreateGameObject(ptx.points, ptx.rows * ptx.columns, _meshShape, _pointSize, _maxPointsCount);//This number of points is skeptical
                }
                else
                {
                    Debug.LogError("Invalid file type");
                }
            };
            root.Add(readButton);
        }

        void OnDisable()
        {
        }
    }
}

