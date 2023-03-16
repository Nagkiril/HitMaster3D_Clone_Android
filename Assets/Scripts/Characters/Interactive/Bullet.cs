using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Characters.Components;

namespace TestTask.Characters.Interactive
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _bulletDamage;
        [SerializeField] private float _bulletStoppingPower;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _flightMaxTime;
        [SerializeField] private float _flightAssistRatio;
        [SerializeField] Rigidbody _ownRigidbody;
        public event Action<Bullet> onDisposed;
        
        private Transform _bulletTarget;
        private float _currentFlightTime;


        private void FixedUpdate()
        {
            _currentFlightTime += Time.deltaTime;

            if (_currentFlightTime < _flightMaxTime)
            {
                var moveDistance = _bulletSpeed * Time.deltaTime;
                if (_bulletTarget == null)
                {
                    _ownRigidbody.MovePosition(transform.position + transform.forward * moveDistance);
                }
                else
                {
                    //This is a little heavy, but should allow for nicer gameplay with a touch of flight assist
                    //This may also be dangerous if bullet passes the target and misses, but we can keep it as is for the prototype
                    Vector3 moveDirection = Vector3.Lerp(transform.forward, (_bulletTarget.position - transform.position).normalized, _flightAssistRatio);
                    _ownRigidbody.MovePosition(transform.position + moveDirection * moveDistance);
                }
            }
            else
            {
                Dispose();
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            var otherRigidbody = other.attachedRigidbody;
            if (otherRigidbody != null)
            {
                var otherHurtbox = otherRigidbody.GetComponent<CharacterInteractor>();
                if (otherHurtbox != null && otherHurtbox.owner is Enemy otherEnemy)
                {
                    otherEnemy.TakeDamage(_bulletDamage, transform.forward * _bulletStoppingPower, otherHurtbox.transform);
                }
            }
            Dispose();
        }

        public void Reactivate()
        {
            gameObject.SetActive(true);
            _currentFlightTime = 0f;
        }

        public void Initialize(Vector3 startPosition, Quaternion startRotation, Transform target)
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            _bulletTarget = target;
        }

        private void Dispose()
        {
            gameObject.SetActive(false);
            onDisposed?.Invoke(this);
        }
    }
}