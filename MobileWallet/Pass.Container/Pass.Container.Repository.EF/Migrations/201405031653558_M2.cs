namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pscn.PassField", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("pscn.PassField", "Status");
        }
    }
}
