using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ggj25
{
    public class UIIMainMenuCanvas : MonoBehaviour
    {
        [SerializeField]
        private Transform _credits;

        private void Awake()
        {
            _credits.gameObject.SetActive(false);
        }

        public void OnClickInPlay()
        {
            GameManager.Instance.ToGame();
        }

        public void OnClickInCredits()
        {
            _credits.gameObject.SetActive(true);
        }
        
        public void OnClickInCloseCredits()
        {
            _credits.gameObject.SetActive(false);
        }

        public void OnClickInExit()
        {
            GameManager.Instance.ExitGame();
        }
    }
}
