using StudioEntropy.Injection;

using UnityEngine;

using Zenject;


namespace StudioEntropy.Demo
{

    /// <summary>
    /// A simple example asset demonstrating module usage.
    /// </summary>
    [ CreateAssetMenu( fileName = "Demo Module", menuName = "Studio Entropy/Modules/Demo") ]
    public class DemoModule : BaseModuleAsset
    {

        [ SerializeField, Tooltip( "The UI prefab to instantiate at runtime." ) ]
        private RectTransform UIPrefab;
        
        /// <summary>
        /// Override this function to provide installer registrations.
        /// </summary>
        public override void Initialise( )
        {
            RegisterInstaller< UserInterfaceContext >( InstallUserInterfaceContextBindings );
        }

        /// <summary>
        /// Installs UI related bindings into <see cref="UserInterfaceContext"/> instances.
        /// </summary>
        /// <param name="uiContext">The target <see cref="UserInterfaceContext"/> for installation.</param>
        /// <param name="container">The associated container.</param>
        private void InstallUserInterfaceContextBindings( UserInterfaceContext uiContext, DiContainer container )
        {
            container.InstantiatePrefab( UIPrefab, new GameObjectCreationParameters
            {
                Name = "User Interface"
            } );
        }
    }

}
