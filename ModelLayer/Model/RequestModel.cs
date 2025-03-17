using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Model
{
    public class RequestModel
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(15)]
        public string PhoneNumber { get; set; }

        [EmailAddress, MaxLength(255)]
        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}