namespace AdventureManagement.MVC.Models
{
    public class ParticipantDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int AdventureCount { get; set; }
        public List<AdventureDetailVM> Adventures { get; set; } = new List<AdventureDetailVM>();
    }
}
