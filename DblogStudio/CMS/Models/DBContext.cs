using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace CMS.Models
{
    public class DblogStudioDBContext : DbContext
    {
        public DblogStudioDBContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DblogStudioDBContext, Migrations.Configuration>("DefaultConnection"));
            //Database.SetInitializer<DblogStudioDBContext>(new DropCreateDatabaseIfModelChanges<DblogStudioDBContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Membership>()
                        .HasMany<Role>(r => r.Roles)
                        .WithMany(u => u.Members)
                        .Map(m =>
                        {
                            m.ToTable("webpages_UsersInRoles");
                            m.MapLeftKey("UserId");
                            m.MapRightKey("RoleId");
                        });
        }
        public DbSet<Membership> Membership { get; set; }
        public DbSet<OAuthMembership> OAuthMembership { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<ExtraUserProfile> ExtraUserProfile { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Notify> Notify { get; set; }
    }

    [Table("webpages_Membership")]
    public class Membership
    {
        public Membership()
        {
            Roles = new List<Role>();
            OAuthMemberships = new List<OAuthMembership>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        [StringLength(128)]
        public string ConfirmationToken { get; set; }
        public bool? IsConfirmed { get; set; }
        public DateTime? LastPasswordFailureDate { get; set; }
        public int PasswordFailuresSinceLastSuccess { get; set; }
        [Required, StringLength(128)]
        public string Password { get; set; }
        public DateTime? PasswordChangedDate { get; set; }
        [Required, StringLength(128)]
        public string PasswordSalt { get; set; }
        [StringLength(128)]
        public string PasswordVerificationToken { get; set; }
        public DateTime? PasswordVerificationTokenExpirationDate { get; set; }

        public ICollection<Role> Roles { get; set; }

        [ForeignKey("UserId")]
        public ICollection<OAuthMembership> OAuthMemberships { get; set; }
    }

    [Table("webpages_OAuthMembership")]
    public class OAuthMembership
    {
        [Key, Column(Order = 0), StringLength(30)]
        public string Provider { get; set; }

        [Key, Column(Order = 1), StringLength(100)]
        public string ProviderUserId { get; set; }

        public int UserId { get; set; }

        [Column("UserId"), InverseProperty("OAuthMemberships")]
        public Membership User { get; set; }
    }

    [Table("webpages_Roles")]
    public class Role
    {
        public Role()
        {
            Members = new List<Membership>();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        [StringLength(256)]
        public string RoleName { get; set; }

        public ICollection<Membership> Members { get; set; }
    }


    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    [Table("ExtraUserProfile")]
    public class ExtraUserProfile
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string BirthDay { get; set; }
        public string Department { get; set; }
        [Required]
        public int Role { get; set; }
        public string StudentId { get; set; }
    }

    [Table("Subject")]
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public virtual List<Article> Articles { get; set; }
        [NotMapped]
        public int TotalArticle { get; set; }
    }

    [Table("Article")]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublicDate { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ExtraUserProfile User { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
    }

    [Table("Notify")]
    public class Notify
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEnd { get; set; }
    }

    /*
     * Class ViewModel
     */

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
