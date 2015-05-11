namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pscn.ClientDevice", "Version", c => c.Int(nullable: false, defaultValue:1));
            AddColumn("pscn.ClientDevice", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("pscn.ClientDevice", "UpdatedDate", c => c.DateTime(nullable: false));
            AddColumn("pscn.Registration", "Version", c => c.Int(nullable: false, defaultValue:1));
            AddColumn("pscn.Registration", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("pscn.Registration", "UpdatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("pscn.Registration", "UpdatedDate");
            DropColumn("pscn.Registration", "CreatedDate");
            DropColumn("pscn.Registration", "Version");
            DropColumn("pscn.ClientDevice", "UpdatedDate");
            DropColumn("pscn.ClientDevice", "CreatedDate");
            DropColumn("pscn.ClientDevice", "Version");
        }
    }
}
