namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pscn.ClientDevice", "DeviceType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("pscn.ClientDevice", "DeviceType");
        }
    }
}
