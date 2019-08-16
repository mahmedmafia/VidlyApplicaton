namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatemembershipmodel : DbMigration
    {
        public override void Up()
        {
            Sql("Update MemberShipTypes set MemberShipTypeName='Pay As You Go' where id=1");
            Sql("Update MemberShipTypes set MemberShipTypeName='Monthly' where id=2");
            Sql("Update MemberShipTypes set MemberShipTypeName='Quarter' where id=3");
            Sql("Update MemberShipTypes set MemberShipTypeName='year' where id=4");

        }

        public override void Down()
        {
        }
    }
}
