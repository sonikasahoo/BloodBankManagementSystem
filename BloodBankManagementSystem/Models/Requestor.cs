namespace BloodBankManagementSystem.Models
{
    public class Requestor
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public DateOnly DOB { get; set; }

        public string Address { get; set; }

        public string ContactNo { get; set; }

        public string EmailId { get; set; }

        public string Gender { get; set; }

        public int RequestorId { get; set; }
    }
}
