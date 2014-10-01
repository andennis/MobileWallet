namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassSiteUser", "UserState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("pm.PassSiteUser", "UserState");
        }
    }
}
