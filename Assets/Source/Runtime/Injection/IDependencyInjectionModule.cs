
using Zenject;


namespace StudioEntropy.Injection
{

    /// <summary>
    /// Defines a module that can install dependencies across any number of injectable containers.
    /// </summary>
    public interface IDependencyInjectionModule
    {

        /// <summary>
        /// If <c>true</c>, this <see cref="IDependencyInjectionModule"/> will be installed.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Initialises this module.
        /// </summary>
        void Initialise( );
        
        /// <summary>
        /// Installs this <see cref="IDependencyInjectionModule"/> into the specified <see cref="Context"/>.
        /// <see cref="Context"/>.
        /// </summary>
        /// <param name="context">The context to install into.</param>
        void Install( Context context );

    }
    
}
