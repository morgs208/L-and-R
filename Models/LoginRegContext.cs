using Microsoft.EntityFrameworkCore;


namespace LoginRegistration.Models
{
    public class LoginRegContext : DbContext
    {
        public LoginRegContext(DbContextOptions options) : base(options) { }
        //  public DbSet<User> Dishes {get;set;}
        public DbSet<User> Users {get; set;}

    }
}