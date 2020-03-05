using UnityEngine;


namespace StudioEntropy.Demo 
{

    [ ExecuteInEditMode ]
    public class WheelComponent : MonoBehaviour
    {

        private Vector3 lastPosition;
        private float circumfrence;

        [ SerializeField ]
        private Transform trailor;
        
        [ SerializeField ]
        private float diameter;
        
        private void Start( )
        {
            circumfrence = diameter * Mathf.PI;
            lastPosition = trailor.position;
        }

        private void Update( )
        {
            var currentPosition = trailor.position;
            var distanceVector = currentPosition - lastPosition;
            var distanceTravelled = distanceVector.magnitude;
            
            var multiplier = Vector3.Dot( distanceVector.normalized, trailor.forward );
            multiplier = multiplier > 0 ? 1 : -1;
            
            var revolutions = distanceTravelled / circumfrence;
            transform.Rotate( Vector3.right, 360 * revolutions * multiplier );
            
            lastPosition = trailor.position;
        }
    }

}
