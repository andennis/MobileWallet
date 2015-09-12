namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M35 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("pm.PassContentTemplateField", "PassProjectFieldId", "pm.PassProjectField");
            DropIndex("pm.PassContentTemplateField", new[] { "PassProjectFieldId" });
            AlterColumn("pm.PassContentTemplateField", "PassProjectFieldId", c => c.Int());
            CreateIndex("pm.PassContentTemplateField", "PassProjectFieldId");
            AddForeignKey("pm.PassContentTemplateField", "PassProjectFieldId", "pm.PassProjectField", "PassProjectFieldId");
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassContentTemplateField", "PassProjectFieldId", "pm.PassProjectField");
            DropIndex("pm.PassContentTemplateField", new[] { "PassProjectFieldId" });
            AlterColumn("pm.PassContentTemplateField", "PassProjectFieldId", c => c.Int(nullable: false));
            CreateIndex("pm.PassContentTemplateField", "PassProjectFieldId");
            AddForeignKey("pm.PassContentTemplateField", "PassProjectFieldId", "pm.PassProjectField", "PassProjectFieldId", cascadeDelete: true);
        }
    }
}
