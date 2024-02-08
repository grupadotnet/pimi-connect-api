using Microsoft.EntityFrameworkCore;
using pimi_connect_app.Data.Entities;

namespace pimi_connect_app.Data.AppDbContext;

public class AppDbContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<AuthEntity> Auths { get; set; }
    public DbSet<AttachmentEntity> Attachments { get; set; }
    public DbSet<UserChatEntity> UserChats { get; set; }
    public DbSet<ChatEntity> Chats { get; set; }
    public DbSet<MessageEntity> Messages { get; set; }
    public DbSet<ChatPasswordEntity> ChatPasswords { get; set; }
    public DbSet<EmailEntity> Emails { get; set; }
    public DbSet<PasswordContainerEntity> PasswordContainers { get; set; }
    public DbSet<UserKeyEntity> UserKeys { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CreateUsers(modelBuilder);
        CreateAuths(modelBuilder);
        CreateAttachments(modelBuilder);
        CreateUserChats(modelBuilder);
        CreateChats(modelBuilder);
        CreateMessages(modelBuilder);
        CreateChatPasswords(modelBuilder);
        CreateEmails(modelBuilder);
        CreatePasswordContainers(modelBuilder);
        CreateUserKeys(modelBuilder);
        CreateAttachments(modelBuilder);
    }

    private static void CreateMessages(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MessageEntity>(ef =>
        {
            ef.Property(u => u.Id)
                .ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<MessageEntity>()
            .Property(u => u.Content);

        modelBuilder.Entity<MessageEntity>()
            .Property(u => u.CreatedDate);
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
    
    private static void CreateAuths(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthEntity>(ef =>
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
    private static void CreateChatPasswords(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatPasswordEntity>(ef =>
        {
            ef.HasNoKey();
        });
    }
    private static void CreateEmails(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailEntity>(ef =>
        {
            ef.Property(u => u.Id)
                .ValueGeneratedOnAdd();
        });
    }
    private static void CreatePasswordContainers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PasswordContainerEntity>(ef =>
        {
            ef.Property(u => u.Id)
                .ValueGeneratedOnAdd();
        });
    }
    private static void CreateUserKeys(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserKeyEntity>(ef =>
        {
            ef.Property(u => u.Id)
                .ValueGeneratedOnAdd();
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
