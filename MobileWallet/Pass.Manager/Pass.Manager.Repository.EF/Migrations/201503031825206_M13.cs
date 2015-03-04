namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M13 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("pm.PassContentTemplate", "SuppressStripShine", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("pm.PassContentTemplate", "SuppressStripShine", c => c.Boolean());
        }
    }
}
