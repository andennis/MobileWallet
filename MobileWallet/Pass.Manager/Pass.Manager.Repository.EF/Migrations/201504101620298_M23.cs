namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassBeacon", "Major", c => c.Int());
            AddColumn("pm.PassBeacon", "Minor", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("pm.PassBeacon", "Minor");
            DropColumn("pm.PassBeacon", "Major");
        }
    }
}
