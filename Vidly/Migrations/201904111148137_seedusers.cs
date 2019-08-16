namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedusers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4fe0acd1-2684-4d70-a5d5-52e1180d1b15', N'guest@vidly.com', 0, N'AEEHVfs3TI5O+8UHA0HfQm/Ie9THlAEXhgXHLUzTg9lVEjzjtAa4ij1zFh6mm/vXbA==', N'697cc95c-dfa0-4a97-9b72-a99e4f6eb950', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd3d27898-25f7-4315-88d7-5698e238717a', N'admin@vidly.com', 0, N'AECvMbrCC2lRjzz0Tr8iv0bxXS0VXYt0XCfvIF2/E+0Zwj1JR9tGhROtSb6g7q+kHg==', N'9e9f0d7c-7526-4440-b4f7-73a8a75cbdac', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')


INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'bad2a8cc-ffc7-4c39-af72-77330c4b2ef9', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd3d27898-25f7-4315-88d7-5698e238717a', N'bad2a8cc-ffc7-4c39-af72-77330c4b2ef9')

");
        }
        
        public override void Down()
        {
        }
    }
}
