using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace StarterAssets.UI
{
    public class ButtonAction : MonoBehaviour
    {
        private void Awake()
        {
            SceneManager.UnloadSceneAsync(1);
        }

        public void Click()
        {
            Debug.Log("Button has been clicked");
            SceneManager.LoadScene(1);
        }
    }
}