namespace server.Data.Entites.JobSection
{
    public enum JobStatus
    {
        NotAssigned = 1,
        WaitingAppointment,
        WaitingPayment,
        NotStarted,
        
        Started,
        Ended,
    }
}
