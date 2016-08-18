namespace ChatModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProfileModels",
                c => new
                {
                    UserID = c.Guid(nullable: false),
                    Nick = c.String(nullable: false),
                    Password = c.String(nullable: false),
                    Image = c.Binary(nullable: false),
                    LoggedIn = c.Boolean(nullable: false),
                    Message = c.String(),
                })
                .PrimaryKey(t => t.UserID);
        }

        public override void Down()
        {
            DropTable("dbo.ProfileModels");
        }
    }
}
