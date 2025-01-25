using System;
using UnityEngine;

namespace ggj25
{
    public class EnterRoomDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        private RoomController _roomController;

        private void Awake()
        {
            _roomController = GetComponentInParent<RoomController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == _layerMask.value)
            {
                GameManager.Instance.LevelManager.SelectRoom(_roomController);
            }
        }
    }
}
