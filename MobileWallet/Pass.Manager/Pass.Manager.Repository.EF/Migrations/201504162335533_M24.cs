namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M24 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pm.PassContent",
                c => new
                    {
                        PassContentId = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        PassContentTemplateId = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassContentId)
                .ForeignKey("pm.PassContentTemplate", t => t.PassContentTemplateId)
                .Index(t => t.PassContentTemplateId);
            
            AddColumn("pm.PassContentTemplate", "Status", c => c.Int(nullable: false, defaultValue:1));
            AddColumn("pm.PassContentTemplate", "PassContainerTemplateId", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassContent", "PassContentTemplateId", "pm.PassContentTemplate");
            DropIndex("pm.PassContent", new[] { "PassContentTemplateId" });
            DropColumn("pm.PassContentTemplate", "PassContainerTemplateId");
            DropColumn("pm.PassContentTemplate", "Status");
            DropTable("pm.PassContent");
        }
    }
}
