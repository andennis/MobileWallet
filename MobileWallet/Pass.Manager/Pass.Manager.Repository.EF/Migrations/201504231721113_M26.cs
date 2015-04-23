namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassBeacon", "Name", c => c.String(nullable: false, maxLength: 512));
            AddColumn("pm.PassBeacon", "Description", c => c.String());
            AddColumn("pm.PassLocation", "Name", c => c.String(nullable: false, maxLength: 512));
            AddColumn("pm.PassLocation", "Description", c => c.String());
            AddColumn("pm.PassProjectField", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("pm.PassProjectField", "Description");
            DropColumn("pm.PassLocation", "Description");
            DropColumn("pm.PassLocation", "Name");
            DropColumn("pm.PassBeacon", "Description");
            DropColumn("pm.PassBeacon", "Name");
        }
    }
}
