using UnityEngine;
using UnityEngine.AI;

namespace Translation
{
    public class MovementGvr : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent agent;
        [SerializeField]
        private Transform localSpace;
        [SerializeField]
        private float
            movementLowerBound = 0.5f,
            movementUpperBound = 0.8f,
            movementMagnitude = 3f;
        private const float MAGNITUDE_MULTIPLIER = 0.01f;

        public void Move(Vector2 direction)
        {
            var finalDirection = GetFinalDirection(direction);
            agent.Move(finalDirection);
        }

        private Vector3 GetFinalDirection(Vector2 rawDirection)
        {
            var x = rawDirection.x;
            var y = rawDirection.y;
            var finalDirection = GetBoundedDirection(new Vector3(x, 0, y));
            finalDirection = localSpace.TransformDirection(finalDirection);
            return finalDirection;
        }

        private Vector3 GetBoundedDirection(Vector3 rawDirection)
        {
            var direction = Vector3.zero;
            if (rawDirection.magnitude >= movementLowerBound)
            {
                var magnitudePercentage = (((rawDirection.magnitude - movementLowerBound) * 100) / (movementUpperBound - movementLowerBound)) * 0.01f;
                var clippedMagnitudePercentage = magnitudePercentage;
                if (clippedMagnitudePercentage < 0f)
                    clippedMagnitudePercentage = 0f;
                else if (clippedMagnitudePercentage > 1f)
                    clippedMagnitudePercentage = 1f;

                direction = 
                    (rawDirection.normalized * clippedMagnitudePercentage)
                    * (movementMagnitude * MAGNITUDE_MULTIPLIER);
            }
            return direction;
        }
    }
}
