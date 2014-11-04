namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pm.PassContentTemplateField",
                c => new
                    {
                        PassContentTemplateFieldId = c.Int(nullable: false, identity: true),
                        FieldKind = c.Int(nullable: false),
                        AttributedValue = c.String(),
                        ChangeMessage = c.String(),
                        Label = c.String(),
                        TextAlignment = c.Int(nullable: false),
                        PassProjectFieldId = c.Int(nullable: false),
                        PassContentTemplateId = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassContentTemplateFieldId)
                .ForeignKey("pm.PassContentTemplate", t => t.PassContentTemplateId, cascadeDelete: true)
                .ForeignKey("pm.PassProjectField", t => t.PassProjectFieldId, cascadeDelete: true)
                .Index(t => t.PassProjectFieldId)
                .Index(t => t.PassContentTemplateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassContentTemplateField", "PassProjectFieldId", "pm.PassProjectField");
            DropForeignKey("pm.PassContentTemplateField", "PassContentTemplateId", "pm.PassContentTemplate");
            DropIndex("pm.PassContentTemplateField", new[] { "PassContentTemplateId" });
            DropIndex("pm.PassContentTemplateField", new[] { "PassProjectFieldId" });
            DropTable("pm.PassContentTemplateField");
        }
    }
}
