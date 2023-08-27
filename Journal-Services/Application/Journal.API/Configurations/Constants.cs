namespace Journal.API.Configurations;

/// <summary>
/// Static class that defines constants to be used across the system
/// </summary>
public static class Constants
{

    /// <summary>
    /// Defines constants for swagger configuration
    /// </summary>
    public static class Swagger
    {
        /// <summary>
        /// Represents the group name of the general Swagger API definition.
        /// The general API is the definition with all endpoints.
        /// </summary>
        public const string GENERAL_API = "general";

        /// <summary>
        /// Represents the group name of the mobile Swagger API definition.
        /// The mobile API is the definition with only the endpoints available for mobile application
        /// </summary>
        public const string MOBILE_API = "mobile";
    }
}