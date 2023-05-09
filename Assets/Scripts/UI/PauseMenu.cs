using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarterAssets.UI
{
    public class PauseMenu: MonoBehaviour
    {
        public GameObject pauseMenu;
        public GameObject restOfUI;
        private bool _isPaused;

        private void Start()
        {
            pauseMenu.SetActive(false);
            _isPaused = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        public void PauseGame()
        {
            restOfUI.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            _isPaused = true;
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(false);
            restOfUI.SetActive(true);
            Time.timeScale = 1f;
            _isPaused = false;
        }

        public void GoToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}