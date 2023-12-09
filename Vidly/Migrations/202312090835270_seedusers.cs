namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedusers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6b873a54-3944-4593-adca-38abbd413192', N'guest@domain.com', 0, N'AM9eDt/AbBYR2HHRdVjb9q6b3/uaSppS2CeV+SwV0mX7CRsPCgC/Q3Lc0MIynuQV6Q==', N'd4035ac3-5c97-4e87-a26e-434676b55859', NULL, 0, 0, NULL, 1, 0, N'guest@domain.com')
                  INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'95e7ca5b-56c5-46b9-97af-253122bccd3c', N'admin@vidly.com', 0, N'AHl8qVBv7exKWCOdbNcLubeup8vtpOjXFBzsP0dhE7z1Cyb6KjdpCK8mgyNVYDg+fg==', N'61b5c687-e2af-4b7d-818c-0466410aeadf', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4e6925c3-36d6-43f2-81d6-e72534f095b6', N'CanManageMovies') 

                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'95e7ca5b-56c5-46b9-97af-253122bccd3c',N'4e6925c3-36d6-43f2-81d6-e72534f095b6')
               ");
        }
        
        public override void Down()
        {
        }
    }
}
