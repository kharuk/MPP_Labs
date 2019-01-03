namespace ValidationLib.Attributes.Interfaces
{
    /// <summary>
    /// IValidationAttribute is common interface, that must be
    /// implemented by other Validation Attributes realized
    /// in this project.
    /// </summary>
    public interface IValidationAttribute
    {
        /// <summary>
        /// Property error message
        /// </summary>
        /// <value>
        /// This property set the value when validation is failed
        /// and get the value to use this somewhere.
        /// </value>
        string ErrorMessage { get; set; }


        /// <summary>
        /// Method verify whether the object is valid.
        /// </summary>
        /// <param name="value">Object to verify.</param>
        /// <returns>Return true if object is valid and false in another case.</returns>
        bool IsValid(object value);
    }
}
