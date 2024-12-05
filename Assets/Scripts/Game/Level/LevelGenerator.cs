using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RunnerGame.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] Transform levelParent;

        LevelGenerationSettingsSO _generationSettings;

        Transform _roadParent;
        Transform _fruitsParent;
        Transform _obstaclesParent;

        Transform _referenceObject;

        Queue<Transform> _roadSegments = new();

        void Awake()
        {
            if (levelParent == null)
                levelParent = transform.parent;

            _roadParent = GameObjectExtensions.NewGameObjectAsChild("Road", levelParent).transform;
            _fruitsParent = GameObjectExtensions.NewGameObjectAsChild("Fruits", levelParent).transform;
            _obstaclesParent = GameObjectExtensions.NewGameObjectAsChild("Obstacles", levelParent).transform;
        }

        void Update()
        {
            UpdateRoadSegments();

        }

        private void UpdateRoadSegments()
        {
            float minPos = _referenceObject.position.z + _generationSettings.ReferenceObjectPositionOffset;
            while (_roadSegments.Peek().position.z < minPos)
            {
                var lastRoadSegment = _roadSegments.Dequeue();
                var newPos = lastRoadSegment.transform.position;
                newPos.z += _generationSettings.LevelLength;
                lastRoadSegment.transform.position = newPos;
                _roadSegments.Enqueue(lastRoadSegment);
            }
        }

        public void SetGenerationSettings(LevelGenerationSettingsSO settings)
        {
            _generationSettings = settings;
        }

        public void SetReferenceObject(Transform reference)
        {
            _referenceObject = reference;
        }

        public void InitGenerate()
        {
            float currentPos = _referenceObject.position.z + _generationSettings.ReferenceObjectPositionOffset;
            float endPos = currentPos + _generationSettings.LevelLength;
            while (currentPos < endPos)
            {
                var roadSegment = Instantiate(_generationSettings.RoadSegmentPrefab, _roadParent);
                roadSegment.transform.position = roadSegment.transform.position.SetZ(currentPos);
                currentPos += _generationSettings.RoadSegmentLength;
                _roadSegments.Enqueue(roadSegment.transform);
            }
        }
    }
}