using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Characters.Components;

namespace TestTask.Characters.Interactive
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float bulletDamage;
        [SerializeField] float bulletStoppingPower;
        [SerializeField] float bulletSpeed;
        [SerializeField] float flightMaxTime;
        [SerializeField] float flightAssistRatio;
        [SerializeField] Rigidbody ownRigidbody;
        public event Action<Bullet> OnDisposed;
        
        Transform _bulletTarget;
        float _currentFlightTime;


        private void FixedUpdate()
        {
            if (gameObject.activeInHierarchy)
            {
                _currentFlightTime += Time.deltaTime;

                if (_currentFlightTime < flightMaxTime)
                {
                    var moveDistance = bulletSpeed * Time.deltaTime;
                    if (_bulletTarget == null)
                    {
                        ownRigidbody.MovePosition(transform.position + transform.forward * moveDistance);
                    }
                    else
                    {
                        //This is a little heavy, but should allow for nicer gameplay with a touch of flight assist
                        //This may also be dangerous if bullet passes the target and misses, but we can keep it as is for the prototype
                        Vector3 moveDirection = Vector3.Lerp(transform.forward, (_bulletTarget.position - transform.position).normalized, flightAssistRatio);
                        ownRigidbody.MovePosition(transform.position + moveDirection * moveDistance);
                    }
                }
                else
                {
                    Dispose();
                }
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            var otherRigidbody = other.attachedRigidbody;
            if (otherRigidbody != null)
            {
                var otherHurtbox = otherRigidbody.GetComponent<CharacterInteractor>();
                if (otherHurtbox != null && otherHurtbox.Owner is Enemy otherEnemy)
                {
                    otherEnemy.TakeDamage(bulletDamage, transform.forward * bulletStoppingPower, otherHurtbox.transform);
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
            OnDisposed?.Invoke(this);
        }
    }
}