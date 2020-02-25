using System.Collections.Generic;

using Zenject;


namespace StudioEntropy.Injection 
{

    /// <summary>
    /// Provides extended <see cref="DiContainer"/> functionality.
    /// </summary>
    public static class DiContainerExtensions
    {
        /// <summary>
        /// Binds a module instance such that it's resolvable using <see cref="ResolveAllModules"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="module">The module.</param>
        public static void BindModule( this DiContainer container, IDependencyInjectionModule module ) =>
            container.Bind< IDependencyInjectionModule >( ).FromInstance( module );
        
        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> collection of all bound modules.
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        internal static IEnumerable< IDependencyInjectionModule > ResolveAllModules( this DiContainer container ) =>
            container.ResolveAll< IDependencyInjectionModule >( );

    }

}
