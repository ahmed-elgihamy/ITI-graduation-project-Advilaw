namespace AdviLaw.Domain.Entites.SessionSection
{
    public enum SessionStatus
    {
        //Before Starting
        NotStarted = 0,
        //Pressing Ready on Session
        ClientReady = 1,
        LawyerReady = 2,
        //Starting
        Started = 3,
        //Actions Within Session
        Reported = 4,
        //Final Result
        Refunded = 5,
        Completed = 6,
    }
}
