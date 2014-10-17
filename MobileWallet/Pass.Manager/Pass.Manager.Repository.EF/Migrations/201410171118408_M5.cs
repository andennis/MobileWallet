namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassCertificate", "CertificateFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("pm.PassCertificate", "CertificateFileName");
        }
    }
}
