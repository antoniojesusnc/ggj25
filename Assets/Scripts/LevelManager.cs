using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ggj25
{
    public class LevelManager : MonoBehaviour
    {
        private float PLAYER_ROOM_CHECK_TIME = 1;

        private float _timestamp = 0;
        
        private List<RoomController> _rooms;
        private RoomController _currentRoom;
        private HeroController _hero;

        public void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rooms = GameObject.FindObjectsOfType<RoomController>().ToList();
            _hero = GameObject.FindObjectOfType<HeroController>();
            
            CheckPlayerRoom();
            _timestamp = PLAYER_ROOM_CHECK_TIME;
        }

        private void CheckPlayerRoom()
        {
            var _heroRect = new Rect(
                _hero.transform.position.x,
                _hero.transform.position.y,
                1,
                1);
            foreach (var roomController in _rooms)
            {
                if (roomController.RoomRect.Overlaps(_heroRect))
                {
                    _currentRoom?.SetActive(false);
                    _currentRoom = roomController;
                    _currentRoom.SetActive(true);
                    break;
                }
            }
        }

        void Update()
        {
            _timestamp -= Time.deltaTime;
            if (_timestamp <= 0)
            {
                CheckPlayerRoom();
                _timestamp = PLAYER_ROOM_CHECK_TIME;
            }

            if (IsCurrentRoomFinished())
            {
                _currentRoom.Complete();
            }
        }

        private bool IsCurrentRoomFinished()
        {
            return _currentRoom.CleanFactor >= GameManager.Instance.GameConfig.CleanSuccessRate;
        }
    }
}
