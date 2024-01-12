using Microsoft.EntityFrameworkCore;
using pimi_connect_app.Data.Entities;

namespace pimi_connect_app.Data.AppDbContext;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<AuthEntity> AuthEntities { get; set; }
    public DbSet<UserChatEntity> UserChats { get; set; }
    public DbSet<AttachmentEntity> Attachments { get; set; }
    public DbSet<ChatEntity> Chats { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CreateUsers(modelBuilder);
        CreateAuthEntities(modelBuilder);
        CreateUserChats(modelBuilder);
        CreateAttachments(modelBuilder);
        CreateChats(modelBuilder);
        CreateMessages(modelBuilder);
    }

    private static void CreateMessages(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MessageEntity>(ef =>
        {
            ef.Property(u => u.Id)
                .ValueGeneratedOnAdd();
        });
    }

    private static void CreateChats(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatEntity>(ef =>
        {
            ef.Property(u => u.Id)
                .ValueGeneratedOnAdd();
        });
    }

    private static void CreateUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(ef =>
        {
            ef.Property(u => u.Id)
                .ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<UserEntity>()
            .Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(50);
        
        modelBuilder.Entity<UserEntity>()
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
    
    private static void CreateAuthEntities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(ef =>
        {
            ef.Property(u => u.Id)
                .ValueGeneratedOnAdd();
        });
    }
    
    private static void CreateUserChats(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserChatEntity>(ef =>
        {
            ef.HasNoKey();
        });
    }
    
    private static void CreateAttachments(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttachmentEntity>(ef =>
        {
            ef.Property(u => u.Id)
                .ValueGeneratedOnAdd();
        });
    }
}
