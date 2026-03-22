namespace PM_2._0.Classes
{
    public class TeamWorker
    {
        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;
        public int WorkerId { get; set; }
        public Worker Worker { get; set; } = null!;
    }
}
