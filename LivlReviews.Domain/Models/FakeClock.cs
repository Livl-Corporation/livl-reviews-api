using LivlReviews.Domain.Domain_interfaces_output;

namespace LivlReviews.Domain.Models;

public class FakeClock(DateTime fakeDateTime) : IClock
{
    public DateTime Now => fakeDateTime;
}