using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestTask.Characters.Components;

namespace TestTask.Characters
{
    public class Enemy : Character
    {
        [SerializeField] CharacterInteractor[] hurtboxes;


        protected override void Awake()
        {
            base.Awake();
            OnDeath += OnEnemyDied;
            foreach (var hurtbox in hurtboxes)
            {
                hurtbox.Initialize(this);
            }
        }

        protected void OnEnemyDied()
        {
            foreach (var hurtbox in hurtboxes)
            {
                hurtbox.Disable();
            }
        }

        protected override void OnCharacterTouch(Character other)
        {
            if (health && other is Player player)
            {
                player.TakeDamage(player.MaxHealth);
            }
        }

        public void Activate()
        {
            StartMoving(Player.GetPosition());
        }

        public override void TakeDamage(float damageAmount, Vector3 hitVector, Transform hitTarget)
        {
            if (health.IsAlive)
            {
                TakeDamage(damageAmount);

                if (!health.IsAlive)
                {
                    foreach (var hurtbox in hurtboxes)
                    {
                        if (hurtbox.transform == hitTarget)
                        {
                            visuals.HitReaction(hitVector, hitTarget.parent);
                        }
                    }
                }
            }
        }

    }
}