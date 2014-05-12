namespace Pass.Container.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pscn.PassTemplateApple", "PassTypeId", c => c.String(nullable: false, maxLength: 128, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("pscn.PassTemplateApple", "PassTypeId", c => c.String(nullable: false, maxLength: 512));
        }
    }
}
