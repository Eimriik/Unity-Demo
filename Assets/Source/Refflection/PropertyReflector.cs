using System;
using System.Reflection;


namespace StudioEntropy.Reflection 
{

    /// <summary>
    /// Base type from which property reflector wrappers are derived.
    /// </summary>
    public abstract class BasePropertyReflector : BaseReflector
    {

        /// <summary>
        /// The reflected property.
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// Constructs an instance of a property reflector.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to retrieve a property from.</param>
        /// <param name="memberName">The name of the property to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the property.</param>
        protected BasePropertyReflector( Type type, string memberName, BindingFlags filter = DefaultFilter )
            : base( type, memberName, filter )
        {
            if( MemberInfo.MemberType == MemberTypes.Property )
                PropertyInfo = MemberInfo as PropertyInfo;
        }

        /// <summary>
        /// Constructs an instance of a reflector.
        /// </summary>
        /// <param name="target">The <see cref="object"/> instance to retrieve a property from.</param>
        /// <param name="memberName">The name of the property to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the property.</param>
        protected BasePropertyReflector( object target, string memberName, BindingFlags filter = DefaultFilter )
            : base( target, memberName, filter )
        {
            if( MemberInfo.MemberType == MemberTypes.Property )
                PropertyInfo = MemberInfo as PropertyInfo;
        }
        
    }

    /// <summary>
    /// A weakly typed wrapper for accessing properties of a type via reflection.
    /// </summary>
    public class PropertyReflector : BasePropertyReflector
    {

        /// <summary>
        /// Gets or sets the value of the reflected property.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if <see cref="BaseReflector.Target"/> is null.</exception>
        public object Value
        {
            get => GetValue( Target );
            set => SetValue( Target, value );
        }
        
        /// <summary>
        /// Constructs an instance of a property reflector.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to retrieve a property from.</param>
        /// <param name="memberName">The name of the property to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the property.</param>
        public PropertyReflector( Type type, string memberName, BindingFlags filter = DefaultFilter )
            : base( type, memberName, filter ) { }

        /// <summary>
        /// Constructs an instance of a property reflector.
        /// </summary>
        /// <param name="target">The <see cref="object"/> instance to retrieve a property from.</param>
        /// <param name="memberName">The name of the property to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the property.</param>
        public PropertyReflector( object target, string memberName, BindingFlags filter = DefaultFilter )
            : base( target, memberName, filter ) { }
        
        
        /// <summary>
        /// Sets the value of the reflected field.
        /// </summary>
        /// <param name="target">The object instance on which to assign the value of the reflected property.</param>
        /// <param name="value">The new value that should be assigned to the reflected property.</param>
        public void SetValue( object target, object value )
        {
            PropertyInfo.SetValue( target, value );
        }

        /// <summary>
        /// Gets the value of the reflected property.
        /// </summary>
        /// <param name="target">The object instance from which the value of the reflected property should be
        /// retrieved.</param>
        /// <returns></returns>
        public object GetValue( object target )
        {
            return PropertyInfo.GetValue( target );
        }
        
    }

    /// <summary>
    /// A strongly typed wrapper for accessing fields of a type via reflection.
    /// </summary>
    public class PropertyReflector< TValue > : BasePropertyReflector
    {
        /// <summary>
        /// Gets or sets the value of the reflected property.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if <see cref="BaseReflector.Target"/> is null.</exception>
        public TValue Value
        {
            get => GetValue( Target );
            set => SetValue( Target, Value );
        }
        
        /// <summary>
        /// Constructs an instance of a property reflector.
        /// </summary>
        /// <param name="memberName">The name of the property to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the property.</param>
        public PropertyReflector( string memberName, BindingFlags filter = DefaultFilter )
            : base( typeof( TValue ), memberName, filter ) { }

        /// <summary>
        /// Constructs an instance of a property reflector.
        /// </summary>
        /// <param name="target">The <see cref="object"/> instance to retrieve a property from.</param>
        /// <param name="memberName">The name of the property to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the property.</param>
        public PropertyReflector( object target, string memberName, BindingFlags filter = DefaultFilter )
            : base( target, memberName, filter ) { }
        
        
        /// <summary>
        /// Sets the value of the reflected field.
        /// </summary>
        /// <param name="target">The object instance on which to assign the value of the reflected property.</param>
        /// <param name="value">The new value that should be assigned to the reflected property.</param>
        public void SetValue( object target, TValue value )
        {
            PropertyInfo.SetValue( target, value );
        }

        /// <summary>
        /// Gets the value of the reflected field.
        /// </summary>
        /// <param name="target">The object instance from which the value of the reflected property should be retrieved.</param>
        /// <returns></returns>
        public TValue GetValue( object target )
        {
            return ( TValue )PropertyInfo.GetValue( target );
        }
    }
}
