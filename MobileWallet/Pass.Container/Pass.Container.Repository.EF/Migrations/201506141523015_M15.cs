namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M15 : DbMigration
    {
        public override void Up()
        {
            DropColumn("pscn.Registration", "UnregisterDate");
        }
        
        public override void Down()
        {
            AddColumn("pscn.Registration", "UnregisterDate", c => c.DateTime());
        }
    }
}
