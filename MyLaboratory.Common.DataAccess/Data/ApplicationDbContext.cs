using Microsoft.EntityFrameworkCore;
using MyLaboratory.Common.DataAccess.Models;

namespace MyLaboratory.Common.DataAccess.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        #region (For Migration) DbSet<T>'s T is table type.
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Expenditure> Expenditures { get; set; }
        public virtual DbSet<FixedExpenditure> FixedExpenditures { get; set; }
        public virtual DbSet<FixedIncome> FixedIncomes { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        #endregion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PRIMARY");

                entity.ToTable("Account");

                entity.HasComment("MyLaboratory.WebSite ����")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Email).HasComment("���� �̸��� (ID)");

                entity.Property(e => e.AgreedServiceTerms).HasComment("��� ���� ����");

                entity.Property(e => e.AvatarImagePath)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'/upload/Management/Profile/default-avatar.jpg'")
                    .HasComment("���� �ƹ�Ÿ �̹��� ���");

                entity.Property(e => e.Created)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("'1900-01-01 00:00:00.000000'")
                    .HasComment("���� ������");

                entity.Property(e => e.Deleted).HasComment("���� ���� ����");

                entity.Property(e => e.EmailConfirmed).HasComment("�̸��� Ȯ�� ����");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("���� ����");

                entity.Property(e => e.HashedPassword)
                    .IsRequired()
                    .HasComment("���� ��ȣȭ �� ��й�ȣ");

                entity.Property(e => e.Locked).HasComment("���� ���");

                entity.Property(e => e.LoginAttempt)
                    .HasColumnType("int(11)")
                    .HasComment("�α��� �õ� Ƚ��");

                entity.Property(e => e.Message).HasComment("���� ���� �޽���");

                entity.Property(e => e.RegistrationToken).HasComment("ȸ������ ���� ��ū");

                entity.Property(e => e.ResetPasswordToken).HasComment("��й�ȣ ã�� ���� ��ū");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'User'")
                    .HasComment("���� ���� (Admin �Ǵ� User)");

                entity.Property(e => e.Updated)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("'1900-01-01 00:00:00.000000'")
                    .HasComment("���� ������Ʈ��");
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.HasKey(e => new { e.ProductName, e.AccountEmail })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("Asset");

                entity.HasComment("MyLaboratory.WebSite �ڻ�")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.AccountEmail, "Asset_FK");

                entity.Property(e => e.ProductName).HasComment("��ǰ�� (���� ���¸�, ���� ���¸�, ���� ��)");

                entity.Property(e => e.AccountEmail).HasComment("���� �̸��� (ID)");

                entity.Property(e => e.Amount)
                    .HasColumnType("bigint(255)")
                    .HasComment("�ݾ�");

                entity.Property(e => e.Created)
                    .HasMaxLength(6)
                    .HasComment("������");

                entity.Property(e => e.Deleted).HasComment("��������");

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("�׸� (��������� �ڻ�, ��Ź �ڻ�, ���� �ڻ�, ���༺ �ڻ�, ���ڼ� �ڻ�, �ε���, ����, ��Ÿ �ǹ� �ڻ�, ���� �ڻ�)");

                entity.Property(e => e.MonetaryUnit)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasComment("ȭ�� ���� (KRW, USD, ETC)");

                entity.Property(e => e.Note)
                    .HasMaxLength(45)
                    .HasComment("���");

                entity.Property(e => e.Updated)
                    .HasMaxLength(6)
                    .HasComment("������Ʈ��");

                entity.HasOne(d => d.AccountEmailNavigation)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.AccountEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Asset_FK");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasComment("ī�װ� /*MyLaboratory.WebSite�� �α��� �� ���� ������ ���� SideBar ���� �� ���*/");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasComment("ID");

                entity.Property(e => e.Action)
                    .HasMaxLength(256)
                    .HasComment("���� MVC Action �� /*�� ���� ������ ���� ī�װ� ����*/");

                entity.Property(e => e.Controller)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("���� MVC Controller ��");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("ǥ���̸�");

                entity.Property(e => e.IconPath)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("ǥ�� ������ ��� /*FontAwesome ���*/");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("�̸�");

                entity.Property(e => e.Order)
                    .HasColumnType("int(11)")
                    .HasComment("��� ����");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasDefaultValueSql("'Admin'")
                    .HasComment("���� ���� ����");
            });

            modelBuilder.Entity<Expenditure>(entity =>
            {
                entity.ToTable("Expenditure");

                entity.HasComment("MyLaboratory.WebSite ����")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => new { e.PaymentMethod, e.AccountEmail }, "Expenditure_FK");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasComment("PK");

                entity.Property(e => e.AccountEmail)
                    .IsRequired()
                    .HasComment("���� �̸��� (ID)");

                entity.Property(e => e.Amount)
                    .HasColumnType("bigint(255)")
                    .HasComment("�ݾ�");

                entity.Property(e => e.Contents)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("���� (A��Ʈ/Bī��/C������/D������)");

                entity.Property(e => e.Created)
                    .HasMaxLength(6)
                    .HasComment("������");

                entity.Property(e => e.MainClass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("��з� (��������/��Һ�����/�Һ�����)");

                entity.Property(e => e.MyDepositAsset)
                    .HasMaxLength(255)
                    .HasComment("�� �Ա� �ڻ� (�ڻ� ��ǰ��/����) (���� �� [������, ���ڻ���ü, ����, ��������, ��ä��ȯ]�� �� ���)");

                entity.Property(e => e.Note)
                    .HasMaxLength(45)
                    .HasComment("���");

                entity.Property(e => e.PaymentMethod)
                    .IsRequired()
                    .HasComment("���� ���� (�ڻ� ��ǰ��/����)");

                entity.Property(e => e.SubClass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("�Һз� (������/���ڻ���ü/���� | ��������/��ä��ȯ/����/��ȸ����/������ ��������/�񿵸���ü ���� | (�ĺ�/�ܽĺ�)/(�ְ�/��ǰ��)/������/�Ƿ��/�����/��ź�/(����/��ȭ)/(�Ƿ�/�Ź�)/�뵷/���强����/��Ÿ����/���ľ�����)");

                entity.Property(e => e.Updated)
                    .HasMaxLength(6)
                    .HasComment("������Ʈ��");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.Expenditures)
                    .HasForeignKey(d => new { d.PaymentMethod, d.AccountEmail })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Expenditure_FK");
            });

            modelBuilder.Entity<FixedExpenditure>(entity =>
            {
                entity.ToTable("FixedExpenditure");

                entity.HasComment("MyLaboratory.WebSite ��������")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => new { e.PaymentMethod, e.AccountEmail }, "FixedExpenditure_FK");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasComment("PK");

                entity.Property(e => e.AccountEmail)
                    .IsRequired()
                    .HasComment("���� �̸��� (ID)");

                entity.Property(e => e.Amount)
                    .HasColumnType("bigint(255)")
                    .HasComment("�ݾ�");

                entity.Property(e => e.Contents)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("���� (A��Ʈ/Bī��/C������/D������)");

                entity.Property(e => e.Created)
                    .HasMaxLength(6)
                    .HasComment("������");

                entity.Property(e => e.DepositDay)
                    .HasColumnType("tinyint(2) unsigned")
                    .HasComment("�Ա���");

                entity.Property(e => e.DepositMonth)
                    .HasColumnType("tinyint(2) unsigned")
                    .HasComment("�Աݿ�");

                entity.Property(e => e.MainClass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("��з� (��������/��Һ�����/�Һ�����)");

                entity.Property(e => e.MaturityDate)
                    .HasMaxLength(6)
                    .HasComment("������");

                entity.Property(e => e.MyDepositAsset)
                    .HasMaxLength(255)
                    .HasComment("�� �Ա� �ڻ� (�ڻ� ��ǰ��/����) (���� �� [������, ���ڻ���ü, ����, ��������, ��ä��ȯ]�� �� ���)");

                entity.Property(e => e.Note)
                    .HasMaxLength(45)
                    .HasComment("���");

                entity.Property(e => e.PaymentMethod)
                    .IsRequired()
                    .HasComment("���� ���� (�ڻ� ��ǰ��/����)");

                entity.Property(e => e.SubClass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("�Һз� (������/���ڻ���ü/���� | ��������/��ä��ȯ/����/��ȸ����/������ ��������/�񿵸���ü ���� | (�ĺ�/�ܽĺ�)/(�ְ�/��ǰ��)/������/�Ƿ��/�����/��ź�/(����/��ȭ)/(�Ƿ�/�Ź�)/�뵷/���强����/��Ÿ����/���ľ�����)");

                entity.Property(e => e.Updated)
                    .HasMaxLength(6)
                    .HasComment("������Ʈ��");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.FixedExpenditures)
                    .HasForeignKey(d => new { d.PaymentMethod, d.AccountEmail })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FixedExpenditure_FK");
            });

            modelBuilder.Entity<FixedIncome>(entity =>
            {
                entity.ToTable("FixedIncome");

                entity.HasComment("MyLaboratory.WebSite ��������")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => new { e.DepositMyAssetProductName, e.AccountEmail }, "FixedIncome_FK");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasComment("PK");

                entity.Property(e => e.AccountEmail)
                    .IsRequired()
                    .HasComment("���� �̸��� (ID)");

                entity.Property(e => e.Amount)
                    .HasColumnType("bigint(255)")
                    .HasComment("�ݾ�");

                entity.Property(e => e.Contents)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("���� (ȸ���/�����)");

                entity.Property(e => e.Created)
                    .HasMaxLength(6)
                    .HasComment("������");

                entity.Property(e => e.DepositDay)
                    .HasColumnType("tinyint(2) unsigned")
                    .HasComment("�Ա���");

                entity.Property(e => e.DepositMonth)
                    .HasColumnType("tinyint(2) unsigned")
                    .HasComment("�Աݿ�");

                entity.Property(e => e.DepositMyAssetProductName)
                    .IsRequired()
                    .HasComment("�Ա� �ڻ� (�ڻ� ��ǰ��/����)");

                entity.Property(e => e.MainClass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("��з� (�������/���������)");

                entity.Property(e => e.MaturityDate)
                    .HasMaxLength(6)
                    .HasComment("������");

                entity.Property(e => e.Note)
                    .HasMaxLength(45)
                    .HasComment("���");

                entity.Property(e => e.SubClass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("�Һз� (�ٷμ���/�������/���ݼ���/�����ҵ�/�Ӵ����/��Ÿ����)");

                entity.Property(e => e.Updated)
                    .HasMaxLength(6)
                    .HasComment("������Ʈ��");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.FixedIncomes)
                    .HasForeignKey(d => new { d.DepositMyAssetProductName, d.AccountEmail })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FixedIncome_FK");
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.ToTable("Income");

                entity.HasComment("MyLaboratory.WebSite ����")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => new { e.DepositMyAssetProductName, e.AccountEmail }, "Income_FK");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasComment("PK");

                entity.Property(e => e.AccountEmail)
                    .IsRequired()
                    .HasComment("���� �̸��� (ID)");

                entity.Property(e => e.Amount)
                    .HasColumnType("bigint(255)")
                    .HasComment("�ݾ�");

                entity.Property(e => e.Contents)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("���� (ȸ���/�����)");

                entity.Property(e => e.Created)
                    .HasMaxLength(6)
                    .HasComment("������");

                entity.Property(e => e.DepositMyAssetProductName)
                    .IsRequired()
                    .HasComment("�Ա� �ڻ� (�ڻ� ��ǰ��/����)");

                entity.Property(e => e.MainClass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("��з� (�������/���������)");

                entity.Property(e => e.Note)
                    .HasMaxLength(45)
                    .HasComment("���");

                entity.Property(e => e.SubClass)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("�Һз� (�ٷμ���/�������/���ݼ���/�����ҵ�/�Ӵ����/��Ÿ����)");

                entity.Property(e => e.Updated)
                    .HasMaxLength(6)
                    .HasComment("������Ʈ��");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.Incomes)
                    .HasForeignKey(d => new { d.DepositMyAssetProductName, d.AccountEmail })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Income_FK");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("SubCategory");

                entity.HasComment("���� ī�װ� /*MyLaboratory.WebSite�� �α��� �� ���� ������ ���� SideBar ���� �� ���*/");

                entity.HasIndex(e => e.CategoryId, "SubCategory_FK");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasComment("ID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("���� MVC Action �� /*�� ���� ������ ���� ī�װ� ����*/");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("int(11)")
                    .HasComment("�θ� ī�װ� ID");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("ǥ���̸�");

                entity.Property(e => e.IconPath)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("ǥ�� ������ ��� /*FontAwesome ���*/");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasComment("�̸�");

                entity.Property(e => e.Order)
                    .HasColumnType("int(11)")
                    .HasComment("��� ����");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasDefaultValueSql("'Admin'")
                    .HasComment("���� ���� ����");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("SubCategory_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}