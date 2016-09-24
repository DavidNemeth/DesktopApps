namespace DchatDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatRooms",
                c => new
                    {
                        ChatRoomId = c.Guid(nullable: false, identity: true),
                        RoomName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ChatRoomId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        LoggedIn = c.Boolean(nullable: false),
                        Image = c.Binary(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserUsers",
                c => new
                    {
                        User_UserId = c.Guid(nullable: false),
                        User_UserId1 = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.User_UserId1 })
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .ForeignKey("dbo.Users", t => t.User_UserId1)
                .Index(t => t.User_UserId)
                .Index(t => t.User_UserId1);
            
            CreateTable(
                "dbo.UserChatRooms",
                c => new
                    {
                        User_UserId = c.Guid(nullable: false),
                        ChatRoom_ChatRoomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.ChatRoom_ChatRoomId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.ChatRooms", t => t.ChatRoom_ChatRoomId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.ChatRoom_ChatRoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserChatRooms", "ChatRoom_ChatRoomId", "dbo.ChatRooms");
            DropForeignKey("dbo.UserChatRooms", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_UserId1", "dbo.Users");
            DropForeignKey("dbo.UserUsers", "User_UserId", "dbo.Users");
            DropIndex("dbo.UserChatRooms", new[] { "ChatRoom_ChatRoomId" });
            DropIndex("dbo.UserChatRooms", new[] { "User_UserId" });
            DropIndex("dbo.UserUsers", new[] { "User_UserId1" });
            DropIndex("dbo.UserUsers", new[] { "User_UserId" });
            DropTable("dbo.UserChatRooms");
            DropTable("dbo.UserUsers");
            DropTable("dbo.Users");
            DropTable("dbo.ChatRooms");
        }
    }
}
