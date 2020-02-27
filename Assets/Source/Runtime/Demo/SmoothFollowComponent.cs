using System;

using UnityEngine;


namespace StudioEntropy.Demo 
{

    [ ExecuteInEditMode ]
    public class SmoothFollowComponent : MonoBehaviour
    {
        
        [ SerializeField ]
        private Transform target;
        private float offsetMagnitude;
        
        
        private void Start( )
        {
            offsetMagnitude = ( target.position - transform.position ).magnitude;
        }
        
        private void Update( )
        {
            transform.LookAt( target );
            transform.position = target.position - transform.forward * offsetMagnitude;
        }

    }

}
