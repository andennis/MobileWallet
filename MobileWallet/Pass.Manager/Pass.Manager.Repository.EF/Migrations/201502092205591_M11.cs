namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M11 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("pm.PassContentTemplate", "PassContentTemplateId", "pm.PassProject");
            DropForeignKey("pm.PassBeacon", "PassContentTemplateId", "pm.PassContentTemplate");
            DropForeignKey("pm.PassLocation", "PassContentTemplateId", "pm.PassContentTemplate");
            DropForeignKey("pm.PassContentTemplateField", "PassContentTemplateId", "pm.PassContentTemplate");
            DropIndex("pm.PassContentTemplate", new[] { "PassContentTemplateId" });
            DropPrimaryKey("pm.PassContentTemplate");
            AddColumn("pm.PassContentTemplate", "PassProjectId", c => c.Int(nullable: false));
            AlterColumn("pm.PassContentTemplate", "PassContentTemplateId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("pm.PassContentTemplate", "PassContentTemplateId");
            CreateIndex("pm.PassContentTemplate", "PassProjectId");
            AddForeignKey("pm.PassContentTemplate", "PassProjectId", "pm.PassProject", "PassProjectId");
            AddForeignKey("pm.PassBeacon", "PassContentTemplateId", "pm.PassContentTemplate", "PassContentTemplateId", cascadeDelete: true);
            AddForeignKey("pm.PassLocation", "PassContentTemplateId", "pm.PassContentTemplate", "PassContentTemplateId", cascadeDelete: true);
            AddForeignKey("pm.PassContentTemplateField", "PassContentTemplateId", "pm.PassContentTemplate", "PassContentTemplateId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassContentTemplateField", "PassContentTemplateId", "pm.PassContentTemplate");
            DropForeignKey("pm.PassLocation", "PassContentTemplateId", "pm.PassContentTemplate");
            DropForeignKey("pm.PassBeacon", "PassContentTemplateId", "pm.PassContentTemplate");
            DropForeignKey("pm.PassContentTemplate", "PassProjectId", "pm.PassProject");
            DropIndex("pm.PassContentTemplate", new[] { "PassProjectId" });
            DropPrimaryKey("pm.PassContentTemplate");
            AlterColumn("pm.PassContentTemplate", "PassContentTemplateId", c => c.Int(nullable: false));
            DropColumn("pm.PassContentTemplate", "PassProjectId");
            AddPrimaryKey("pm.PassContentTemplate", "PassContentTemplateId");
            CreateIndex("pm.PassContentTemplate", "PassContentTemplateId");
            AddForeignKey("pm.PassContentTemplateField", "PassContentTemplateId", "pm.PassContentTemplate", "PassContentTemplateId", cascadeDelete: true);
            AddForeignKey("pm.PassLocation", "PassContentTemplateId", "pm.PassContentTemplate", "PassContentTemplateId", cascadeDelete: true);
            AddForeignKey("pm.PassBeacon", "PassContentTemplateId", "pm.PassContentTemplate", "PassContentTemplateId", cascadeDelete: true);
            AddForeignKey("pm.PassContentTemplate", "PassContentTemplateId", "pm.PassProject", "PassProjectId");
        }
    }
}
