using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Characters.Interactive
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        private List<Bullet> _pooledInstances;

        private static BulletPool _instance;


        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                _pooledInstances = new List<Bullet>();
            } else
            {
                Debug.LogWarning("There has to be only 1 BulletPool on the scene! Destroying 2nd instance.");
                Destroy(this);
            }
        }

        private void PoolBullet(Bullet bullet)
        {
            _pooledInstances.Add(bullet);
        }

        private void ReactivateBullet(Bullet bullet)
        {
            bullet.Reactivate();
            _pooledInstances.Remove(bullet);
        }

        private Bullet InstantiateBullet()
        {
            if (_pooledInstances.Count > 0)
            {
                Bullet oldBullet = _pooledInstances[0];
                ReactivateBullet(oldBullet);
                return oldBullet;
            }
            else
            {
                Bullet newBullet = Instantiate(_bulletPrefab, transform);
                newBullet.onDisposed += PoolBullet;
                return newBullet;
            }
        }

        public static Bullet GetBullet()
        {
            return _instance.InstantiateBullet();
        }
    }
}