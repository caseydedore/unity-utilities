using UnityEngine;

namespace Sensor
{

	public class LineOfSight
	{
		public bool CheckLineOfSight(Vector3 position, GameObject target)
		{
            return CheckLineOfSight(position, target.transform);
		}

        public bool CheckLineOfSight(Vector3 position, Transform target)
        {
            RaycastHit hit;

            if (Physics.Raycast(position, target.position - position, out hit, Vector3.Distance(position, target.position) + 5f))
            {
                foreach (var col in target.GetComponentsInChildren<Collider>())
                {
                    if (col == hit.collider) return true;
                }
            }

            return false;
        }

        public bool CheckFieldOfView(Vector3 position, Vector3 lookDirection, GameObject target, float fieldOfView)
        {
            return CheckFieldOfView(position, lookDirection, target.transform.position, fieldOfView);
        }

        public bool CheckFieldOfView(Vector3 position, Vector3 lookDirection, Vector3 target, float fieldOfView, float distanceToIncreaseFOV = 0)
        {
            if (fieldOfView * 0.5f >= Vector3.Angle(lookDirection, target - position)) return true;

            return false;
        }
	}
}

