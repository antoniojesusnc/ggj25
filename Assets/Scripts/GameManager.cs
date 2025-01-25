using UnityEngine;
using UnityEngine.SceneManagement;

namespace ggj25
{
    public class GameManager : Singleton<GameManager>
    {
        public bool IsInGame { get; private set; }
        public bool IsPlaying => Time.timeScale > 0;

        public void ToMainMenu()
        {
            SceneManager.LoadScene(0);
            IsInGame = false;
            
        }

        public void ToGame()
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
            IsInGame = true;
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
            FindObjectOfType<UIPauseView>(true).Open();
        }

        public void ContinueGame()
        {
            Time.timeScale = 1;
            FindObjectOfType<UIPauseView>().Close();
        }

        public void GameOver(bool isWin)
        {
            Time.timeScale = 0;
            FindObjectOfType<UIGameOverView>().Open(isWin);
        }

        public void ExitGame()
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape))
            {
                return;
            }

            if (!IsInGame)
            {
                GameManager.Instance.ExitGame();
            }
            else if (IsPlaying)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }
}