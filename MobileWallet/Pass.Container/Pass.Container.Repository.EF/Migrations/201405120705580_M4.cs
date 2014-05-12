namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pscn.Pass", "AuthToken", c => c.String(nullable: false, maxLength: 64, unicode: false));
            AlterColumn("pscn.Pass", "SerialNumber", c => c.String(nullable: false, maxLength: 64, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("pscn.Pass", "SerialNumber", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("pscn.Pass", "AuthToken", c => c.String(nullable: false, maxLength: 512));
        }
    }
}
