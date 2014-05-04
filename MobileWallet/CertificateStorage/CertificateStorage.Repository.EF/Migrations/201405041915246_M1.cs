namespace CertificateStorage.Repository.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "cer.Certificate",
                c => new
                    {
                        CertificateId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 512),
                        Password = c.String(maxLength: 512),
                        FileId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CertificateId)
                .Index(t => t.Name, unique: true, name: "IX_Certificate_Name");
            
        }
        
        public override void Down()
        {
            DropIndex("cer.Certificate", "IX_Certificate_Name");
            DropTable("cer.Certificate");
        }
    }
}
