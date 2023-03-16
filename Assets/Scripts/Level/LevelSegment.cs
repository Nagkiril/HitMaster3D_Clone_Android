using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Characters;

namespace TestTask.Level
{
    public class LevelSegment : MonoBehaviour
    {
        [SerializeField] private Waypoint _segmentWaypoint;
        [SerializeField] private Enemy[] _enemies;

        public event Action onSegmentCleared;

        private int _remainingEnemies;

        private void Awake()
        {
            _segmentWaypoint.onSegmentReached += OnSegmentReach;
            _remainingEnemies = _enemies.Length;
            foreach (var enemy in _enemies)
            {
                enemy.onDeath += OnEnemyDeath;
            }
        }

        public void ActivateSegment()
        {
            //Here we could put some logic in case segment will need something done when player starts moving there
        }

        void OnSegmentReach()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Activate();
            }
            CheckSegmentCleared();
        }

        void OnEnemyDeath()
        {
            _remainingEnemies--;
            CheckSegmentCleared();
        }

        void CheckSegmentCleared()
        {
            if (_remainingEnemies == 0)
            {
                onSegmentCleared?.Invoke();
            }
        }

        public Vector3 GetPlayerPlace() => _segmentWaypoint.GetPlayerPlace();
    }
}