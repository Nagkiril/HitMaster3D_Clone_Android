using System;
using System.Collections;
using System.Collections.Generic;
using TestTask.Level;
using TestTask.Characters.Interactive;
using UnityEngine;

namespace TestTask.Characters
{
    public class Player : Character
    {
        static Player _instance;

        public static event Action OnPlayerDied;


        override protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                base.Awake();
                OnDeath += NotifyPlayerDeath;
            } else
            {
                Debug.LogWarning("There should be only 1 Player on the scene! Destroying another one.");
                Destroy(gameObject);
            }
        }

        protected override void OnWaypointTouch(Waypoint other)
        {
            other.EnterWaypoint(this);
        }

        public static void OrderMovement(Vector3 targetPosition)
        {
            _instance.StartMoving(targetPosition);
        }

        public static void OrderWarp(Vector3 targetPosition)
        {
            _instance.Warp(targetPosition);
        }

        public static void OrderShoot(Vector3 targetPosition, Transform optionalTarget = null)
        {
            var bullet = BulletPool.GetBullet();

            //bullet.Initialize(, optionalTarget);
        }

        public static Vector3 GetPosition() => _instance.transform.position;

        private void NotifyPlayerDeath()
        {
            OnPlayerDied?.Invoke();
        }
    }
}