namespace BloodBankManagementSystem.Models
{
    public class DonateBlood
    {
        public string DonorId { get; set; }

        public string FullName { get; set; }

        public DateTime DOB { get; set; }

        public string City { get; set; }

        public string Gender { get; set; }

        public string Blood_Group { get; set; }

        public string Weight { get; set;}

        public string Date_Of_Last_Donation { get; set; }


        public string How_Many_Times { get; set; }

        public string Phone_Number { get; set; }

        public string EmailId { get; set; }

        public string Status { get; set; }
    }
}
