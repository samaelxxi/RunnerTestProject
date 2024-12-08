using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DesignPatterns;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;


namespace RunnerGame.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] Transform levelParent;
        [SerializeField] EnvironmentGenerator env;

        LevelGenerationSettingsSO _generationSettings;

        Transform _roadParent;
        Transform _referenceObject;

        BinomialDistribution _obstacleDistribution;

        Queue<Transform> _roadSegments = new();
        List<TransformPoolable> _obstacles = new();
        List<FruitPoolable> _fruits = new();

        float _lastGC;
        int _lastSegmentIdx;


        void Awake()
        {
            if (levelParent == null)
                levelParent = transform.parent;

            _roadParent = GameObjectExtensions.NewGameObjectAsChild("Road", levelParent).transform;
            env.SetSettings(_generationSettings);
        }

        void Update()
        {
            UpdateRoadSegments();

            _obstacleDistribution.Probability += _generationSettings.ObstacleProbabilityAcceleration * Time.deltaTime;

            CollectGarbage();

            env.GenerateIfNeeded(_referenceObject.position.z);
        }

        void CollectGarbage()
        {
            if (Time.time - _lastGC > 2)  // TODO make better
            {
                List<TransformPoolable> toRemove = new();
                foreach (var obj in _obstacles)
                {
                    if (obj.transform.position.z < _referenceObject.transform.position.z + _generationSettings.ReferenceObjectRoadOffset)
                    {
                        toRemove.Add(obj);
                        obj.Release();
                    }
                }
                toRemove.ForEach(obj => _obstacles.Remove(obj));

                List<FruitPoolable> toRemove2 = new();
                foreach (var obj in _fruits)
                {
                    if (!obj.Fruit.gameObject.activeSelf ||
                            obj.transform.position.z < _referenceObject.transform.position.z + _generationSettings.ReferenceObjectRoadOffset)
                    {
                        toRemove2.Add(obj);
                        if (obj.Fruit.gameObject.activeSelf)
                            obj.Release();
                    }
                }
                toRemove2.ForEach(obj => _fruits.Remove(obj));

                _lastGC = Time.time;
            }
        }


        void UpdateRoadSegments()
        {
            float minPos = _referenceObject.position.z + _generationSettings.ReferenceObjectRoadOffset;
            while (_roadSegments.Peek().position.z < minPos)
            {
                var lastRoadSegment = _roadSegments.Dequeue();
                var newPos = lastRoadSegment.transform.position;
                newPos.z += _generationSettings.LevelLength;
                lastRoadSegment.transform.position = newPos;
                _roadSegments.Enqueue(lastRoadSegment);
                SetupNewRoadSegment(lastRoadSegment);
            }
        }

        public void SetGenerationSettings(LevelGenerationSettingsSO settings)
        {
            _generationSettings = settings;
            _obstacleDistribution = _generationSettings.StartObstacleDistribution;
        }

        public void SetReferenceObject(Transform reference)
        {
            _referenceObject = reference;
        }

        public void InitGenerate()
        {
            float currentPos = _referenceObject.position.z + _generationSettings.ReferenceObjectRoadOffset;
            float endPos = currentPos + _generationSettings.LevelLength;
            while (currentPos < endPos)
            {
                var roadSegment = Instantiate(_generationSettings.RoadSegmentPrefab, _roadParent);
                roadSegment.transform.position = roadSegment.transform.position.SetZ(currentPos);
                currentPos += _generationSettings.RoadSegmentLength;
                _roadSegments.Enqueue(roadSegment.transform);

                SetupNewRoadSegment(roadSegment.transform);
            }
        }

        void SetupNewRoadSegment(Transform segment)
        {
            if (IsRoadSegmentHasItems(_lastSegmentIdx))
                SetupRoadSegmentItems(segment);

            _lastSegmentIdx++;
        }

        bool IsRoadSegmentHasItems(int idx)
        {
            return idx > _generationSettings.StartEmptySegments &&
                     (_generationSettings.NoItemsSegments == 0 || 
                     (idx % (_generationSettings.NoItemsSegments + 1)) == 0);
        }

        void SetupRoadSegmentItems(Transform roadSegment)
        {
            int numObstacles = _obstacleDistribution.Sample();
            int numFruits = _generationSettings.FruitDistribution.Sample();
            int totalItems = numObstacles + numFruits;

            while (totalItems > _generationSettings.MaxItemsPerSegment)
            {
                numFruits--;
                totalItems--;
                if (totalItems <= _generationSettings.MaxItemsPerSegment)
                    break;
                numObstacles--;
                totalItems--;
            }

            float totalWidth = _generationSettings.LevelWidth;
            float segmentWidth = totalWidth / totalItems;
            float maxOffset = Mathf.Clamp(segmentWidth - 2, 0, segmentWidth); // lets suggest an item has max width == 2(not true tho)
            
            var randomOrder = Enumerable.Range(0, totalItems).OrderBy(_ => Random.value);
            foreach (var i in randomOrder)
            {
                float itemX = i * segmentWidth + segmentWidth / 2;
                itemX += Random.Range(-maxOffset / 2, maxOffset / 2);
                itemX -= totalWidth / 2;

                Vector3 position = new(
                        itemX, 
                        roadSegment.position.y, 
                        roadSegment.position.z + _generationSettings.RoadSegmentLength / 2
                );

                if (numFruits > 0)
                {
                    CreateNewFruit(position);
                    numFruits--;
                }
                else
                {
                    CreateNewObstacle(position);
                    numObstacles--;
                }
            }
        }

        void CreateNewFruit(Vector3 position)
        {
            Fruit.FruitType fruitToCreate = GetRandomFruitType();

            var newFruit = ServiceLocator.Get<FruitFactory>().GetFruit(fruitToCreate);
            newFruit.transform.position = position;
            _fruits.Add(newFruit);
        }

        Fruit.FruitType GetRandomFruitType()
        {
            float totalProbability = _generationSettings.Fruits.Sum((fruit) => fruit.FruitProbability);
            float probability = Random.Range(0, totalProbability);

            Fruit.FruitType fruitType = default;
            float accum = 0;
            foreach (var fruit in _generationSettings.Fruits)
            {
                accum += fruit.FruitProbability;
                if (probability < accum)
                {
                    fruitType = fruit.FruitType;
                    break;
                }
            }

            return fruitType;
        }

        void CreateNewObstacle(Vector3 position)
        {
            var obstacle = ServiceLocator.Get<ObstacleFactory>().GetRandomObstacle();
            obstacle.transform.SetPositionAndRotation(position, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            _obstacles.Add(obstacle);
        }
    }
}
