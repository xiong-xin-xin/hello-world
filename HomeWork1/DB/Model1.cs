namespace HomeWork1.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1Context")
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //���ᴴ�����ݿ⣬��������������������ã���ͨ��sql���������޸�
            Database.SetInitializer<Model1>(null);

            //// ���ݿⲻ����ʱ���´������ݿ�,��ɾ������Ŀ����������ı�
            //Database.SetInitializer<Model1>(new CreateDatabaseIfNotExists<Model1>());


            ////ÿ������Ӧ�ó���ʱ�������ݿ�,��ɾ������Ŀ����������ı�
            //Database.SetInitializer<Model1>(new DropCreateDatabaseAlways<Model1>());

            ////ģ�͸���ʱ���´������ݿ�,��ɾ������Ŀ����������ı�
            //Database.SetInitializer<Model1>(new DropCreateDatabaseIfModelChanges<Model1>());


            modelBuilder.Entity<User>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Mobile)
                .IsUnicode(false);


            //���ı����or���ֶ���
            //modelBuilder.Entity<User>().ToTable("NewUser").Property(e => e.Account).HasColumnName("NewAccount")
            //   .IsUnicode(false);


        }
    }
}
