using UnityEngine;

using Zenject;


namespace StudioEntropy.Injection 
{

    /// <summary>
    /// Performs module installation on a specific <see cref="Context"/>. This must be added to all <see cref="GameObject"/>s
    /// in the scene heirarchy that have a <see cref="Context"/> or <see cref="Context"/> derived component.
    /// </summary>
    [ DisallowMultipleComponent ]
    public sealed class ModuleInstaller : MonoInstaller< ModuleInstaller >
    {

        /// <summary>
        /// The <see cref="Context"/> this installer targets.
        /// </summary>
        public Context Context { get; private set; }

        [ MonoConstructor ]
        private void Construct( Context context )
        {
            Context = context;
        }
        
        /// <summary>
        /// Installs bindings into this <see cref="DiContainer"/>.
        /// </summary>
        public override void InstallBindings( )
        {
            // Honour enabled state.
            if( enabled )
                InstallModules( Container, Context );
        }

        /// <summary>
        /// Installs all modules into the specified container.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="context"></param>
        private static void InstallModules( DiContainer container, Context context )
        {
            // Get all bound modules.
            
            var modules = container.ResolveAllModules( );

            foreach ( var module in modules )
            {
                if ( container.IsValidating )
                {
                    // In validation, module instances may in fact be null when resolved.
                    if ( module == null )
                    {
                        Debug.Log( $"{nameof(ModuleInstaller)} found to be null in validation. Skipping installation." +
                            $" If errors in validation persist, consider decorating your module class with the " +
                            $"{nameof(ZenjectAllowDuringValidationAttribute)} attribute." );

                        break;
                    }

                    Debug.Log( $"Installing module: {module?.GetType( ).FullName ?? "NULL"} into " +
                        $"context: {context?.gameObject?.name ?? "Unknown"}" );
                }

                // Only install the module if it is enabled.
                
                if ( module.Enabled )
                    module.Install( context );
            }
        }

    }

}
