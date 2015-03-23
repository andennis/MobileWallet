namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M18 : DbMigration
    {
        public override void Up()
        {
            DropColumn("pm.PassImage", "Name");
        }
        
        public override void Down()
        {
            AddColumn("pm.PassImage", "Name", c => c.String(nullable: false, maxLength: 512));
        }
    }
}
