using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TestTask.Characters.Components
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _ownAgent;

        private bool _agentMotionAwaited;
        private Vector3 _agentDestination;
        
        public event Action onMovementStopped;

        private void FixedUpdate()
        {
            if (_agentMotionAwaited && !_ownAgent.pathPending)
            {
                if (_ownAgent.remainingDistance <= _ownAgent.stoppingDistance)
                {
                    OnDestinatonReached();
                }
            }
        }

        private void OnDestinatonReached()
        {
            _agentMotionAwaited = false;
            onMovementStopped?.Invoke();
        }

        public void MoveToPosition(Vector3 position)
        {
            //I've had some issues related to navmesh agent sometimes forgetting their destination if stopped (isStopped = true) and then restarted
            //Thus it's a good idea (in my opinion) to remember destination, especially if we'll need more complex moving behaviour later (i.e. jumping, etc)
            _agentDestination = position;
            _agentMotionAwaited = true;
            _ownAgent.SetDestination(_agentDestination);
        }

        public void WarpToPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Disable()
        {
            enabled = false;
            _ownAgent.isStopped = true;
            _agentMotionAwaited = false;
        }
    }
}