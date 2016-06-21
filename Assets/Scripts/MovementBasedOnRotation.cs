using UnityEngine;

namespace Assets.Scripts
{
    public class MovementBasedOnRotation : MonoBehaviour
    {
        private Quaternion Rotation;

        public float DeadZone = .25f;

        public float SpeedMultiplier = 5f;

        public void Update()
        {
            Rotation = gameObject.transform.rotation;

            var rotatedVector = Rotation * Vector3.up;

            var force = new Vector3(rotatedVector.x, 0f, rotatedVector.z);

            if (force.magnitude > DeadZone)
            {
                gameObject.transform.position += force * Time.deltaTime * SpeedMultiplier;    
            }
        }
    }
}
