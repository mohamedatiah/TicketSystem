namespace FutureWorkshopTicketSystem.Infrastructure.Data
{
    public class FutureWorkshopTicketSystemDbContext : DbContext
    {
        public FutureWorkshopTicketSystemDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketNote> TicketNotes { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
