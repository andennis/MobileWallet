namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("pm.PassContentField", "PassContentId", c => c.Int(nullable: false));
            CreateIndex("pm.PassContentField", "PassContentId");
            AddForeignKey("pm.PassContentField", "PassContentId", "pm.PassContent", "PassContentId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassContentField", "PassContentId", "pm.PassContent");
            DropIndex("pm.PassContentField", new[] { "PassContentId" });
            DropColumn("pm.PassContentField", "PassContentId");
        }
    }
}
