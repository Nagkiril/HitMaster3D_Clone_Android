using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Characters;

namespace TestTask.Level
{
    public class LevelSegment : MonoBehaviour
    {
        [SerializeField] Waypoint segmentWaypoint;
        [SerializeField] Enemy[] enemies;

        public event Action OnSegmentCleared;

        int _remainingEnemies;

        public void ActivateSegment()
        {
            foreach (var enemy in enemies)
            {
                enemy.Activate();
                enemy.OnDeath += OnEnemyDeath;
            }
            _remainingEnemies = enemies.Length;
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
                OnSegmentCleared?.Invoke();
            }
        }

        public Vector3 GetPlayerPlace() => segmentWaypoint.GetPlayerPlace();
    }
}