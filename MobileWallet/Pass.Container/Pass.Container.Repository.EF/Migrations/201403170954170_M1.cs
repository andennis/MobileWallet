namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pscn.PassField", "DefaultValue", c => c.String(maxLength: 400));
            AddColumn("pscn.PassField", "DefaultLabel", c => c.String(maxLength: 400));
            AddColumn("pscn.PassField", "Format", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("pscn.PassField", "Format");
            DropColumn("pscn.PassField", "DefaultLabel");
            DropColumn("pscn.PassField", "DefaultValue");
        }
    }
}
