namespace BloodBankManagementSystem.Models
{
    public class RequestStatus
    {
        public string RequestorId { get; set; }

        public string PatientId { get; set; }

        public DateTime Time_Of_The_Day { get; set;}

        public string Blood_Glucose_Level { get; set; }

        public string Notes { get; set; }
    }
}
