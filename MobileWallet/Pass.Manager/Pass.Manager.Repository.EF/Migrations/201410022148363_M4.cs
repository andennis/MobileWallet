namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M4 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("pm.PassSiteCertificate");
            DropPrimaryKey("pm.PassSiteUser");
            AddColumn("pm.PassSiteCertificate", "PassSiteCertificateId", c => c.Int(nullable: false, identity: true));
            AddColumn("pm.PassSiteUser", "PassSiteUserId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("pm.PassSiteCertificate", "PassSiteCertificateId");
            AddPrimaryKey("pm.PassSiteUser", "PassSiteUserId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("pm.PassSiteUser");
            DropPrimaryKey("pm.PassSiteCertificate");
            DropColumn("pm.PassSiteUser", "PassSiteUserId");
            DropColumn("pm.PassSiteCertificate", "PassSiteCertificateId");
            AddPrimaryKey("pm.PassSiteUser", new[] { "PassSiteId", "UserId" });
            AddPrimaryKey("pm.PassSiteCertificate", new[] { "PassSiteId", "PassCertificateId" });
        }
    }
}
