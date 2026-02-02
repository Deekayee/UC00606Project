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
            DateChanged?.Invoke();
        }

        public void ResetDate()
        {
            CurrentDate = DateTime.Now;
            DateChanged?.Invoke();
        }

        public event Action? DateChanged;
    }
}
