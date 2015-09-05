namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M19 : DbMigration
    {
        public override void Up()
        {
            DropColumn("pscn.PassField", "DefaultValue");
            DropColumn("pscn.PassField", "DefaultLabel");
        }
        
        public override void Down()
        {
            AddColumn("pscn.PassField", "DefaultLabel", c => c.String(maxLength: 512));
            AddColumn("pscn.PassField", "DefaultValue", c => c.String(maxLength: 512));
        }
    }
}
