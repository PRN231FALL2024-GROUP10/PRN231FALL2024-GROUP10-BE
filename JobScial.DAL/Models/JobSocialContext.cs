using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JobScial.DAL.Models;
public partial class JobSocialContext : DbContext
{
    public JobSocialContext()
    {
    }

    public JobSocialContext(DbContextOptions<JobSocialContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountCertificate> AccountCertificates { get; set; }

    public virtual DbSet<AccountEducation> AccountEducations { get; set; }

    public virtual DbSet<AccountExperience> AccountExperiences { get; set; }

    public virtual DbSet<AccountSkill> AccountSkills { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Connection> Connections { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupMember> GroupMembers { get; set; }

    public virtual DbSet<GroupRole> GroupRoles { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostCategory> PostCategories { get; set; }

    public virtual DbSet<PostPhoto> PostPhotos { get; set; }

    public virtual DbSet<PostSkill> PostSkills { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<SkillCategory> SkillCategories { get; set; }

    public virtual DbSet<TimespanUnit> TimespanUnits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionStringDB")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Account__A9D105358BF3FD5C");

            entity.ToTable("Account");

            entity.Property(e => e.Email).HasMaxLength(400);
            entity.Property(e => e.AccountId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AccountID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DoB).HasColumnType("date");
            entity.Property(e => e.FullName).HasMaxLength(400);
            entity.Property(e => e.FullNameSearch).HasMaxLength(400);
            entity.Property(e => e.Image).HasMaxLength(400);
            entity.Property(e => e.Password).HasMaxLength(400);
        });

        modelBuilder.Entity<AccountCertificate>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.Index }).HasName("PK__AccountC__AD3813A4F9127600");

            entity.ToTable("AccountCertificate");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Link).HasMaxLength(400);
        });

        modelBuilder.Entity<AccountEducation>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.SchoolId, e.YearStart }).HasName("PK__AccountE__EF7961B2AA5CDD21");

            entity.ToTable("AccountEducation");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.Description).HasMaxLength(400);

            entity.HasOne(d => d.School).WithMany(p => p.AccountEducations)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountEd__Schoo__5BE2A6F2");
        });

        modelBuilder.Entity<AccountExperience>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.CompanyId, e.YearStart }).HasName("PK__AccountE__9E7A5601B409A6A9");

            entity.ToTable("AccountExperience");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.JobTitle).HasMaxLength(400);

            entity.HasOne(e => e.Company)
                    .WithMany() // Giả sử Company không có navigation property đến AccountExperience
                    .HasForeignKey(e => e.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(d => d.TimespanUnitNavigation).WithMany(p => p.AccountExperiences)
                .HasForeignKey(d => d.TimespanUnit)
                .HasConstraintName("FK__AccountEx__Times__5CD6CB2B");
        });

        modelBuilder.Entity<AccountSkill>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.SkillCategoryId }).HasName("PK__AccountS__E9B7FA0DD74EF72E");

            entity.ToTable("AccountSkill");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.SkillCategoryId).HasColumnName("SkillCategoryID");
            entity.Property(e => e.Description).HasMaxLength(400);

            entity.HasOne(d => d.SkillCategory).WithMany(p => p.AccountSkills)
                .HasForeignKey(d => d.SkillCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountSk__Skill__5DCAEF64");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFAAD0DE0C5A");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Content).HasMaxLength(400);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PostId).HasColumnName("PostID");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Comment__PostID__5EBF139D");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__2D971C4CC1FBF6E0");

            entity.ToTable("Company");

            entity.Property(e => e.CompanyId)
                .ValueGeneratedNever()
                .HasColumnName("CompanyID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity
                .HasKey(e => new {  e.AccountId, e.FollowedAccountId});
            entity
                .ToTable("Connection");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.FollowedAccountId).HasColumnName("FollowedAccountID");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Group__149AF30ADDC39D03");

            entity.ToTable("Group");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.GroupName).HasMaxLength(400);

            entity.HasOne(d => d.Company).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Group__CompanyID__5FB337D6");
        });

        modelBuilder.Entity<GroupMember>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.AccountId }).HasName("PK__GroupMem__67D3295229B1E649");

            entity.ToTable("GroupMember");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.GroupRoleId).HasColumnName("GroupRoleID");
            entity.Property(e => e.JoinedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupMemb__Group__60A75C0F");

            entity.HasOne(d => d.GroupRole).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupRoleId)
                .HasConstraintName("FK__GroupMemb__Group__619B8048");
        });

        modelBuilder.Entity<GroupRole>(entity =>
        {
            entity.HasKey(e => e.GroupRoleId).HasName("PK__GroupRol__599A507464590F79");

            entity.ToTable("GroupRole");

            entity.Property(e => e.GroupRoleId)
                .ValueGeneratedNever()
                .HasColumnName("GroupRoleID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__JobTitle__056690E24B5B4EE1");

            entity.ToTable("JobTitle");

            entity.Property(e => e.JobId)
                .ValueGeneratedNever()
                .HasColumnName("JobID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.AccountId }).HasName("PK__Like__D95BBA60B0E4C340");

            entity.ToTable("Like");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");

            entity.HasOne(d => d.Post).WithMany(p => p.Likes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Like__PostID__628FA481");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostID).HasName("PK__Post__AA12603864E1EC76");

            entity.ToTable("Post");

            entity.Property(e => e.PostID).HasColumnName("PostID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.JobId).HasColumnName("JobID");
            entity.Property(e => e.PostCategoryId).HasColumnName("PostCategoryID");

            entity.HasOne(d => d.Group).WithMany(p => p.Posts)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Post__GroupID__6383C8BA");

            entity.HasOne(d => d.Job).WithMany(p => p.Posts)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK__Post__JobID__6477ECF3");

            entity.HasOne(d => d.PostCategory).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PostCategoryId)
                .HasConstraintName("FK__Post__PostCatego__656C112C");
        });

        modelBuilder.Entity<PostCategory>(entity =>
        {
            entity.HasKey(e => e.PostCategoryId).HasName("PK__PostCate__FE61E369EBB5191E");

            entity.ToTable("PostCategory");

            entity.Property(e => e.PostCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("PostCategoryID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<PostPhoto>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.Index }).HasName("PK__PostPhot__33B7D61A2E19EF8B");

            entity.ToTable("PostPhoto");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.Caption).HasMaxLength(400);
            entity.Property(e => e.Link).HasMaxLength(400);

            entity.HasOne(d => d.Post).WithMany(p => p.PostPhotos)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostPhoto__PostI__66603565");
        });

        modelBuilder.Entity<PostSkill>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__PostSkil__AA126038DDF6C7CE");

            entity.ToTable("PostSkill");

            entity.Property(e => e.PostId)
                .ValueGeneratedNever()
                .HasColumnName("PostID");
            entity.Property(e => e.SkillCategoryId).HasColumnName("SkillCategoryID");
            entity.Property(e => e.SkillLevelRequirement).HasMaxLength(30);

            entity.HasOne(d => d.Post).WithOne(p => p.PostSkill)
                .HasForeignKey<PostSkill>(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostSkill__PostI__6754599E");

            entity.HasOne(d => d.SkillCategory).WithMany(p => p.PostSkills)
                .HasForeignKey(d => d.SkillCategoryId)
                .HasConstraintName("FK__PostSkill__Skill__68487DD7");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Report__D5BD48E5F42A5334");

            entity.ToTable("Report");

            entity.Property(e => e.ReportId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ReportID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(400);
            entity.Property(e => e.TargetId).HasColumnName("TargetID");
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.SchoolId).HasName("PK__School__3DA4677B165680D4");

            entity.ToTable("School");

            entity.Property(e => e.SchoolId)
                .ValueGeneratedNever()
                .HasColumnName("SchoolID");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<SkillCategory>(entity =>
        {
            entity.HasKey(e => e.SkillCategoryId).HasName("PK__SkillCat__D2A5F8BCE0622F61");

            entity.ToTable("SkillCategory");

            entity.Property(e => e.SkillCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("SkillCategoryID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<TimespanUnit>(entity =>
        {
            entity.HasKey(e => e.TimespanUnitId).HasName("PK__Timespan__B62F7B86B8DEA7C0");

            entity.ToTable("TimespanUnit");

            entity.Property(e => e.TimespanUnitId)
                .ValueGeneratedNever()
                .HasColumnName("TimespanUnitID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
