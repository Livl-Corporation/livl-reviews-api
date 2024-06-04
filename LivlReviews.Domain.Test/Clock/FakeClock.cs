namespace LivlReviews.Domain.Test.Clock;

public class FakeClock(DateTime fakeDateTime) : IClock
{
    public DateTime Now => fakeDateTime;
}