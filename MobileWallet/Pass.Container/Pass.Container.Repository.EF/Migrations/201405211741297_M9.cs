namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pscn.ClientDevice", "DeviceId", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("pscn.ClientDeviceApple", "PushToken", c => c.String(nullable: false, maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("pscn.ClientDeviceApple", "PushToken", c => c.String(nullable: false, maxLength: 512));
            AlterColumn("pscn.ClientDevice", "DeviceId", c => c.String(nullable: false, maxLength: 512));
        }
    }
}
