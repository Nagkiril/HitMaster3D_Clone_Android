using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TestTask.Characters;

namespace TestTask.Level
{
    public class ShootController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] float maxProjectionDistance;
        [SerializeField] float targetlessAimDistance;
        const string TARGET_LAYER_NAME = "Targetable";
        LayerMask _targetLayerMask;

        void Awake()
        {
            _targetLayerMask = LayerMask.GetMask(TARGET_LAYER_NAME);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PropagateInput(eventData);
        }

        private void PropagateInput(PointerEventData eventData)
        {
            Vector3 shootPosition;
            Transform shootTarget = null;

            RaycastHit raycastHit;
            Vector3 propagationStart = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, transform.localPosition.z));
            Vector3 propagationEnd = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, maxProjectionDistance));
            if (Physics.Raycast(propagationStart, propagationEnd - propagationStart, out raycastHit, maxProjectionDistance, _targetLayerMask))
            {
                shootPosition = raycastHit.point;
                shootTarget = raycastHit.transform;
            } else
            {
                shootPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, targetlessAimDistance));
            }
            Player.OrderShoot(shootPosition, shootTarget);
        }
    }
}