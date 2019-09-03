using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ItersTutoriov1.Models
{
    public partial class ITERSTutoriov10Context : DbContext
    {
        public ITERSTutoriov10Context()
        {
        }

        public ITERSTutoriov10Context(DbContextOptions<ITERSTutoriov10Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TbCourseAssignments> TbCourseAssignments { get; set; }
        public virtual DbSet<TbCourseSubscriptions> TbCourseSubscriptions { get; set; }
        public virtual DbSet<TbCourses> TbCourses { get; set; }
        public virtual DbSet<TbSubscriptionStatuses> TbSubscriptionStatuses { get; set; }
        public virtual DbSet<TbSubscriptionTokens> TbSubscriptionTokens { get; set; }
        public virtual DbSet<TbSubscriptions> TbSubscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<TbCourseAssignments>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.InstructorId })
                    .HasName("PK_CourseAssignments");

                entity.ToTable("tb_course_assignments");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");
            });

            modelBuilder.Entity<TbCourseSubscriptions>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.CourseId })
                    .HasName("PK__tb_cours__5E57FC831BC23552");

                entity.ToTable("tb_course_subscriptions");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbCourses>(entity =>
            {
                entity.HasKey(e => e.CourseId)
                    .HasName("PK__tb_cours__C92D71A700A9450E");

                entity.ToTable("tb_courses");

                entity.Property(e => e.CourseBrief)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CourseCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CourseDetails).IsRequired();

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CourseTags)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CoverImage).HasMaxLength(512);

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.KnowledgePoints)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SubTitle).HasMaxLength(255);

                entity.Property(e => e.TargetAudiences)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TbSubscriptionStatuses>(entity =>
            {
                entity.HasKey(e => e.SubscriptionStatusId)
                    .HasName("PK_tb_cms_subscription_statuses");

                entity.ToTable("tb_subscription_statuses");

                entity.Property(e => e.SubscriptionStatusId).ValueGeneratedNever();

                entity.Property(e => e.SubscriptionStatusName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TbSubscriptionTokens>(entity =>
            {
                entity.HasKey(e => e.AuthToken)
                    .HasName("PK_tb_cms_subscription_tokens");

                entity.ToTable("tb_subscription_tokens");

                entity.Property(e => e.AuthToken)
                    .HasMaxLength(255)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ExpiresOn).HasColumnType("datetime");

                entity.Property(e => e.Id).HasMaxLength(255);

                entity.Property(e => e.IssuedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbSubscriptions>(entity =>
            {
                entity.HasKey(e => e.UniqueId)
                    .HasName("PK_tb_cms_subscriptions");

                entity.ToTable("tb_subscriptions");

                entity.Property(e => e.UniqueId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActivationKey)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Birthdate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(255);

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FaxNumber).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.GroupId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Id).HasMaxLength(255);

                entity.Property(e => e.IpAddress)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.MobileNumber).HasMaxLength(255);

                entity.Property(e => e.Notes).HasMaxLength(1000);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PasswordResetKey).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(255);

                entity.Property(e => e.PostCode).HasMaxLength(255);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Salutation).HasMaxLength(50);

                entity.Property(e => e.State).HasMaxLength(255);

                entity.Property(e => e.UpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.WeChatId).HasMaxLength(255);

                entity.HasOne(d => d.SubscriptionStatus)
                    .WithMany(p => p.TbSubscriptions)
                    .HasForeignKey(d => d.SubscriptionStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tb_cms_users_tb_cms_user_status");
            });
        }
    }
}
