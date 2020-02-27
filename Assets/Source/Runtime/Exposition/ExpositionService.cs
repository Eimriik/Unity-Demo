using System;
using System.Collections.Generic;

using Zenject;


namespace StudioEntropy.Exposition 
{

    public class ExpositionService : IInitializable
    {
        
        private readonly Dictionary< Type, List< object > > exposedPropertyPool = 
            new Dictionary< Type, List< object > >( );
        
        /// <summary>
        /// Initialises the exposition service.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Initialize( )
        {
            
        }

    }

}
