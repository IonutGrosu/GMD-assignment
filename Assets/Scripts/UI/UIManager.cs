using UnityEngine;
using UnityEngine.SceneManagement;

namespace StarterAssets.UI
{
    public class UIManager: MonoBehaviour
    {
        [SerializeField] private GameObject settingsMenu;
        private void Start()
        {
            Debug.Log("Start and unload");
            settingsMenu.SetActive(false);
        }
    }
}