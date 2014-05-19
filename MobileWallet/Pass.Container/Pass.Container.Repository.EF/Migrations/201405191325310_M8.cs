namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pscn.PassTemplateNative", "DeviceType", c => c.Int(nullable: false));
            DropColumn("pscn.PassTemplateNative", "ClientType");
        }
        
        public override void Down()
        {
            AddColumn("pscn.PassTemplateNative", "ClientType", c => c.Int(nullable: false));
            DropColumn("pscn.PassTemplateNative", "DeviceType");
        }
    }
}
