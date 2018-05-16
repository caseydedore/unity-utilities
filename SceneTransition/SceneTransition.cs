
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace SceneTransition
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField]
        private int loadingSceneIndex = 0;
        [SerializeField]
        private float minimumTimeToRemainInLoadingScene = 2f;
        [SerializeField]
        private UnityEvent onTransitionComplete;
        private SceneTransitionManager manager = new SceneTransitionManager();

        public void NextScene()
        {
            manager.TransitionToNextScene(onTransitionComplete.Invoke);
        }

        public void NextScene(int sceneIndex)
        {
            manager.TransitionToScene(sceneIndex, onTransitionComplete.Invoke);
        }

        public void NextSceneDelayed(int sceneIndex)
        {
            var startTime = Time.time;
            StartCoroutine(WaitToCompleteLoadSceneRecursive(sceneIndex, startTime, onTransitionComplete.Invoke));
        }

        public void NextSceneWithLoadSceneTransition(int sceneIndex)
        {
            Action onLoadingSceneReady = () => {
                var transitions = FindObjectsOfType<SceneTransition>();
                var newTransition = transitions.Where(t => !ReferenceEquals(this, t)).FirstOrDefault();
                newTransition.NextSceneDelayed(sceneIndex);
            };
            manager.TransitionToScene(loadingSceneIndex, onLoadingSceneReady);
        }

        private IEnumerator WaitToCompleteLoadSceneRecursive(int sceneIndex, float startTimestamp, Action onComplete)
        {
            var runningTime = Time.time - startTimestamp;
            if (runningTime < minimumTimeToRemainInLoadingScene)
            {
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(WaitToCompleteLoadSceneRecursive(sceneIndex, startTimestamp, onComplete));
            }
            else
            {
                manager.TransitionToScene(sceneIndex, onComplete);
            }
        }
    }
}
