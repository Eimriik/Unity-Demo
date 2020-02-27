using System;


namespace StudioEntropy.Exposition 
{

    /// <summary>
    /// Decorate a proeprty with attribute to make it known to the <see cref="ExpositionService"/>.
    /// </summary>
    public class ExposedPropertyAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the handle used to reference this property.
        /// </summary>
        /// <remarks>
        /// Using a handle ensures that code doesn't break should the property name change.
        /// </remarks>
        public string Handle { get; set; }
    }

}
