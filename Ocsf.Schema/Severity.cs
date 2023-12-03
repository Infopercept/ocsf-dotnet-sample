namespace OcsfDemo.Schema
{
    public enum Severity
    {
        /// <summary>
        /// The event severity is not known.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///  Informational message. No action required.
        /// </summary>
        Informational = 1,

        /// <summary>
        /// The user decides if action is needed.
        /// </summary>
        Low = 2,

        /// <summary>
        /// Action is required but the situation is not serious at this time.
        /// </summary>
        Medium = 3,

        /// <summary>
        /// Action is required immediately.
        /// </summary>
        High = 4,

        /// <summary>
        /// Action is required immediately and the scope is broad.
        /// </summary>
        Critical = 5,

        /// <summary>
        /// An error occurred but it is too late to take remedial action.
        /// </summary>
        Fatal = 6,

        /// <summary>
        /// The event severity is not mapped. See the severity attribute, which contains a data source specific value.
        /// </summary>
        Other = 99,
    }
}
