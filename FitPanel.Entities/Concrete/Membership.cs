namespace FitPanel.Entities.Concrete
{
    public class Membership
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int SportPackageId { get; set; }
        public SportPackage? SportPackage { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
