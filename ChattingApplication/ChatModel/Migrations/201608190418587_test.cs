namespace ChatModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProfileModels", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProfileModels", "Image", c => c.Binary(nullable: false));
        }
    }
}
