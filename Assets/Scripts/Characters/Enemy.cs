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
            
            foreach (var hurtbox in hurtboxes)
            {
                hurtbox.Initialize(this);
            }
        }

        protected override void OnCharacterTouch(Character other)
        {
            if (other is Player player)
            {
                player.TakeDamage(player.MaxHealth);
            }
        }

        public void Activate()
        {
            StartMoving(Player.GetPosition());
        }

    }
}