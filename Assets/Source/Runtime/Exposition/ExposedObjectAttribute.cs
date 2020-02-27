using System;


namespace StudioEntropy.Exposition
{

    /// <summary>
    /// Decorate a class with this attribute to make it known to the <see cref="ExpositionService"/>.
    /// </summary>
    [ AttributeUsage( AttributeTargets.Class ) ]
    public class ExposedObjectAttribute : Attribute
    {

    }

}
