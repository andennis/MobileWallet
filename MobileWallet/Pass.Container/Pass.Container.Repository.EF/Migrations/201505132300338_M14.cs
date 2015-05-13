namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M14 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pscn.Registration", "UnregisterDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("pscn.Registration", "UnregisterDate", c => c.DateTime(nullable: false));
        }
    }
}
