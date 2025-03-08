using Microsoft.AspNetCore.Identity;
namespace WritingAssistant.Models
{
    public class User : IdentityUser<int> // sử dụng IdentityUser<int> thay vì IdentityUser<string>
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
