using UnityEventExtension;
using UnityEngine;

namespace Control
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField]
        private string
            mouseX = "Mouse X",
            mouseY = "Mouse Y";

        [SerializeField]
        private UnityEventFloat
            moveMouseX = null,
            moveMouseY = null;


        void Update()
        {
            MoveMouseX(Input.GetAxis(mouseX));
            MoveMouseY(Input.GetAxis(mouseY));
        }

        private void MoveMouseX(float value)
        {
            moveMouseX.Invoke(value);
        }

        private void MoveMouseY(float value)
        {
            moveMouseY.Invoke(value);
        }
    }
}
