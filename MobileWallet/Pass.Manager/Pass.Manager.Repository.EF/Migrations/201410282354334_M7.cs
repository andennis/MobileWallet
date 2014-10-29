namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("pm.PassBeacon", "PassContentId");
            DropColumn("pm.PassLocation", "PassContentId");
        }
        
        public override void Down()
        {
            AddColumn("pm.PassLocation", "PassContentId", c => c.Int(nullable: false));
            AddColumn("pm.PassBeacon", "PassContentId", c => c.Int(nullable: false));
        }
    }
}
