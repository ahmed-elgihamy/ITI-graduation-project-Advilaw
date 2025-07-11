public class LawyerProfileDTO
{
    public string Id { get; set; }  
    public string Name { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int Experience { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    public decimal HourlyRate { get; set; }
    public int Points { get; set; } // Add this property
}
