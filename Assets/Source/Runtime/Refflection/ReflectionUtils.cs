using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace StudioEntropy.Reflection 
{

    /// <summary>
    /// Provides reflection based utilities.
    /// </summary>
    public static class ReflectionUtils
    {

        /// <summary>
        /// Returns a collection of types decorated with the <see cref="TAttribute"/> attribute. 
        /// </summary>
        /// <remarks>
        /// .Net assemblies are exluded from the searched assemblies.
        /// </remarks>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static IEnumerable< Type > GetTypesDecoratedWith< TAttribute >( )
            where TAttribute : Attribute
        {
            return  from assembly in AppDomain.CurrentDomain.GetAssemblies( )
                    where !assembly.IsDefined( typeof( AssemblyProductAttribute ) )
                    from type in assembly.GetTypes( )
                    where type.IsDefined( typeof( TAttribute ) )
                    select type;
        }

    }

}
