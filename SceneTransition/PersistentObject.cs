using UnityEngine;


namespace SceneTransition
{
    public class PersistentObject : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
