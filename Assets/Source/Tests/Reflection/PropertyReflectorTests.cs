using System;
using System.Collections.Generic;
using System.Reflection;

using NUnit.Framework;

using StudioEntropy.Reflection;


namespace StudioEntropy.Demo.Tests 
{

    [ TestFixture ]
    public class PropertyReflectorTests
    {
        private Person Agent007;
            
        [ OneTimeSetUp ]
        public void SetUp( )
        {
            Agent007 = new Person( );
        }

        /// <summary>
        /// Expect reflected property to exist in the inspected tpye and have a non-null value.
        /// </summary>
        [ Test ]
        public void Test_WeaklyTyped_Get( )
        {
            // Wrapping instantiation in a using statements allows for automatic disposable of the reflector when
            // it's no longer required.
            using ( var property = new PropertyReflector( Agent007, "id" ) )
            {
                Assert.NotNull( property.PropertyInfo, $"Property named \"id\" not found in type {Agent007.GetType( )}" );
                Assert.NotNull( property.GetValue( Agent007 ), $"Value returned was null." );
            }
        }
        
        /// <summary>
        /// A <see cref="TargetException"/> is expected when accessing a reflected non-static property without providing a
        /// target object instance.
        /// </summary>
        [ Test ]
        public void Test_WeaklyTyped_TargetException_Get( )
        {
            using ( var property = new PropertyReflector( typeof( Person ), "id" ) )
            {
                // This is a non-static field thus property access requires a target object instance.
                Assert.Catch< TargetException >( ( ) => property.GetValue( null ), "Expected TargetException to be raised when attempting to " +
                    "retrieve the value of non-static property." );
            }
        }

        /// <summary>
        /// Expect reflected property to exist in the inspected type, be non-null and of the requisite type. After modification,
        /// property.GetValue is expected to return the new value.
        /// </summary>
        [ Test ]
        public void Test_WeaklyTyped_Set( )
        {
            using ( var property = new PropertyReflector( Agent007, "id" ) )
            {
                Assert.NotNull( property.PropertyInfo, $"Property named \"id\" not found in type {Agent007.GetType( )}" );
                Assert.NotNull( property.GetValue( Agent007 ), $"Value returned was null." );

                var id = property.GetValue( Agent007 );
                property.SetValue( Agent007, Guid.NewGuid( ) );

                Assert.AreNotEqual( id, property.GetValue( Agent007 ), "Expected change in Agent007's id did not occur." );
            }
        }
        
        /// <summary>
        /// A <see cref="TargetException"/> is expected when accessing a reflected non-static property without providing a
        /// target object instance.
        /// </summary>
        [ Test ]
        public void Test_WeaklyTyped_TargetException_Set( )
        {
            using ( var property = new PropertyReflector( typeof( Person ), "id" ) )
            {
                // This is a non-static field thus field access requires a target object instance.
                Assert.Catch< TargetException >( ( ) => property.SetValue( null, Guid.NewGuid( ) ), "Expected TargetException to be raised when attempting to " +
                    "assign the value of non-static property." );
            }
        }

        
        /// <summary>
        /// Expect reflected property to exist in the inspected type, be non-null and of the requisite type.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_Get( )
        {
            using ( var property = new PropertyReflector< Guid >( Agent007, "id" ) )
            {
                Assert.NotNull( property.PropertyInfo, $"Property named \"id\" not found in type {Agent007.GetType( )}" );
                Assert.NotNull( property.GetValue( Agent007 ), $"Value returned was null." );
                Assert.IsTrue( property.Value is Guid, $"Unexpected value type for property \"id\" in {Agent007.GetType( )}, " +
                    $"expected {typeof( Guid )}." );
            }
        }

        /// <summary>
        /// A <see cref="InvalidCastException"/> is expected when accessing a reflected property as the wrong type.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_InvalidCast_Get( )
        {
            using ( var property = new PropertyReflector< string >( Agent007, "id" ) )
            {
                Assert.Catch< InvalidCastException >( ( ) => property.GetValue( Agent007 ), "Expected InvalidCastException to be raised when attempting to" +
                    " retrieve property of type Guid as a string." );
            }
        }

        /// <summary>
        /// A <see cref="TargetException"/> is expected when accessing a reflected non-static property without providing a
        /// target object instance.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_TargetException_Get( )
        {
            using ( var property = new PropertyReflector< Guid >( typeof( Person ), "id" ) )
            {
                // This is a non-static field thus field access requires a target object instance.
                Assert.Catch< TargetException >( ( ) => property.GetValue( null ), "Expected TargetException to be raised when attempting to " +
                    "retrieve the value of non-static property." );
            }
        }
        
        /// <summary>
        /// Expect reflected field to exist in the inspected type, be non-null and of the requisite type. After modification,
        /// field.GetValue is expected to return the new value.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_Set( )
        {
            using ( var property = new PropertyReflector< Guid >( Agent007, "id" ) )
            {
                Assert.NotNull( property.PropertyInfo, $"Field named \"id\" not found in type {Agent007.GetType( )}" );
                Assert.NotNull( property.GetValue( Agent007 ), $"Value returned was null." );

                var id = property.GetValue( Agent007 );
                property.SetValue( Agent007, Guid.NewGuid( ) );

                Assert.AreNotEqual( id, property.GetValue( Agent007 ), "Expected change in Agent007's id did not occur." );
            }
        }
        
        /// <summary>
        /// A <see cref="ArgumentException"/> is expected when accessing a reflected property as the wrong type.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_InvalidCast_Set( )
        {
            using ( var property = new PropertyReflector< string >( Agent007, "id" ) )
            {
                Assert.Catch< ArgumentException >( ( ) => property.SetValue( Agent007, "Abacus" ), "Expected InvalidCastException to be raised when attempting to" +
                    " retrieve field of type Guid as a string." );
            }
        }
        
        /// <summary>
        /// A <see cref="TargetException"/> is expected when accessing a reflected non-static property without providing a
        /// target object instance.
        /// </summary>
        [ Test ]
        public void Test_StronglyTyped_TargetException_Set( )
        {
            using ( var property = new PropertyReflector< Guid >( typeof( Person ), "id" ) )
            {
                // This is a non-static field thus field access requires a target object instance.
                Assert.Catch< TargetException >( ( ) => property.SetValue( null, Guid.NewGuid( ) ), "Expected TargetException to be raised when attempting to " +
                    "assign the value of non-static field." );
            }
        }
        
        /// <summary>
        /// Simple class definition used to test <see cref="PropertyReflector"/> and <see cref="PropertyReflector{TValue}"/>
        /// functionality against.
        /// </summary>
        private class Person
        {

            private Guid id { get; set; }

            private List< string > tags;
            
            public Person( )
            {
                id = Guid.NewGuid( );
                tags = new List< string >( new[ ] { "secret", "agent", "licensed", "kill" } );
            }
            
        }
        
    }

}
