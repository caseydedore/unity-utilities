
using UnityEngine;

namespace Control
{
    public class ToggleBehaviourEnabled : MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour target = null;
        [SerializeField]
        private bool isEnabledAtStart = true;

        void Start()
        {
            target.enabled = isEnabledAtStart;
        }

        public void Toggle()
        {
            target.enabled = !target.enabled;
        }
    }
}
