using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace PtsImporter
{
    public class Window : EditorWindow
    {
        MeshShape _meshShape = MeshShape.Tetrahedron;
        int _maxPointsCount = 0;
        
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
                    Generator.CreateGameObject(Importer.ReadPtsFile(path).points, _meshShape, 0.01f, _maxPointsCount);
                }
                else if (path.EndsWith("ptx"))
                {
                    Generator.CreateGameObject(Importer.ReadPtxFile(path).points, _meshShape, 0.01f, _maxPointsCount);
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

