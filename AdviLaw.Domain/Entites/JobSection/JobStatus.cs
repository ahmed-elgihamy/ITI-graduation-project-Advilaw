namespace AdviLaw.Domain.Entites.JobSection
{
    public enum JobStatus
    {
        WaitingApproval = 0,
        NotAssigned = 1,
        WaitingAppointment=2,
        WaitingPayment=3,
        NotStarted=4,
        LawyerRequestedAppointment=5,
        ClientRequestedAppointment=6,
        Accepted ,
        Rejected ,
        Started,
        Ended,
    }
}
