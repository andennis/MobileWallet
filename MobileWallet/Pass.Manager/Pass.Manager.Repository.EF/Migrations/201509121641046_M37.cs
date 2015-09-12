namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M37 : DbMigration
    {
        public override void Up()
        {
            DropColumn("pm.PassContentTemplateField", "Name");
        }
        
        public override void Down()
        {
            AddColumn("pm.PassContentTemplateField", "Name", c => c.String(maxLength: 512));
        }
    }
}
