using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Characters.Components;
using TestTask.Level;

namespace TestTask.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] protected CharacterMovement movement;
        [SerializeField] protected CharacterVisuals visuals;
        [SerializeField] protected CharacterInteractor interactor;
        [SerializeField] protected CharacterHealth health;


        public float MaxHealth => health.MaxHealth;
        public event Action OnDeath;

        protected virtual void Awake()
        {
            movement.OnMovementStopped += OnMovementStop;
            interactor.OnCharacterTouched += OnCharacterTouch;
            interactor.OnWaypointTouched += OnWaypointTouch;
            interactor.Initialize(this);
            health.OnHealthDepleted += OnHealthDepletion;
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnCharacterTouch(Character other)
        {

        }

        protected virtual void OnWaypointTouch(Waypoint other)
        {

        }

        protected virtual void OnHealthDepletion()
        {
            Die();
        }

        protected virtual void OnMovementStop()
        {
            visuals.SetMoving(false);
        }

        public virtual void Die()
        {
            visuals.SetDeath();
            movement.Disable();
            interactor.Disable();
            OnDeath?.Invoke();
        }

        public virtual void StartMoving(Vector3 targetPosition)
        {
            movement.MoveToPosition(targetPosition);
            visuals.SetMoving(true);
        }

        public virtual void Warp(Vector3 targetPosition)
        {
            movement.WarpToPosition(targetPosition);
        }
        
        public virtual void TakeDamage(float damageAmount)
        {
            health.TakeDamage(damageAmount);
        }

        public virtual void TakeDamage(float damageAmount, Vector3 hitVector, Transform hitTarget)
        {
            health.TakeDamage(damageAmount);
        }
    }
}