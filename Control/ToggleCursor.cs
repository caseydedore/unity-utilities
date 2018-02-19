
using UnityEngine;

namespace Control
{
    public class ToggleCursor : MonoBehaviour
    {
        [SerializeField]
        private bool isVisibleAtStart = true;


        public void Start()
        {
            Cursor.visible = isVisibleAtStart;
        }

        public void Toggle()
        {
            Cursor.visible = !Cursor.visible;
        }
    }
}
