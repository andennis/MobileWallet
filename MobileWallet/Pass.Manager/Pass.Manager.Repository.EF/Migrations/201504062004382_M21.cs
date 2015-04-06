namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassProject", "PassCertificateId", c => c.Int(nullable: false));
            CreateIndex("pm.PassProject", "PassCertificateId");
            AddForeignKey("pm.PassProject", "PassCertificateId", "pm.PassCertificate", "PassCertificateId");
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassProject", "PassCertificateId", "pm.PassCertificate");
            DropIndex("pm.PassProject", new[] { "PassCertificateId" });
            DropColumn("pm.PassProject", "PassCertificateId");
        }
    }
}
