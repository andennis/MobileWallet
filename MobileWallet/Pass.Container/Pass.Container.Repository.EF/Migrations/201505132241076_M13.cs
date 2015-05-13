namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pscn.Registration", "UnregisterDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("pscn.Registration", "UnregisterDate");
        }
    }
}
