using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Characters
{
    public class Enemy : Character
    {


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