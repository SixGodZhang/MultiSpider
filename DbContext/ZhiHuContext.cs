namespace Spider.MultiDbContext
{
    using Repos;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ZhiHuContext : DbContext
    {
        //方法一：这种用法一般用在没有数据库或数据库模型有大的改动，需要删除旧的，按新模型重建时可以使用。
        // 一旦数据库初始化成功，就可以注释该段语句，或使用Database.SetInitializer<ZhiHuContext>(null);
        static ZhiHuContext()
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<ZhiHuContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ZhiHuContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<ZhiHuContext>());
            //Database.SetInitializer<ZhiHuContext>(null);
        }

        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“ZhiHuContext”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“GitHubToMail.DbContext.ZhiHuContext”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“ZhiHuContext”
        //连接字符串。
        public ZhiHuContext()
            : base("name=ZhiHu")
        {

        }

        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。

        public virtual DbSet<HotRepo> HotEntities { get; set; }
        // public virtual DbSet<MyEntity> MyEntities { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}