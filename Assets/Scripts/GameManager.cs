using UnityEngine;
using UnityEngine.SceneManagement;

namespace ggj25
{
    public class GameManager : Singleton<GameManager>
    {
        [field: SerializeField]
        public GameConfig GameConfig { get; private set; }

        public bool IsInGame { get; private set; }
        public bool IsPlaying => Time.timeScale > 0;
        
        public LevelManager LevelManager { get; private set; }

        public void ToMainMenu()
        {
            SceneManager.LoadScene(0);
            IsInGame = false;
            
            //De momento dejo esta cancion para empezar
            SoundManager.Instance.PlayLoop(AudioType.Loop.MainTheme);
        }

        public void ToGame()
        {
            SceneManager.LoadScene(1);
            SceneManager.sceneLoaded += OnGameSceneLoaded;

            Time.timeScale = 1;
            IsInGame = true;
        }

        private void OnGameSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            LevelManager = new GameObject(nameof(ggj25.LevelManager)).AddComponent<LevelManager>();
            SceneManager.sceneLoaded -= OnGameSceneLoaded;
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
            FindObjectOfType<UIGameOverView>(true).Open(isWin);
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