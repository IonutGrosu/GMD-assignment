using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarterAssets.UI
{
    public class Buttons : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject settingsMenu;
        public void PlayGame()
        {
            Debug.Log("Clicked play");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Settings()
        {
            Debug.Log("Clicked settings");
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }

        public void BackToMainMenu()
        {
            Debug.Log("Clicked back to main menu");
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }
    }
}