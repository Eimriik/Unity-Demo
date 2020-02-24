using System.Collections.Generic;

using UnityEngine;

using Zenject;


namespace StudioEntropy.Injection 
{

    /// <summary>
    /// Binds all configured modules such that they are resolvable via <see cref="DiContainerExtensions.ResolveAllModules"/>
    /// calls. This <see cref="Component"/> must always accompany a <see cref="ProjectContext"/>. 
    /// </summary>
    /// <remarks>
    /// The component is critical in ensuring module installation occurs correctly. If you have Zenject resolve errors,
    /// ensure that you haven't forgotten to add a <see cref="ModuleBinder"/> component to your <see cref="ProjectContext"/>
    /// prefab and that the <see cref="ModuleBinder"/> is configured appropriately with the required modules.
    /// </remarks>
    [ RequireComponent( typeof( ProjectContext ) ) ]
    [ DisallowMultipleComponent ]
    public class ModuleBinder : MonoInstaller< ModuleBinder >
    {

        [ SerializeField ]
        private List< BaseModuleAsset > assetModules = new List< BaseModuleAsset >( );


        /// <summary>
        /// Installs bindings into this <see cref="DiContainer"/>.
        /// </summary>
        public override void InstallBindings( )
        {
            foreach ( var module in assetModules )
            {
                // Don't attempt to install a null module, raise a warning to alert the user instead.
                if ( module == null )
                {
                    Debug.LogWarning( $"Found null {nameof(BaseModuleAsset)} in {name}. Skipping installation." );
                    continue;
                }
                
                // Honour enabled state.
                if ( module.Enabled )
                {
                    module.Initialise( );
                    Container.BindModule( module );
                }
            }
        }
    }
}
