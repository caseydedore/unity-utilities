using System;
using UnityEngine.SceneManagement;

namespace SceneTransition
{
    public class SceneTransitionManager
    {
        private Scene currentScene;
        private Action transitionComplete;

        public void TransitionToNextScene(Action onTransitionComplete)
        {
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            var nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            TransitionToScene(nextSceneIndex, onTransitionComplete);
        }

        public void TransitionToScene(int sceneIndex, Action onTransitionComplete)
        {
            currentScene = SceneManager.GetActiveScene();
            transitionComplete = onTransitionComplete;
            SceneManager.sceneLoaded += SceneLoaded;
            SceneManager.sceneUnloaded += TransitionComplete;
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }

        private void SceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            SceneManager.sceneLoaded -= SceneLoaded;
            SceneManager.SetActiveScene(scene);
            SceneManager.UnloadSceneAsync(currentScene);
        }

        private void TransitionComplete(Scene scene)
        {
            SceneManager.sceneUnloaded -= TransitionComplete;
            transitionComplete();
            transitionComplete = () => { };
        }
    }
}
