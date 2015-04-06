namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("pm.PassProject", "PassProjectId", "pm.PassCertificate");
            DropForeignKey("pm.PassProjectField", "PassProjectId", "pm.PassProject");
            DropForeignKey("pm.PassContentTemplate", "PassProjectId", "pm.PassProject");
            DropIndex("pm.PassProject", new[] { "PassProjectId" });
            DropPrimaryKey("pm.PassProject");
            AlterColumn("pm.PassProject", "PassProjectId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("pm.PassProject", "PassProjectId");
            AddForeignKey("pm.PassProjectField", "PassProjectId", "pm.PassProject", "PassProjectId");
            AddForeignKey("pm.PassContentTemplate", "PassProjectId", "pm.PassProject", "PassProjectId");
            DropColumn("pm.PassProject", "PassCertificateId");
        }
        
        public override void Down()
        {
            AddColumn("pm.PassProject", "PassCertificateId", c => c.Int(nullable: false));
            DropForeignKey("pm.PassContentTemplate", "PassProjectId", "pm.PassProject");
            DropForeignKey("pm.PassProjectField", "PassProjectId", "pm.PassProject");
            DropPrimaryKey("pm.PassProject");
            AlterColumn("pm.PassProject", "PassProjectId", c => c.Int(nullable: false));
            AddPrimaryKey("pm.PassProject", "PassProjectId");
            CreateIndex("pm.PassProject", "PassProjectId");
            AddForeignKey("pm.PassContentTemplate", "PassProjectId", "pm.PassProject", "PassProjectId");
            AddForeignKey("pm.PassProjectField", "PassProjectId", "pm.PassProject", "PassProjectId");
            AddForeignKey("pm.PassProject", "PassProjectId", "pm.PassCertificate", "PassCertificateId");
        }
    }
}
