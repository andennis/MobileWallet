namespace Pass.Manager.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "pm.PassProjectField",
                c => new
                    {
                        PassProjectFieldId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 512),
                        DefaultValue = c.String(),
                        DefaultLabel = c.String(),
                        PassProjectId = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassProjectFieldId)
                .ForeignKey("pm.PassProject", t => t.PassProjectId)
                .Index(t => t.PassProjectId);
            
            CreateTable(
                "pm.PassContentTemplate",
                c => new
                    {
                        PassContentTemplateId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        OrganizationName = c.String(nullable: false, maxLength: 512),
                        PassStyle = c.Int(nullable: false),
                        MaxDistance = c.Int(),
                        RelevantDate = c.DateTime(),
                        GroupingIdentifier = c.String(),
                        LogoText = c.String(),
                        SuppressStripShine = c.Boolean(),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassContentTemplateId);
            
            CreateTable(
                "pm.PassBeacon",
                c => new
                    {
                        PassBeaconId = c.Int(nullable: false, identity: true),
                        PassContentId = c.Int(nullable: false),
                        ProximityUuid = c.String(nullable: false, maxLength: 128),
                        RelevantText = c.String(),
                        PassContentTemplateId = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassBeaconId)
                .ForeignKey("pm.PassContentTemplate", t => t.PassContentTemplateId, cascadeDelete: true)
                .Index(t => t.PassContentTemplateId);
            
            CreateTable(
                "pm.PassLocation",
                c => new
                    {
                        PassLocationId = c.Int(nullable: false, identity: true),
                        PassContentId = c.Int(nullable: false),
                        Altitude = c.Double(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        RelevantText = c.String(),
                        PassContentTemplateId = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PassLocationId)
                .ForeignKey("pm.PassContentTemplate", t => t.PassContentTemplateId, cascadeDelete: true)
                .Index(t => t.PassContentTemplateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("pm.PassLocation", "PassContentTemplateId", "pm.PassContentTemplate");
            DropForeignKey("pm.PassBeacon", "PassContentTemplateId", "pm.PassContentTemplate");
            DropForeignKey("pm.PassProjectField", "PassProjectId", "pm.PassProject");
            DropIndex("pm.PassLocation", new[] { "PassContentTemplateId" });
            DropIndex("pm.PassBeacon", new[] { "PassContentTemplateId" });
            DropIndex("pm.PassProjectField", new[] { "PassProjectId" });
            DropTable("pm.PassLocation");
            DropTable("pm.PassBeacon");
            DropTable("pm.PassContentTemplate");
            DropTable("pm.PassProjectField");
        }
    }
}
