using System;

using JetBrains.Annotations;

using Zenject;


namespace StudioEntropy.Injection 
{
    
    /// <summary>
    /// Used to decorate a "Constructor" method for use by Zenject when constructing Monobehaviours.
    /// </summary>
    [ MeansImplicitUse, AttributeUsage( AttributeTargets.Method ) ]
    public sealed class MonoConstructorAttribute : InjectAttribute { }
    
    /// <summary>
    /// Denotes that the parameter of a Monobehaviours "constructor" <see cref="MonoConstructorAttribute"/> method is
    /// optional.
    /// </summary>
    [ MeansImplicitUse, AttributeUsage( AttributeTargets.Parameter ) ]
    public sealed class OptionalAttribute : InjectOptionalAttribute { }
    
    /// <summary>
    /// Identifies field and property members as members whos values should be provided via dependency injection.
    /// </summary>
    /// <remarks>
    /// This attribute is decorated with the <see cref="UsedImplicitlyAttribute"/> to suppress unecessary "variable not
    /// assigned" warnings.
    /// </remarks>
    [ MeansImplicitUse ]
    public sealed class DependencyAttribute : InjectAttribute { }

}
