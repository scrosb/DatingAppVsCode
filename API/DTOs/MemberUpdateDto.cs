namespace API.DTOs
{
    //In order to allow the member to update their profile we need to add a DTO. 
    public class MemberUpdateDto
    {
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}