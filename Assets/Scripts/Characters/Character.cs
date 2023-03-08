using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Characters
{
    public class Character : MonoBehaviour
    {


        public event Action OnDeath;

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {

        }

        public virtual void StartMoving(Vector3 targetPosition)
        {

        }

        public virtual void Warp(Vector3 targetPosition)
        {

        }
    }
}