using System;

namespace TrainingHub.Services;

public interface IDateProvider
{
    DateTime Today { get; }
    void AdvanceDays(int days);
}