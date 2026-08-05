using MedLink.Domain.Enums;
namespace MedLink.Domain.Entities;

public class BranchWorkingHour : BaseEntity
{
    public int BranchId { get; set; }
    public Enums.DayOfWeek DayOfWeek { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
    public bool IsClosed { get; set; }

    public Branch Branch { get; set; }


}
