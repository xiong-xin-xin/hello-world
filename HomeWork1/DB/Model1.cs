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

            //不会创建数据库，生产环境建议用这个设置，表通过sql来创建或修改
            Database.SetInitializer<Model1>(null);

            //// 数据库不存在时重新创建数据库,会删除主项目或其他插件的表
            //Database.SetInitializer<Model1>(new CreateDatabaseIfNotExists<Model1>());


            ////每次启动应用程序时创建数据库,会删除主项目或其他插件的表
            //Database.SetInitializer<Model1>(new DropCreateDatabaseAlways<Model1>());

            ////模型更改时重新创建数据库,会删除主项目或其他插件的表
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


            //更改表别名or别字段名
            //modelBuilder.Entity<User>().ToTable("NewUser").Property(e => e.Account).HasColumnName("NewAccount")
            //   .IsUnicode(false);


        }
    }
}
