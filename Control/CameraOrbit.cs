using UnityEngine;

namespace Control
{
    public class CameraOrbit : MonoBehaviour
    {
        [SerializeField]
        private Transform target = null;

        [SerializeField]
        private LayerMask layerMask = new LayerMask();

        [SerializeField]
        private float
            distance = 5f,
            distanceFirstPersonCutoff = 0.5f,
            cameraBufferSize = 0.1f,
            cameraSmoothDollySpeed = 20f,
            xSpeed = 120f,
            ySpeed = 120f,
            yMinLimit = -20f,
            yMaxLimit = 80f;

        private float
            currentTargetDistance = 0f,
            lastDistance = 0f,
            xInput = 0f,
            yInput = 0f,
            x = 0f,
            y = 0f;

        private Vector3 position = Vector3.zero;
        private Quaternion rotation = Quaternion.identity;
        private RaycastHit hit = new RaycastHit();


        void Start()
        {
            var angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;

            currentTargetDistance = distance;
            lastDistance = distance;
        }

        void LateUpdate()
        {
            x += xInput * xSpeed * Time.deltaTime;
            y -= yInput * ySpeed * Time.deltaTime;

            ResetXAxisInput();
            ResetYAxisInput();

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            rotation = Quaternion.Euler(y, x, 0);

            currentTargetDistance = GetFinalCameraDistance();

            position =
                new Vector3(0f, 0f, -Mathf.Lerp(lastDistance, currentTargetDistance, cameraSmoothDollySpeed * Time.deltaTime));
            position = rotation * position + target.position;

            lastDistance = Mathf.Lerp(lastDistance, currentTargetDistance, cameraSmoothDollySpeed * Time.deltaTime);

            transform.rotation = rotation;
            transform.position = position;
        }

        public void AddXAxisInput(float value)
        {
            xInput = value;
        }

        public void AddYAxisInput(float value)
        {
            yInput = value;
        }

        private void ResetXAxisInput()
        {
            xInput = 0f;
        }

        private void ResetYAxisInput()
        {
            yInput = 0f;
        }

        private float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;

            return Mathf.Clamp(angle, min, max);
        }

        private float GetFinalCameraDistance()
        {
            var targetDistance = distance;

            if (Physics.SphereCast(target.position, cameraBufferSize,
                    transform.position - target.position, out hit, distance, layerMask))
            {
                if (hit.distance > distanceFirstPersonCutoff)
                {
                    targetDistance = hit.distance;
                }
                else
                {
                    targetDistance = 0.05f;
                }
            }

            return targetDistance;
        }
    }
}