namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M32 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pm.User", "FirstName", c => c.String(maxLength: 512));
            AlterColumn("pm.User", "LastName", c => c.String(maxLength: 512));
            AlterColumn("pm.User", "Password", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            AlterColumn("pm.User", "Password", c => c.String());
            AlterColumn("pm.User", "LastName", c => c.String());
            AlterColumn("pm.User", "FirstName", c => c.String());
        }
    }
}
