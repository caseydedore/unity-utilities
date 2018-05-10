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
            SceneManager.sceneLoaded += UnloadOldScene;
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }

        private void UnloadOldScene(Scene scene, LoadSceneMode loadMode)
        {
            SceneManager.sceneLoaded -= UnloadOldScene;
            SceneManager.SetActiveScene(scene);
            SceneManager.sceneUnloaded += OldSceneUnloaded;
            SceneManager.UnloadSceneAsync(currentScene);
        }

        private void OldSceneUnloaded(Scene scene)
        {
            SceneManager.sceneUnloaded -= OldSceneUnloaded;
            transitionComplete();
            transitionComplete = () => { };
        }
    }
}
