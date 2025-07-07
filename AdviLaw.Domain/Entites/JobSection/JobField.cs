namespace AdviLaw.Domain.Entites.JobSection
{
    public class JobField
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation Properties
        public List<Job> Jobs { get; set; } = new();
        public List<LawyerJobField> LawyerJobs { get; set; } = new();
    }
}


