namespace AdviLaw.Domain.Entites.JobSection
{
    public enum JobStatus
    {
        NotAssigned = 1,
        WaitingAppointment,
        WaitingPayment,
        NotStarted,
            
        LawyerRequestedAppointment,
        ClientRequestedAppointment,

        Started,
        Ended,
    }
}
