namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M19 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("pm.PassProjectField", "PassProjectId", "pm.PassProject");
            DropForeignKey("pm.PassContentTemplate", "PassProjectId", "pm.PassProject");
            DropPrimaryKey("pm.PassProject");
            AddColumn("pm.PassProject", "PassCertificateId", c => c.Int(nullable: false));
            AlterColumn("pm.PassProject", "PassProjectId", c => c.Int(nullable: false));
            AddPrimaryKey("pm.PassProject", "PassProjectId");
            CreateIndex("pm.PassProject", "PassProjectId");
            AddForeignKey("pm.PassProject", "PassProjectId", "pm.PassCertificate", "PassCertificateId");
            AddForeignKey("pm.PassProjectField", "PassProjectId", "pm.PassProject", "PassProjectId");
            AddForeignKey("pm.PassContentTemplate", "PassProjectId", "pm.PassProject", "PassProjectId");
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassContentTemplate", "PassProjectId", "pm.PassProject");
            DropForeignKey("pm.PassProjectField", "PassProjectId", "pm.PassProject");
            DropForeignKey("pm.PassProject", "PassProjectId", "pm.PassCertificate");
            DropIndex("pm.PassProject", new[] { "PassProjectId" });
            DropPrimaryKey("pm.PassProject");
            AlterColumn("pm.PassProject", "PassProjectId", c => c.Int(nullable: false, identity: true));
            DropColumn("pm.PassProject", "PassCertificateId");
            AddPrimaryKey("pm.PassProject", "PassProjectId");
            AddForeignKey("pm.PassContentTemplate", "PassProjectId", "pm.PassProject", "PassProjectId");
            AddForeignKey("pm.PassProjectField", "PassProjectId", "pm.PassProject", "PassProjectId");
        }
    }
}
