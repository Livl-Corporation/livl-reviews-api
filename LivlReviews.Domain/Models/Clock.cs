using LivlReviews.Domain.Domain_interfaces_output;

namespace LivlReviews.Domain.Models;

public class Clock : IClock
{
    public DateTime Now => DateTime.Now;
}