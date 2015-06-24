namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pscn.PassNative",
                c => new
                    {
                        PassNativeId = c.Int(nullable: false, identity: true),
                        PassId = c.Int(nullable: false),
                        PassFileStorageId = c.Int(),
                        DeviceType = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassNativeId)
                .ForeignKey("pscn.Pass", t => t.PassId, cascadeDelete: true)
                .Index(t => t.PassId);
            
            DropColumn("pscn.Pass", "PassFileStorageId");
        }
        
        public override void Down()
        {
            AddColumn("pscn.Pass", "PassFileStorageId", c => c.Int());
            DropForeignKey("pscn.PassNative", "PassId", "pscn.Pass");
            DropIndex("pscn.PassNative", new[] { "PassId" });
            DropTable("pscn.PassNative");
        }
    }
}
