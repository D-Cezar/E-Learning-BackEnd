using E_Learning.DB.Models;
using Microsoft.EntityFrameworkCore;
using static E_Learning.Constants;

namespace E_Learning.DB
{
    public class MyDBContext : DbContext
    {
        public DbSet<Users> Users { set; get; }
        public DbSet<Courses> Courses { set; get; }
        public DbSet<CourseSections> CourseSections { set; get; }
        public DbSet<CoursesUsers> CoursesUsers { get; set; }
        public DbSet<UserAnswers> UserAnswers { set; get; }
        public DbSet<Questions> Questions { set; get; }

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoursesUsers>()
                .HasKey(cu => new { cu.UserId, cu.CourseId });

            modelBuilder.Entity<CoursesUsers>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.CoursesUsers)
                .HasForeignKey(cu => cu.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CoursesUsers>()
                .HasOne(cu => cu.Course)
                .WithMany(c => c.CoursesUsers)
                .HasForeignKey(cu => cu.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserAnswers>()
               .HasOne(ua => ua.User)
               .WithMany(u => u.UserAnswers)
               .HasForeignKey(ua => ua.UserId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserAnswers>()
                .HasOne(ua => ua.Question)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Users>().HasData(
    new Users
    {
        Id = 1,
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe@example.com",
        Role = 0,
        Password = BCrypt.Net.BCrypt.HashPassword("password123")
    },
        new Users
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("mypassword")
        },
        new Users
        {
            Id = 3,
            FirstName = "Robert",
            LastName = "Brown",
            Email = "robert.brown@example.com",
            Role = Roles.Teacher,
            Password = BCrypt.Net.BCrypt.HashPassword("teachpass")
        },
        new Users
        {
            Id = 4,
            FirstName = "Emily",
            LastName = "Clark",
            Email = "emily.clark@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("emilypass")
        },
        new Users
        {
            Id = 5,
            FirstName = "Net",
            LastName = "Ninja",
            Email = "netninja@example.com",
            Role = Roles.Teacher,
            Password = BCrypt.Net.BCrypt.HashPassword("NetNinjaBest")
        },
        // New users
        new Users
        {
            Id = 6,
            FirstName = "Alice",
            LastName = "Johnson",
            Email = "alice.johnson@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("alicepass")
        },
        new Users
        {
            Id = 7,
            FirstName = "Michael",
            LastName = "Davis",
            Email = "michael.davis@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("mikepass")
        },
        new Users
        {
            Id = 8,
            FirstName = "Sarah",
            LastName = "Wilson",
            Email = "sarah.wilson@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("sarah123")
        },
        new Users
        {
            Id = 9,
            FirstName = "David",
            LastName = "Taylor",
            Email = "david.taylor@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("davidpass")
        },
        new Users
        {
            Id = 10,
            FirstName = "Laura",
            LastName = "Martinez",
            Email = "laura.martinez@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("laurapass")
        },
        new Users
        {
            Id = 11,
            FirstName = "Kevin",
            LastName = "Moore",
            Email = "kevin.moore@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("kevinpassword")
        },
        new Users
        {
            Id = 12,
            FirstName = "Sophia",
            LastName = "White",
            Email = "sophia.white@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("sophiapass")
        },
        new Users
        {
            Id = 13,
            FirstName = "Paul",
            LastName = "Harris",
            Email = "paul.harris@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("paulpassword")
        },
        new Users
        {
            Id = 14,
            FirstName = "Anna",
            LastName = "Walker",
            Email = "anna.walker@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("annapass")
        },
        new Users
        {
            Id = 15,
            FirstName = "Thomas",
            LastName = "Lee",
            Email = "thomas.lee@example.com",
            Role = 0,
            Password = BCrypt.Net.BCrypt.HashPassword("thomaspass")
        }
);

            modelBuilder.Entity<Courses>().HasData(
                 new Courses { Id = 1, Title = "Full React Course", Description = "A comprehensive course on React, covering all the essential topics for web development.", AuthorId = 5, Type = "Web Development", ImageSourse = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQcR5U16C8yXgBpl7-Bc7Itjx3_LRl425zINA&s" }
             );

            modelBuilder.Entity<CourseSections>().HasData(
                new CourseSections { Id = 1, Title = "Setting Up", TextSource = "Configurare initiala pentru dezvoltarea aplicatiilor React.", VideoSource = "https://www.youtube.com/watch?v=xehoCy7k0DE", CourseId = 1 },
                new CourseSections { Id = 2, Title = "JSX Basics", TextSource = "Bazele JSX: Syntactic sugar pentru JavaScript.", VideoSource = "https://www.youtube.com/watch?v=QFaFIcGhPoM", CourseId = 1 },
                new CourseSections { Id = 3, Title = "Components & Templates", TextSource = "Cum sa construim componente si template-uri in React.", VideoSource = "https://www.youtube.com/watch?v=XxVg_s8xAms", CourseId = 1 },
                new CourseSections { Id = 4, Title = "Dynamic Values in Templates", TextSource = "Valori dinamice in template-uri React.", VideoSource = "https://www.youtube.com/watch?v=ftI2bcu_bYk", CourseId = 1 },
                new CourseSections { Id = 5, Title = "Event Basics", TextSource = "Bazele evenimentelor in React.", VideoSource = "https://www.youtube.com/watch?v=kVeOpcw4GWY", CourseId = 1 },
                new CourseSections { Id = 6, Title = "React State (useState hook)", TextSource = "Utilizarea hook-ului useState pentru state in React.", VideoSource = "https://www.youtube.com/watch?v=O6P86uwfdR0", CourseId = 1 },
                new CourseSections { Id = 7, Title = "Intro to Forms", TextSource = "Introducere in formulare cu React.", VideoSource = "https://www.youtube.com/watch?v=owld54-sf6k", CourseId = 1 },
                new CourseSections { Id = 8, Title = "Controlled Inputs (forms)", TextSource = "Gestionarea formularelor controlate in React.", VideoSource = "https://www.youtube.com/watch?v=Pke1S-B0mHM", CourseId = 1 },
                new CourseSections { Id = 9, Title = "useEffect Hook (the basics)", TextSource = "Bazele hook-ului useEffect in React.", VideoSource = "https://www.youtube.com/watch?v=ML7FLpaJszk", CourseId = 1 },
                new CourseSections { Id = 10, Title = "useEffect Dependencies", TextSource = "Gestionarea dependentelor in useEffect.", VideoSource = "https://www.youtube.com/watch?v=dGcsHMXbSOA", CourseId = 1 },
                new CourseSections { Id = 11, Title = "Cleaning up with useEffect", TextSource = "Curatarea resurselor cu useEffect.", VideoSource = "https://www.youtube.com/watch?v=TLr8hdDy7PY", CourseId = 1 },
                new CourseSections { Id = 12, Title = "Fetching Data with useEffect", TextSource = "Obtinerea datelor cu hook-ul useEffect.", VideoSource = "https://www.youtube.com/watch?v=jQc_bTFZ5_I", CourseId = 1 },
                new CourseSections { Id = 13, Title = "Conditional Loading Message", TextSource = "Afisarea mesajelor de incarcare conditionate.", VideoSource = "https://www.youtube.com/watch?v=CVpUuw9XSjY", CourseId = 1 },
                new CourseSections { Id = 14, Title = "Handling Fetch Errors", TextSource = "Gestionarea erorilor la obtinerea datelor.", VideoSource = "https://www.youtube.com/watch?v=Jm6r3nr4ujA", CourseId = 1 },
                new CourseSections { Id = 15, Title = "Making a Custom Hook", TextSource = "Crearea unui hook personalizat in React.", VideoSource = "https://www.youtube.com/watch?v=mDYOg_yerxk", CourseId = 1 },
                new CourseSections { Id = 16, Title = "Reusing the Custom Hook", TextSource = "Reutilizarea hook-urilor personalizate.", VideoSource = "https://www.youtube.com/watch?v=Rpm3aTpEKIY", CourseId = 1 },
                new CourseSections { Id = 17, Title = "The React Router", TextSource = "Utilizarea React Router pentru navigare.", VideoSource = "https://www.youtube.com/watch?v=Law7wfdg_ls", CourseId = 1 },
                new CourseSections { Id = 18, Title = "Route Parameters", TextSource = "Parametrii rutelor in React Router.", VideoSource = "https://www.youtube.com/watch?v=7q0SCZPtKmE", CourseId = 1 },
                new CourseSections { Id = 19, Title = "Link Component", TextSource = "Utilizarea componentei Link din React Router.", VideoSource = "https://www.youtube.com/watch?v=gg6kYL5KBX4", CourseId = 1 },
                new CourseSections { Id = 20, Title = "useHistory Hook", TextSource = "Utilizarea hook-ului useHistory pentru navigare programatica.", VideoSource = "https://www.youtube.com/watch?v=Jh1y-YiXMeE", CourseId = 1 },
                new CourseSections { Id = 21, Title = "Programmatic Redirects", TextSource = "Redirectionari programatice in React.", VideoSource = "https://www.youtube.com/watch?v=IxrDUWDnQaA", CourseId = 1 },
                new CourseSections { Id = 22, Title = "404 Pages & Next Steps", TextSource = "Crearea paginilor 404 si pasii urmatori.", VideoSource = "https://www.youtube.com/watch?v=nL6XMMBsp2E", CourseId = 1 }
            );
        }
    }
}