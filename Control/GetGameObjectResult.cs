
using UnityEngine;

namespace Assets.Game.Scripts.Control
{
    public struct GetGameObjectResult
    {
        public GameObject GameObjectResult { get; private set; }
        public Vector3 ContactPoint { get; private set; }


        public GetGameObjectResult(GameObject gameObject, Vector3 contactPoint)
        {
            GameObjectResult = gameObject;
            ContactPoint = contactPoint;
        }
    }
}
