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

        private void Awake()
        {
            segmentWaypoint.OnSegmentReached += OnSegmentReach;
            _remainingEnemies = enemies.Length;
            foreach (var enemy in enemies)
            {
                enemy.OnDeath += OnEnemyDeath;
            }
        }

        public void ActivateSegment()
        {
            //Here we could put some logic in case segment will need something done when player starts moving there
        }

        void OnSegmentReach()
        {
            foreach (var enemy in enemies)
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
                OnSegmentCleared?.Invoke();
            }
        }

        public Vector3 GetPlayerPlace() => segmentWaypoint.GetPlayerPlace();
    }
}