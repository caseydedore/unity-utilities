
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Control
{
    public class Menu : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
        }

        public void RestartLevel()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }

}