using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using NUnit.Framework;

using StudioEntropy.Reflection;

using UnityEngine;


namespace StudioEntropy.Demo.Tests
{
    [ TestFixture ]
    public class FieldReflectorTests
    {
        private Person Agent007;
            
        [ OneTimeSetUp ]
        public void SetUp( )
        {
            Agent007 = new Person( );
        }

        /// <summary>
        /// Expect reflected field to exist in the inspected tpye and have a non-null value.
        /// </summary>
        [ Test ]
        public void Test_WeaklyTyped_Get( )
        {
            // Wrapping instantiation in a using statements allows for automatic disposable of the reflector when
            // it's no longer required.
            using ( var field = new FieldReflector( Agent007, "id" ) )
            {
                Assert.NotNull( field.FieldInfo, $"Field named \"id\" not found in type {Agent007.GetType( )}" );
                Assert.NotNull( field.GetValue( Agent007 ), $"Value returned was null." );
            }
        }
        
        /// <summary>
        /// A <see cref="TargetException"/> is expected when accessing a reflected non-static field without providing a
        /// target object instance.
        /// </summary>
        [ Test ]
        public void Test_WeaklyTyped_TargetException_Get( )
        {
            using ( var field = new FieldReflector( typeof( Person ), "id" ) )
            {
                // This is a non-static field thus field access requires a target object instance.
                Assert.Catch< TargetException >( ( ) => field.GetValue( null ), "Expected TargetException to be raised when attempting to " +
                    "retrieve the value of non-static field." );
            }
        }

        /// <summary>
        /// Expect reflected field to exist in the inspected type, be non-null and of the requisite type. After modification,
        /// field.GetValue is expected to return the new value.
        /// </summary>
        [ Test ]
        public void Test_WeaklyTyped_Set( )
        {
            using ( var field = new FieldReflector( Agent007, "id" ) )
            {
                Assert.NotNull( field.FieldInfo, $"Field named \"id\" not found in type {Agent007.GetType( )}" );
                Assert.NotNull( field.GetValue( Agent007 ), $"Value returned was null." );

                var id = field.GetValue( Agent007 );
                field.SetValue( Agent007, Guid.NewGuid( ) );

                Assert.AreNotEqual( id, field.GetValue( Agent007 ), "Expected change in Agent007's id did not occur." );
            }
        }
        
        /// <summary>
        /// A <see cref="TargetException"/> is expected when accessing a reflected non-static field without providing a
        /// target object instance.
        /// </summary>
        [ Test ]
        public void Test_WeaklyTyped_TargetException_Set( )
        {
            using ( var field = new FieldReflector( typeof( Person ), "id" ) )
            {
                // This is a non-static field thus field access requires a target object instance.
                Assert.Catch< TargetException >( ( ) => field.SetValue( null, Guid.NewGuid( ) ), "Expected TargetException to be raised when attempting to " +
                    "assign the value of non-static field." );
            }
        }

        
        /// <summary>
        /// Expect reflected field to exist in the inspected type, be non-null and of the requisite type.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_Get( )
        {
            using ( var field = new FieldReflector< Guid >( Agent007, "id" ) )
            {
                Assert.NotNull( field.FieldInfo, $"Field named \"id\" not found in type {Agent007.GetType( )}" );
                Assert.NotNull( field.GetValue( Agent007 ), $"Value returned was null." );
                Assert.IsTrue( field.Value is Guid, $"Unexpected value type for field \"id\" in {Agent007.GetType( )}, " +
                    $"expected {typeof( Guid )}." );
            }
        }

        /// <summary>
        /// A <see cref="InvalidCastException"/> is expected when accessing a reflected vield as the wrong type.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_InvalidCast_Get( )
        {
            using ( var field = new FieldReflector< string >( Agent007, "id" ) )
            {
                Assert.Catch< InvalidCastException >( ( ) => field.GetValue( Agent007 ), "Expected InvalidCastException to be raised when attempting to" +
                    " retrieve field of type Guid as a string." );
            }
        }

        /// <summary>
        /// A <see cref="TargetException"/> is expected when accessing a reflected non-static field without providing a
        /// target object instance.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_TargetException_Get( )
        {
            using ( var field = new FieldReflector< Guid >( typeof( Person ), "id" ) )
            {
                // This is a non-static field thus field access requires a target object instance.
                Assert.Catch< TargetException >( ( ) => field.GetValue( null ), "Expected TargetException to be raised when attempting to " +
                    "retrieve the value of non-static field." );
            }
        }
        
        /// <summary>
        /// Expect reflected field to exist in the inspected type, be non-null and of the requisite type. After modification,
        /// field.GetValue is expected to return the new value.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_Set( )
        {
            using ( var field = new FieldReflector< Guid >( Agent007, "id" ) )
            {
                Assert.NotNull( field.FieldInfo, $"Field named \"id\" not found in type {Agent007.GetType( )}" );
                Assert.NotNull( field.GetValue( Agent007 ), $"Value returned was null." );

                var id = field.GetValue( Agent007 );
                field.SetValue( Agent007, Guid.NewGuid( ) );

                Assert.AreNotEqual( id, field.GetValue( Agent007 ), "Expected change in Agent007's id did not occur." );
            }
        }
        
        /// <summary>
        /// A <see cref="ArgumentException"/> is expected when accessing a reflected vield as the wrong type.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_InvalidCast_Set( )
        {
            using ( var field = new FieldReflector< string >( Agent007, "id" ) )
            {
                Assert.Catch< ArgumentException >( ( ) => field.SetValue( Agent007, "Abacus" ), "Expected InvalidCastException to be raised when attempting to" +
                    " retrieve field of type Guid as a string." );
            }
        }
        
        /// <summary>
        /// A <see cref="TargetException"/> is expected when accessing a reflected non-static field without providing a
        /// target object instance.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_TargetException_Set( )
        {
            using ( var field = new FieldReflector< Guid >( typeof( Person ), "id" ) )
            {
                // This is a non-static field thus field access requires a target object instance.
                Assert.Catch< TargetException >( ( ) => field.SetValue( null, Guid.NewGuid( ) ), "Expected TargetException to be raised when attempting to " +
                    "assign the value of non-static field." );
            }
        }
        
        /// <summary>
        /// Simple class definition used to test <see cref="FieldReflector"/> and <see cref="FieldReflector{TValue}"/>
        /// functionality against.
        /// </summary>
        private class Person
        {

            private Guid id;

            private List< string > tags;
            
            public Person( )
            {
                id = Guid.NewGuid( );
                tags = new List< string >( new[ ] { "secret", "agent", "licensed", "kill" } );
            }
            
        }
        
    }

}

