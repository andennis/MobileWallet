namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("pm.PassBeacon", "PassContentTemplateId", "pm.PassContentTemplate");
            DropForeignKey("pm.PassLocation", "PassContentTemplateId", "pm.PassContentTemplate");
            DropPrimaryKey("pm.PassContentTemplate");
            AlterColumn("pm.PassContentTemplate", "PassContentTemplateId", c => c.Int(nullable: false));
            AddPrimaryKey("pm.PassContentTemplate", "PassContentTemplateId");
            CreateIndex("pm.PassContentTemplate", "PassContentTemplateId");
            AddForeignKey("pm.PassContentTemplate", "PassContentTemplateId", "pm.PassProject", "PassProjectId");
            AddForeignKey("pm.PassBeacon", "PassContentTemplateId", "pm.PassContentTemplate", "PassContentTemplateId", cascadeDelete: true);
            AddForeignKey("pm.PassLocation", "PassContentTemplateId", "pm.PassContentTemplate", "PassContentTemplateId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassLocation", "PassContentTemplateId", "pm.PassContentTemplate");
            DropForeignKey("pm.PassBeacon", "PassContentTemplateId", "pm.PassContentTemplate");
            DropForeignKey("pm.PassContentTemplate", "PassContentTemplateId", "pm.PassProject");
            DropIndex("pm.PassContentTemplate", new[] { "PassContentTemplateId" });
            DropPrimaryKey("pm.PassContentTemplate");
            AlterColumn("pm.PassContentTemplate", "PassContentTemplateId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("pm.PassContentTemplate", "PassContentTemplateId");
            AddForeignKey("pm.PassLocation", "PassContentTemplateId", "pm.PassContentTemplate", "PassContentTemplateId", cascadeDelete: true);
            AddForeignKey("pm.PassBeacon", "PassContentTemplateId", "pm.PassContentTemplate", "PassContentTemplateId", cascadeDelete: true);
        }
    }
}
