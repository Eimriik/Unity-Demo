using System;
using System.Collections.Generic;

using StudioEntropy.Injection.Internal;

using UnityEngine;

using Zenject;


namespace StudioEntropy.Injection 
{

    public abstract class BaseModuleAsset : ScriptableObject, IDependencyInjectionModule
    {

        /// <summary>
        /// Custom installer actions.
        /// </summary>
        private readonly List< BaseContextInstaller > contextInstallers = new List< BaseContextInstaller >( );
        
        /// <summary>
        /// List used to track <see cref="Context"/> instances in to which this module has been installed.
        /// </summary>
        private readonly List< Context > trackedContexts = new List< Context >( );
        
        
        [ SerializeField, HideInInspector ]
        private bool enabled = true;

        /// <summary>
        /// If <c>true</c>, this <see cref="IDependencyInjectionModule"/> will be installed.
        /// </summary>
        public bool Enabled
        {
            get => enabled;
            set => enabled = value;
        }
        

        /// <summary>
        /// Override this function to provide installer registrations.
        /// </summary>
        public abstract void Initialise( );

        /// <summary>
        /// Installs this <see cref="IDependencyInjectionModule"/> into the specified <see cref="Context"/>.
        /// <see cref="Context"/>.
        /// </summary>
        /// <param name="context">The context to install into.</param>
        public void Install( Context context )
        {
            // Don't attempt to install into a context we've already installed into.
            if ( trackedContexts.Contains( context ) )
                return;
            
            // Track that we've installed into this context
            trackedContexts.Add( context );
            
            OnInstall( context );
        }

        /// <summary>
        /// Called when this module attempts to install bindings into a <see cref="Context"/>.
        /// </summary>
        /// <param name="context"></param>
        protected virtual void OnInstall( Context context )
        {
            foreach ( var contextInstaller in contextInstallers )
                contextInstaller.Install( context );
        }

        /// <summary>
        /// Registers an installation method to invoke when this module is installed.
        /// </summary>
        /// <param name="installMethod">The installation method.</param>
        /// <typeparam name="TContext">The <see cref="Context"/> type this installer targets.</typeparam>
        protected void RegisterInstaller< TContext >( Action< TContext, DiContainer > installMethod )
            where TContext : Context
        {
            contextInstallers.Add( new ContextInstaller< TContext >
            {
                InstallMethod = installMethod
            } );
        }
    }

}
