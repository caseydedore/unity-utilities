
using UnityEngine;
using UnityEngine.Events;

namespace SceneTransition
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onTransitionComplete;
        private SceneTransitionManager manager = new SceneTransitionManager();

        public void NextScene()
        {
            manager.TransitionToNextScene(onTransitionComplete.Invoke);
        }
    }
}
