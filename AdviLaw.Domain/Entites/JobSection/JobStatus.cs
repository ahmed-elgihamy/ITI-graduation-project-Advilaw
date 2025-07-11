namespace AdviLaw.Domain.Entites.JobSection
{
    public enum JobStatus
    {
        WaitingApproval = 0,
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
