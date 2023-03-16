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
        [SerializeField] private Transform _bulletStartPosition;
        private static Player _instance;

        public static event Action onPlayerDied;


        override protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                base.Awake();
            } else
            {
                Debug.LogWarning("There should be only 1 Player on the scene! Destroying another one.");
                Destroy(gameObject);
            }
        }

        public override void Die()
        {
            base.Die();
            onPlayerDied?.Invoke();
        }

        private void Shoot(Vector3 targetPosition, Transform optionalTarget = null)
        {
            var bullet = BulletPool.GetBullet();
            bullet.Initialize(_bulletStartPosition.position, Quaternion.LookRotation(targetPosition - _bulletStartPosition.position, Vector3.up), optionalTarget);
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
            _instance.Shoot(targetPosition, optionalTarget);
        }

        public static Vector3 GetPosition() => _instance.transform.position;
    }
}