namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pm.PassBarcode",
                c => new
                    {
                        PassContentTemplateId = c.Int(nullable: false),
                        AltText = c.String(),
                        Format = c.Int(nullable: false),
                        Message = c.String(nullable: false, maxLength: 64),
                        MessageEncoding = c.String(),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassContentTemplateId)
                .ForeignKey("pm.PassContentTemplate", t => t.PassContentTemplateId)
                .Index(t => t.PassContentTemplateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassBarcode", "PassContentTemplateId", "pm.PassContentTemplate");
            DropIndex("pm.PassBarcode", new[] { "PassContentTemplateId" });
            DropTable("pm.PassBarcode");
        }
    }
}
