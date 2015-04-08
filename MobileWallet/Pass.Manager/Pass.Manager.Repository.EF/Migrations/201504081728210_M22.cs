namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M22 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassContentTemplate", "IsDefault", c => c.Boolean(nullable: false, defaultValue: false));
            DropColumn("pm.PassProject", "PassTemplateId");
        }
        
        public override void Down()
        {
            AddColumn("pm.PassProject", "PassTemplateId", c => c.Int());
            DropColumn("pm.PassContentTemplate", "IsDefault");
        }
    }
}
