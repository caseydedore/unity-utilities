using System.Linq;
using UnityEngine;

namespace Sensor
{
    public class ColliderObjectFinder
    {
        public T[] GetComponents<T>(Vector3 origin, float radius) where T : Component
        {
            var defaultLayerMask = 1 << LayerMask.NameToLayer("Default");
            return GetComponents<T>(origin, radius, defaultLayerMask);
        }

        public T[] GetComponents<T>(Vector3 origin, float radius, LayerMask mask) where T : Component
        {
            var colliders = Physics.OverlapSphere(origin, radius, mask);
            var filtered = 
                colliders
                .Select(collider => collider.GetComponent<T>())
                .Where(component => component != null).ToArray();
            return filtered;
        }
    }
}
