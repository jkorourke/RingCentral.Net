namespace RingCentral
{
    public class GlipTaskRecurrenceInfo
    {
        /// <summary>
        /// Task recurrence settings. For non-periodic tasks the value is 'None'
        /// Enum: None, Daily, Weekdays, Weekly, Monthly, Yearly
        /// </summary>
        public string schedule;

        /// <summary>
        /// Task ending condition
        /// Enum: None, Count, Date
        /// </summary>
        public string endingCondition;

        /// <summary>
        /// Count of iterations of periodic tasks
        /// Maximum: 10
        /// Minimum: 1
        /// </summary>
        public long? endingAfter;

        /// <summary>
        /// End date of periodic task
        /// </summary>
        public string endingOn;
    }
}