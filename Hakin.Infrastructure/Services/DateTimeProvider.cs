using Hakin.Application.Common.Interfaces.Services;

namespace Hakin.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}