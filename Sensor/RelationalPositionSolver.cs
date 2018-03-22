using UnityEngine;

namespace Sensor
{
	public class RelationalPositionSolver
	{
        public Vector3 GetRandomPositionInDirection(Vector3 origin, Vector3 direction, 
            float angleDeviation, float distance, bool keepLineOfSight = true)
        {
            distance = Mathf.Abs(distance);
            var rotation = Quaternion.AngleAxis(Random.Range(-angleDeviation*0.5f, angleDeviation*0.5f), Vector3.up);
            direction = (rotation * direction).normalized * distance;
            //Debug.DrawRay(direction + origin, Vector3.up * 10f, Color.green, 0.5f);
            if (keepLineOfSight)
            {
                RaycastHit hit;
                if (Physics.Raycast(origin, direction, out hit, distance))
                {
                    return hit.point;
                }
            }

            return (direction + origin);
        }

        public Vector3 GetRandomPositionInRange(Vector3 origin, float radius, bool keepLineOfSight = true)
		{
			radius = Mathf.Abs(radius);
			var rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up);
			var direction = rotation * origin;
            //Debug.DrawRay(origin, direction.normalized * radius, Color.red, 0.5f);
            if (keepLineOfSight)
			{
				RaycastHit hit;
				if(Physics.Raycast(origin, direction, out hit, radius))
				{
					return hit.point;
				}
			}

			return (origin + direction.normalized * radius);
		}

	}
}

