namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pscn.PassField", "IsPassField", c => c.Boolean(nullable: false));
            AddColumn("pscn.PassField", "IsDistributionField", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("pscn.PassField", "IsDistributionField");
            DropColumn("pscn.PassField", "IsPassField");
        }
    }
}
