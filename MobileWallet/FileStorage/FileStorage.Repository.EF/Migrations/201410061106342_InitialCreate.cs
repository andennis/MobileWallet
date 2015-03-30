namespace FileStorage.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "fs.FolderItem",
                c => new
                    {
                        FolderItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.FolderItemId)
                .ForeignKey("fs.FolderItem", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "fs.StorageItem",
                c => new
                    {
                        StorageItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        OriginalName = c.String(maxLength: 512),
                        Size = c.Long(),
                        Status = c.Int(nullable: false),
                        ItemType = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StorageItemId)
                .ForeignKey("fs.FolderItem", t => t.ParentId, cascadeDelete: true)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("fs.FolderItem", "ParentId", "fs.FolderItem");
            DropForeignKey("fs.StorageItem", "ParentId", "fs.FolderItem");
            DropIndex("fs.StorageItem", new[] { "ParentId" });
            DropIndex("fs.FolderItem", new[] { "ParentId" });
            DropTable("fs.StorageItem");
            DropTable("fs.FolderItem");
        }
    }
}
