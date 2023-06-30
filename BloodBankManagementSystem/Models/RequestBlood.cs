namespace BloodBankManagementSystem.Models
{
    public class RequestBlood
    {
        public string RequestorId { get; set; }

        public string Patient_Name { get; set; }

        public string Requested_Blood_Group { get; set; }

        public string City { get; set; }

        public string DoctorName { get; set; }

        public string Hospital_Name_Address { get; set; }

        public DateOnly Blood_required_Date { get; set; }

        public string Contact_Name { get; set; }

        public string Contact_Number { get; set;}

        public string Contact_Email_Id { get; set;}

        public string Message { get; set;}
    }
}
