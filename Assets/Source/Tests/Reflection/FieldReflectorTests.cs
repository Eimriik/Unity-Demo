using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using UnityEngine;


namespace StudioEntropy.Demo.Tests
{
    [ TestFixture ]
    public class FieldReflectorTests
    {

        private class Person
        {

            private Guid id;

            private List< string > tags;
            
            public Person( )
            {
                id = Guid.NewGuid( );
            }
            
        }
        
    }

}

