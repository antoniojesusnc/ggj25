using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
        
        private int _completedRooms = 0;
        private bool _gameWon = false;


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
            
            _completedRooms = 0;
            _gameWon = false;
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
            if (_gameWon) return; // Skip updates if game is already won
            
            _timestamp -= Time.deltaTime;
            if (_timestamp <= 0)
            {
                CheckPlayerRoom();
                _timestamp = PLAYER_ROOM_CHECK_TIME;
            }

            if (IsCurrentRoomFinished())
            {
                _currentRoom.Complete();
                _completedRooms++;
                CheckWinCondition();
            }
        }
        
        private void CheckWinCondition()
        {
            if (_completedRooms >= _rooms.Count)
            {
                _gameWon = true;
                GameManager.Instance.GameOver(true);
            }
        }

        private bool IsCurrentRoomFinished()
        {
            return _currentRoom.CleanFactor >= GameManager.Instance.GameConfig.CleanSuccessRate 
                && !_currentRoom.IsCompleted;
        }
    }
}
