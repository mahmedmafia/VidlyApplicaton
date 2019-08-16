namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewRental : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateRented = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        Customer_id = c.Int(nullable: false),
                        Movie_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_id, cascadeDelete: true)
                .Index(t => t.Customer_id)
                .Index(t => t.Movie_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "Movie_id", "dbo.Movies");
            DropForeignKey("dbo.Rentals", "Customer_id", "dbo.Customers");
            DropIndex("dbo.Rentals", new[] { "Movie_id" });
            DropIndex("dbo.Rentals", new[] { "Customer_id" });
            DropTable("dbo.Rentals");
        }
    }
}
