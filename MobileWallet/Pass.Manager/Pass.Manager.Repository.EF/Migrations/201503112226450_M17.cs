namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pm.PassImage",
                c => new
                    {
                        PassImageId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        ImageType = c.Int(nullable: false),
                        FileStorageId = c.Int(),
                        FileStorage2xId = c.Int(),
                        PassContentTemplateId = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassImageId)
                .ForeignKey("pm.PassContentTemplate", t => t.PassContentTemplateId, cascadeDelete: true)
                .Index(t => t.PassContentTemplateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassImage", "PassContentTemplateId", "pm.PassContentTemplate");
            DropIndex("pm.PassImage", new[] { "PassContentTemplateId" });
            DropTable("pm.PassImage");
        }
    }
}
