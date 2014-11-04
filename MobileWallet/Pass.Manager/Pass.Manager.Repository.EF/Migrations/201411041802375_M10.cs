namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pm.PassContentTemplateField", "AttributedValue", c => c.String(maxLength: 128));
            AlterColumn("pm.PassContentTemplateField", "ChangeMessage", c => c.String(maxLength: 128));
            AlterColumn("pm.PassContentTemplateField", "Label", c => c.String(maxLength: 128));
            AlterColumn("pm.PassContentTemplateField", "TextAlignment", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("pm.PassContentTemplateField", "TextAlignment", c => c.Int(nullable: false));
            AlterColumn("pm.PassContentTemplateField", "Label", c => c.String());
            AlterColumn("pm.PassContentTemplateField", "ChangeMessage", c => c.String());
            AlterColumn("pm.PassContentTemplateField", "AttributedValue", c => c.String());
        }
    }
}
