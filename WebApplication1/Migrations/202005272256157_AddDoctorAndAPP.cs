namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoctorAndAPP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppointmentModels",
                c => new
                    {
                        AppointmentID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        DoctorID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        TimeBlockHelper = c.String(),
                    })
                .PrimaryKey(t => t.AppointmentID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.DoctorModels", t => t.DoctorID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.DoctorModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        BirthDate = c.DateTime(nullable: false),
                        Sex = c.Int(nullable: false),
                        Department = c.Int(nullable: false),
                        Degree = c.Int(nullable: false),
                        DisableNewAppointments = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppointmentModels", "DoctorID", "dbo.DoctorModels");
            DropForeignKey("dbo.AppointmentModels", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.AppointmentModels", new[] { "DoctorID" });
            DropIndex("dbo.AppointmentModels", new[] { "UserID" });
            DropTable("dbo.DoctorModels");
            DropTable("dbo.AppointmentModels");
        }
    }
}
