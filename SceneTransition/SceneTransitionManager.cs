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
            transitionComplete = onTransitionComplete;
            currentScene = SceneManager.GetActiveScene();
            var currentSceneIndex = currentScene.buildIndex;
            var nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.sceneLoaded += UnloadOldScene;
            SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Additive);
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
