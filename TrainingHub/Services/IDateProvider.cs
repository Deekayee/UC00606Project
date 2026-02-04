using System;

namespace TrainingHub.Services;

public interface IDateProvider
{
    DateTime Today { get; }
    void AdvanceDays(int days);
    void ResetDate();
    event Action DateChanged;
}