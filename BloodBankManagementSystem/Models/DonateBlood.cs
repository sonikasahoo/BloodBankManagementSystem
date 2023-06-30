namespace BloodBankManagementSystem.Models
{
    public class DonateBlood
    {
        public string Donor_Id { get; set; }

        public string FullName { get; set; }

        public DateOnly DOB { get; set; }

        public string City { get; set; }

        public string Gender { get; set; }

        public string Blood_Group { get; set; }

        public string Weight { get; set;}

        public DateOnly Date_Of_Last_Donation { get; set; }


        public string How_Many_Times { get; set; }

        public string Phone_Number { get; set; }

        public string EmailId { get; set; }

        public string Status { get; set; }
    }
}
