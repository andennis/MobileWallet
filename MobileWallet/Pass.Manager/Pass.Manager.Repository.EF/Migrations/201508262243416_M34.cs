namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M34 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassContentTemplate", "BackgroundColor", c => c.Int());
            AddColumn("pm.PassContentTemplate", "ForegroundColor", c => c.Int());
            AddColumn("pm.PassContentTemplate", "LabelColor", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("pm.PassContentTemplate", "LabelColor");
            DropColumn("pm.PassContentTemplate", "ForegroundColor");
            DropColumn("pm.PassContentTemplate", "BackgroundColor");
        }
    }
}
