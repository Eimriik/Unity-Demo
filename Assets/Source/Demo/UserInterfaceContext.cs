using UnityEngine;

using Zenject;


namespace StudioEntropy.Demo 
{

    /// <summary>
    /// Defines a container we can leverage to construct the UI using dependency injection at runtime. The is useful when
    /// designing modular GUI's.
    /// </summary>
    [ RequireComponent( typeof( Canvas ) ) ]
    public class UserInterfaceContext : GameObjectContext
    {
        
    }

}
