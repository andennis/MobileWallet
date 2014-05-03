namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("pscn.PassTemplateApple", "CertificateStorageItemId");
        }
        
        public override void Down()
        {
            AddColumn("pscn.PassTemplateApple", "CertificateStorageItemId", c => c.Int(nullable: false));
        }
    }
}
