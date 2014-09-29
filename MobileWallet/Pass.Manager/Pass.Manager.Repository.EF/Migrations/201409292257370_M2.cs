namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pm.PassProject", "PassContentId", c => c.Int());
            AlterColumn("pm.PassProject", "PassTemplateId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("pm.PassProject", "PassTemplateId", c => c.Int(nullable: false));
            AlterColumn("pm.PassProject", "PassContentId", c => c.Int(nullable: false));
        }
    }
}
