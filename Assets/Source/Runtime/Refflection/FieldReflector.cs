using System;
using System.Reflection;


namespace StudioEntropy.Reflection 
{

    /// <summary>
    /// Base type from which field reflectors wrapper are derived.
    /// </summary>
    public abstract class BaseFieldReflector : BaseReflector
    {

        /// <summary>
        /// The reflected field.
        /// </summary>
        public FieldInfo FieldInfo { get; }

        /// <summary>
        /// Constructs an instance of a reflector.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to retrieve a member from.</param>
        /// <param name="memberName">The name of the member to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the member.</param>
        protected BaseFieldReflector( Type type, string memberName, BindingFlags filter = DefaultFilter )
            : base( type, memberName, filter )
        {
            if( MemberInfo.MemberType == MemberTypes.Field )
                FieldInfo = MemberInfo as FieldInfo;
        }

        /// <summary>
        /// Constructs an instance of a reflector.
        /// </summary>
        /// <param name="target">The <see cref="object"/> instance to retrieve a member from.</param>
        /// <param name="memberName">The name of the member to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the member.</param>
        protected BaseFieldReflector( object target, string memberName, BindingFlags filter = DefaultFilter )
            : base( target, memberName, filter )
        {
            if( MemberInfo.MemberType == MemberTypes.Field )
                FieldInfo = MemberInfo as FieldInfo;
        }
        
    }

    /// <summary>
    /// A weakly typed wrapper for accessing fields of a type via reflection.
    /// </summary>
    public class FieldReflector : BaseFieldReflector
    {

        /// <summary>
        /// Gets or sets the value of the reflected field.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if <see cref="BaseReflector.Target"/> is null.</exception>
        public object Value
        {
            get => GetValue( Target );
            set => SetValue( Target, value );
        }
        
        /// <summary>
        /// Constructs an instance of a reflector.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to retrieve a member from.</param>
        /// <param name="memberName">The name of the member to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the member.</param>
        public FieldReflector( Type type, string memberName, BindingFlags filter = DefaultFilter )
            : base( type, memberName, filter ) { }

        /// <summary>
        /// Constructs an instance of a reflector.
        /// </summary>
        /// <param name="target">The <see cref="object"/> instance to retrieve a member from.</param>
        /// <param name="memberName">The name of the member to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the member.</param>
        public FieldReflector( object target, string memberName, BindingFlags filter = DefaultFilter )
            : base( target, memberName, filter ) { }
        
        
        /// <summary>
        /// Sets the value of the reflected field.
        /// </summary>
        /// <param name="target">The object instance on which to assign the reflected fields value.</param>
        /// <param name="value">The new value that should be assigned to the reflected field.</param>
        public void SetValue( object target, object value )
        {
            FieldInfo.SetValue( target, value );
        }

        /// <summary>
        /// Gets the value of the reflected field.
        /// </summary>
        /// <param name="target">The object instance from which the value of the reflected field should be
        /// retrieved.</param>
        /// <returns></returns>
        public object GetValue( object target )
        {
            return FieldInfo.GetValue( target );
        }
        
    }

    /// <summary>
    /// A strongly typed wrapper for accessing fields of a type via reflection.
    /// </summary>
    public class FieldReflector< TValue > : BaseFieldReflector
    {
        /// <summary>
        /// Gets or sets the value of the reflected field.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown if <see cref="BaseReflector.Target"/> is null.</exception>
        public TValue Value
        {
            get => GetValue( Target );
            set => SetValue( Target, value );
        }
        
        /// <summary>
        /// Constructs an instance of a reflector.
        /// </summary>
        /// <param name="memberName">The name of the member to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the member.</param>
        public FieldReflector( string memberName, BindingFlags filter = DefaultFilter )
            : base( typeof( TValue ), memberName, filter ) { }

        /// <summary>
        /// Constructs an instance of a reflector.
        /// </summary>
        /// <param name="target">The <see cref="object"/> instance to retrieve a member from.</param>
        /// <param name="memberName">The name of the member to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the member.</param>
        public FieldReflector( object target, string memberName, BindingFlags filter = DefaultFilter )
            : base( target, memberName, filter ) { }
        
        
        /// <summary>
        /// Sets the value of the reflected field.
        /// </summary>
        /// <param name="target">The object instance on which to assign the reflected fields value.</param>
        /// <param name="value">The new value that should be assigned to the reflected field.</param>
        public void SetValue( object target, TValue value )
        {
            FieldInfo.SetValue( target, value );
        }

        /// <summary>
        /// Gets the value of the reflected field.
        /// </summary>
        /// <param name="target">The object instance from which the value of the reflected field should be
        /// retrieved.</param>
        /// <returns></returns>
        public TValue GetValue( object target )
        {
            return ( TValue )FieldInfo.GetValue( target );
        }
    }

}
