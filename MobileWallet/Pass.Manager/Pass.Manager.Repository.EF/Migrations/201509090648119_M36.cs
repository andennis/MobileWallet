namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M36 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassContentTemplateField", "Name", c => c.String(maxLength: 512));
            AddColumn("pm.PassContentTemplateField", "Value", c => c.String());
            AlterColumn("pm.PassProjectField", "DefaultLabel", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("pm.PassProjectField", "DefaultLabel", c => c.String());
            DropColumn("pm.PassContentTemplateField", "Value");
            DropColumn("pm.PassContentTemplateField", "Name");
        }
    }
}
