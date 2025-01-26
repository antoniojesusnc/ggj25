using System;
using DG.Tweening;
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

        private Tween _mainMusicTimer;
        
        private void Start()
        {
            SoundManager.Instance.StopLoop();
            SoundManager.Instance.PlayLoop(AudioType.Loop.MainTheme);
        }

        public void ToMainMenu()
        {
            _mainMusicTimer?.Kill();
            SceneManager.LoadScene(0);
            IsInGame = false;
            
            //De momento dejo esta cancion para empezar
            SoundManager.Instance.StopLoop();
            SoundManager.Instance.PlayLoop(AudioType.Loop.MainTheme);
        }

        public void ToGame()
        {
            SoundManager.Instance.StopLoop();
            SoundManager.Instance.PlaySFX(AudioType.SFX.GameTheme, OnPlayMainTheme);
            _mainMusicTimer?.Kill();
            
            SceneManager.LoadScene(1);
            SceneManager.sceneLoaded += OnGameSceneLoaded;

            Time.timeScale = 1;
            IsInGame = true;
        }

        private void OnPlayMainTheme(AudioSFX sfx, AudioClip clip)
        {
            _mainMusicTimer = DOVirtual.DelayedCall(clip.length, () => 
                                      SoundManager.Instance.PlaySFX(AudioType.SFX.GameTheme, OnPlayMainTheme));
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
            _mainMusicTimer?.Kill();
            SoundManager.Instance.StopLoop();
            Time.timeScale = 0;

            if (isWin)
            {
                SoundManager.Instance.PlayLoop(AudioType.Loop.GameWon);
            }
            else
            {
                SoundManager.Instance.PlayLoop(AudioType.Loop.GameOver);
            }
            
            DOVirtual.DelayedCall(GameConfig.GameOverDelay, () => 
                                      FindObjectOfType<UIGameOverView>(true).Open(isWin));
        }

        public void ExitGame()
        {
            _mainMusicTimer?.Kill();
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
                ExitGame();
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