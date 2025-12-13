using System;

namespace TrainingHub.Services
{
    public class DateProvider : IDateProvider
    {
        // Field
        private DateTime CurrentDate;

        // Constructor
        public DateProvider()
        {
            CurrentDate = DateTime.Now;
        }

        // Property
        public DateTime Today => CurrentDate.Date;

        // Method
        public void AdvanceDays(int days)
        {
            CurrentDate = CurrentDate.AddDays(days);
        }
    }
}
