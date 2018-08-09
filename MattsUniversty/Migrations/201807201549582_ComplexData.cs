namespace MattsUniversty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComplexData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        InstructorID = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentID)
                .ForeignKey("dbo.Instructor", t => t.InstructorID)
                .Index(t => t.InstructorID);
            
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                    {
                        InstructorID = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.InstructorID)
                .ForeignKey("dbo.Instructor", t => t.InstructorID)
                .Index(t => t.InstructorID);

            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        InstructorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.InstructorID })
                .ForeignKey("dbo.Course", t => t.InstructorID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.InstructorID);


           Sql("INSERT INTO dbo.Department(Name, Budget,StartDate)Values ('Temp',0.00, GETDATE())");
            AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false, defaultValue: 1));
            //AddColumn("dbo.Course", "DepartmentID", c => c.Int(nullable: false));
            AddColumn("dbo.Course", "Instructors_ID", c => c.Int());
            AlterColumn("dbo.Course", "Title", c => c.String(maxLength: 50));
            AlterColumn("dbo.Course", "Title", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Student", "FirstName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Course", "DepartmentID");
            CreateIndex("dbo.Course", "Instructors_ID");
            AddForeignKey("dbo.Course", "Instructors_ID", "dbo.Instructor", "ID");
            AddForeignKey("dbo.Course", "DepartmentID", "dbo.Department", "DepartmentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "InstructorID", "dbo.Instructor");
            DropForeignKey("dbo.OfficeAssignment", "InstructorID", "dbo.Instructor");
            DropForeignKey("dbo.Course", "Instructors_ID", "dbo.Instructor");
            DropIndex("dbo.OfficeAssignment", new[] { "InstructorID" });
            DropIndex("dbo.Department", new[] { "InstructorID" });
            DropIndex("dbo.Course", new[] { "Instructors_ID" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            AlterColumn("dbo.Student", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Student", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Course", "Title", c => c.String());
            DropColumn("dbo.Course", "Instructors_ID");
            DropColumn("dbo.Course", "DepartmentID");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.Instructor");
            DropTable("dbo.Department");
        }
    }
}
