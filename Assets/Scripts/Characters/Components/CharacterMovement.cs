using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestTask.Characters.Components
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] NavMeshAgent ownAgent;

        bool _agentMotionAwaited;
        Vector3 _agentDestination;
        public event Action OnMovementStopped;

        private void FixedUpdate()
        {
            if (_agentMotionAwaited && !ownAgent.pathPending)
            {
                if (ownAgent.remainingDistance <= ownAgent.stoppingDistance)
                {
                    OnDestinatonReached();
                }
            }
        }

        private void OnDestinatonReached()
        {
            _agentMotionAwaited = false;
            OnMovementStopped?.Invoke();
        }

        public void MoveToPosition(Vector3 position)
        {
            //I've had some issues related to navmesh agent sometimes forgetting their destination if stopped (IsStopped = true) and then restarted
            //Thus it's a good idea (in my opinion) to remember destination, especially if we'll need more complex moving behaviour later (i.e. jumping, etc)
            _agentDestination = position;
            _agentMotionAwaited = true;
            ownAgent.SetDestination(_agentDestination);
        }

        public void WarpToPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Disable()
        {
            enabled = false;
            ownAgent.isStopped = true;
            _agentMotionAwaited = false;
        }
    }
}