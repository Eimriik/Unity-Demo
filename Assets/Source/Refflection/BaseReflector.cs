
using System;
using System.Linq;
using System.Reflection;


namespace StudioEntropy.Reflection
{
    /// <summary>
    /// A wrapper for accessing members of an object via reflection.
    /// </summary>
    public abstract class BaseReflector : IDisposable
    {
        /// <summary>
        /// Default binding flags used to filter type members.
        /// </summary>
        protected const BindingFlags DefaultFilter = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        
        /// <summary>
        /// The found member. May be null.
        /// </summary>
        protected MemberInfo MemberInfo { get; private set; }

        /// <summary>
        /// The target object. Members are retrived from this object instance.
        /// </summary>
        protected object Target { get; }

        
        /// <summary>
        /// Constructs an instance of a reflector.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to retrieve a member from.</param>
        /// <param name="memberName">The name of the member to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the member.</param>
        protected BaseReflector( Type type, string memberName, BindingFlags filter = DefaultFilter )
        {
            MemberInfo = type.GetMember( memberName, filter ).FirstOrDefault( );
        }
        
        /// <summary>
        /// Constructs an instance of a reflector.
        /// </summary>
        /// <param name="target">The <see cref="object"/> instance to retrieve a member from.</param>
        /// <param name="memberName">The name of the member to retrieve.</param>
        /// <param name="filter"><see cref="BindingFlags"/> filter to use when attempting to retrieve the member.</param>
        protected BaseReflector( object target, string memberName, BindingFlags filter = DefaultFilter ) : 
            this( target.GetType( ), memberName, filter )
        {
            Target = target;
        }
        

        /// <summary>
        /// Disposes this reflector.
        /// </summary>
        public void Dispose( )
        {
            MemberInfo = null;
        }

    }

}

