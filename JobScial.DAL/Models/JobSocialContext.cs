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

    public virtual DbSet<Follow> Follows { get; set; }

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
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionStringDB"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Account__A9D10535370AE998");

            entity.ToTable("Account");

            entity.Property(e => e.Email).HasMaxLength(400);
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DoB).HasColumnType("date");
            entity.Property(e => e.FullName).HasMaxLength(400);
            entity.Property(e => e.FullNameSearch).HasMaxLength(400);
            entity.Property(e => e.Image).HasMaxLength(400);
            entity.Property(e => e.Password).HasMaxLength(400);
        });

        modelBuilder.Entity<AccountCertificate>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.Index }).HasName("PK__AccountC__AD3813A4D48F1EAC");

            entity.ToTable("AccountCertificate");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Link).HasMaxLength(400);
        });

        modelBuilder.Entity<AccountEducation>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.SchoolId, e.YearStart }).HasName("PK__AccountE__EF7961B2CF4F6AEF");

            entity.ToTable("AccountEducation");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.SchoolId).HasColumnName("SchoolID");
            entity.Property(e => e.Description).HasMaxLength(400);

            entity.HasOne(d => d.School).WithMany(p => p.AccountEducations)
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountEd__Schoo__73BA3083");
        });

        modelBuilder.Entity<AccountExperience>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.CompanyId, e.YearStart }).HasName("PK__AccountE__9E7A560163C6061B");

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
                .HasConstraintName("FK__AccountEx__Times__628FA481");
        });

        modelBuilder.Entity<AccountSkill>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.SkillCategoryId }).HasName("PK__AccountS__E9B7FA0D8D3C4C59");

            entity.ToTable("AccountSkill");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.SkillCategoryId).HasColumnName("SkillCategoryID");
            entity.Property(e => e.Description).HasMaxLength(400);

            entity.HasOne(d => d.SkillCategory).WithMany(p => p.AccountSkills)
                .HasForeignKey(d => d.SkillCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountSk__Skill__70DDC3D8");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFAAE54F1317");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId)
                .HasColumnName("CommentID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Content).HasMaxLength(400);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PostId).HasColumnName("PostID");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__Comment__PostID__6477ECF3");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__Company__2D971C4CF6699785");

            entity.ToTable("Company");

            entity.Property(e => e.CompanyId)
                .HasColumnName("CompanyID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.HasKey(e => new { e.FollowId, e.FollowType, e.AccountId }).HasName("PK__Follow__FE8988E371C12206");

            entity.ToTable("Follow");

            entity.Property(e => e.FollowId).HasColumnName("FollowID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.FollowedOn).HasColumnType("datetime");

            entity.HasOne(d => d.FollowNavigation).WithMany(p => p.Follows)
                .HasForeignKey(d => d.FollowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Follow__FollowID__6A30C649");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Group__149AF30AC1A61D7A");

            entity.ToTable("Group");

            entity.Property(e => e.GroupId)
                .HasColumnName("GroupID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.GroupName).HasMaxLength(400);

            entity.HasOne(d => d.Company).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__Group__CompanyID__5CD6CB2B");
        });

        modelBuilder.Entity<GroupMember>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.AccountId }).HasName("PK__GroupMem__67D32952C92E25F9");

            entity.ToTable("GroupMember");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.GroupRoleId).HasColumnName("GroupRoleID");
            entity.Property(e => e.JoinedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GroupMemb__Group__5EBF139D");

            entity.HasOne(d => d.GroupRole).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupRoleId)
                .HasConstraintName("FK__GroupMemb__Group__5FB337D6");
        });

        modelBuilder.Entity<GroupRole>(entity =>
        {
            entity.HasKey(e => e.GroupRoleId).HasName("PK__GroupRol__599A507444F2265B");

            entity.ToTable("GroupRole");

            entity.Property(e => e.GroupRoleId)
                .HasColumnName("GroupRoleID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__JobTitle__056690E220506AE9");

            entity.ToTable("JobTitle");

            entity.Property(e => e.JobId)
                .HasColumnName("JobID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.AccountId }).HasName("PK__Like__D95BBA6088C84A98");

            entity.ToTable("Like");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");

            entity.HasOne(d => d.Post).WithMany(p => p.Likes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Like__PostID__656C112C");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostID).HasName("PK__Post__AA126038473CBD71");

            entity.ToTable("Post");

            entity.Property(e => e.PostID)
                .HasColumnName("PostID");
            entity.Property(e => e.Content).HasMaxLength(400);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.JobId).HasColumnName("JobID");
            entity.Property(e => e.PostCategoryId).HasColumnName("PostCategoryID");

            entity.HasOne(d => d.Group).WithMany(p => p.Posts)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Post__GroupID__75A278F5");

            entity.HasOne(d => d.Job).WithMany(p => p.Posts)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK__Post__JobID__76969D2E");

            entity.HasOne(d => d.PostCategory).WithMany(p => p.Posts)
                .HasForeignKey(d => d.PostCategoryId)
                .HasConstraintName("FK__Post__PostCatego__5BE2A6F2");
        });

        modelBuilder.Entity<PostCategory>(entity =>
        {
            entity.HasKey(e => e.PostCategoryId).HasName("PK__PostCate__FE61E36957792497");

            entity.ToTable("PostCategory");

            entity.Property(e => e.PostCategoryId)
                .HasColumnName("PostCategoryID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<PostPhoto>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.Index }).HasName("PK__PostPhot__33B7D61A68B9139D");

            entity.ToTable("PostPhoto");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.Caption).HasMaxLength(400);
            entity.Property(e => e.Link).HasMaxLength(400);

            entity.HasOne(d => d.Post).WithMany(p => p.PostPhotos)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostPhoto__PostI__6EF57B66");
        });

        modelBuilder.Entity<PostSkill>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__PostSkil__AA1260384119F159");

            entity.ToTable("PostSkill");

            entity.Property(e => e.PostId)
                .HasColumnName("PostID");
            entity.Property(e => e.SkillCategoryId).HasColumnName("SkillCategoryID");

            entity.HasOne(d => d.Post).WithOne(p => p.PostSkill)
                .HasForeignKey<PostSkill>(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostSkill__PostI__6383C8BA");

            entity.HasOne(d => d.SkillCategory).WithMany(p => p.PostSkills)
                .HasForeignKey(d => d.SkillCategoryId)
                .HasConstraintName("FK__PostSkill__Skill__72C60C4A");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Report");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(400);
            entity.Property(e => e.ReportId).HasColumnName("ReportID");

            entity.HasOne(d => d.ReportNavigation).WithMany()
                .HasForeignKey(d => d.ReportId)
                .HasConstraintName("FK__Report__ReportID__6E01572D");

            entity.HasOne(d => d.Report1).WithMany()
                .HasForeignKey(d => d.ReportId)
                .HasConstraintName("FK__Report__ReportID__6D0D32F4");
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.SchoolId).HasName("PK__School__3DA4677B3BAA04AF");

            entity.ToTable("School");

            entity.Property(e => e.SchoolId)
                .HasColumnName("SchoolID");
            entity.Property(e => e.Description).HasMaxLength(400);
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<SkillCategory>(entity =>
        {
            entity.HasKey(e => e.SkillCategoryId).HasName("PK__SkillCat__D2A5F8BCB593B4BA");

            entity.ToTable("SkillCategory");

            entity.Property(e => e.SkillCategoryId)
                .HasColumnName("SkillCategoryID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        modelBuilder.Entity<TimespanUnit>(entity =>
        {
            entity.HasKey(e => e.TimespanUnitId).HasName("PK__Timespan__B62F7B862C761CDE");

            entity.ToTable("TimespanUnit");
            entity.ToTable("TimespanUnit");

            entity.Property(e => e.TimespanUnitId)
                .HasColumnName("TimespanUnitID");
            entity.Property(e => e.Name).HasMaxLength(400);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}