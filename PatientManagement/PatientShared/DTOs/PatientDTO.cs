namespace PatientShared.DTOs
{
    /// <summary>
    /// This is a Data transfer object
    /// A DTO is a simple class used to transfer data between different layers of an application,
    /// such as between your Web API and frontend, without exposing your database/entity
    /// models directly.
    /// </summary>
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}