namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seeddatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes (Id, Name , SignUpFee, DurationInMonths, DiscountRate) VALUES (1,'Pay as You Go', 0, 0, 0)");
            Sql("INSERT INTO MembershipTypes (Id, Name , SignUpFee, DurationInMonths, DiscountRate) VALUES (2,'Monthly', 30, 1, 10)");
            Sql("INSERT INTO MembershipTypes (Id, Name , SignUpFee, DurationInMonths, DiscountRate) VALUES (3,'Quarterly', 90, 3, 15)");
            Sql("INSERT INTO MembershipTypes (Id, Name , SignUpFee, DurationInMonths, DiscountRate) VALUES (4,'Annual', 300, 12, 20)");

            Sql("set Identity_Insert Genres On");
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Action')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Thriller')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Family')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Romance')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Comedy')");
            Sql("set Identity_Insert Genres Off");
        }
        
        public override void Down()
        {
        }
    }
}
