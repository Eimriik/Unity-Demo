using System;

using UnityEngine;

using Zenject;


namespace StudioEntropy.Injection.Internal
{

    internal class ContextInstaller< TContext > : BaseContextInstaller
        where TContext : Context
    {
        public Action< TContext, DiContainer > InstallMethod { get; internal set; }
        
        /// <summary>
        /// Executes this installer on the specified <see cref="Context"/>.
        /// </summary>
        /// <param name="context">The <see cref="Context"/> to execute this installer on.</param>
        public override void Install( Context context )
        {
            if( this.Targets( context ) )
                InstallMethod.Invoke( ( TContext ) context, context.Container );
        }
        
    }

    internal static class ContextInstallMethodExtensions
    {

        /// <summary>
        /// Returns <c>true</c> if this <see cref="ContextInstaller{TContext}"/> targets contexts of type
        /// <see cref="TContext"/>.
        /// </summary>
        /// <param name="contextInstaller"></param>
        /// <typeparam name="TContext"></typeparam>
        /// <returns></returns>
        public static bool Targets< TContext >( this ContextInstaller< TContext > contextInstaller, Context context )
            where TContext : Context
        {
            return context is TContext;
        }

    }
    
}
