namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pscn.ClientDevice", "DeviceId", c => c.String(nullable: false, maxLength: 64, unicode: false));
            AlterColumn("pscn.ClientDeviceApple", "PushToken", c => c.String(nullable: false, maxLength: 64, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("pscn.ClientDeviceApple", "PushToken", c => c.String(nullable: false, maxLength: 64));
            AlterColumn("pscn.ClientDevice", "DeviceId", c => c.String(nullable: false, maxLength: 64));
        }
    }
}
