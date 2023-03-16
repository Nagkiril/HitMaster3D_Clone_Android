using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Characters.Components;

namespace TestTask.Characters
{
    public class Enemy : Character
    {
        [SerializeField] private CharacterInteractor[] _hurtboxes;


        protected override void Awake()
        {
            base.Awake();
            onDeath += OnEnemyDied;
            foreach (var hurtbox in _hurtboxes)
            {
                hurtbox.Initialize(this);
            }
        }

        protected void OnEnemyDied()
        {
            foreach (var hurtbox in _hurtboxes)
            {
                hurtbox.Disable();
            }
        }

        protected override void OnCharacterTouch(Character other)
        {
            if (_health && other is Player player)
            {
                player.TakeDamage(player.maxHealth);
            }
        }

        public void Activate()
        {
            StartMoving(Player.GetPosition());
        }

        public override void TakeDamage(float damageAmount, Vector3 hitVector, Transform hitTarget)
        {
            if (_health.isAlive)
            {
                TakeDamage(damageAmount);

                if (!_health.isAlive)
                {
                    foreach (var hurtbox in _hurtboxes)
                    {
                        if (hurtbox.transform == hitTarget)
                        {
                            _visuals.HitReaction(hitVector, hitTarget.parent);
                        }
                    }
                }
            }
        }

    }
}