using Microsoft.AspNetCore.Identity;

namespace MovieStoreMvc.Models.Domain
{
    //IdentityUser cung cấp tính năng cần thiết quản lý người dùng 
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}