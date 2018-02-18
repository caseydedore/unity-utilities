
using UnityEngine;

namespace Control
{
    public class GetObjectUnderCursor
    {
        private LayerMask layerMask { get; set; }
        private Camera camera { get; set; }
        public float CursorWidth { get; set; }
        public float MaxDistance { get; set; }

        private GameObject currentObject = null;
        private Ray currentDirection;
        private RaycastHit currentHit;

        public GetObjectUnderCursor(Camera camera, LayerMask layerMask)
        {
            this.camera = camera;
            this.layerMask = layerMask;
            CursorWidth = 0.01f;
            MaxDistance = 50f;
        }

        public GetGameObjectResult Get()
        {
            currentObject = null;
            currentHit = new RaycastHit();
            currentDirection = camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(currentDirection, out currentHit, MaxDistance, layerMask))
            {
                currentObject = currentHit.collider.gameObject;
            }

            if(currentObject == null &&
                Physics.SphereCast(currentDirection, CursorWidth, out currentHit, MaxDistance, layerMask))
            {
                currentObject = currentHit.collider.gameObject;
            }

            var result = new GetGameObjectResult(currentObject, currentHit.point);

            return result;
        }
    }
}
