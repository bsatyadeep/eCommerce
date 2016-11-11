namespace eCommerce.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatdDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CostPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Vouchers", "VoucherCode", c => c.String(maxLength: 10));
            CreateIndex("dbo.BasketVouchers", "BasketId");
            AddForeignKey("dbo.BasketVouchers", "BasketId", "dbo.Baskets", "BasketId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketVouchers", "BasketId", "dbo.Baskets");
            DropIndex("dbo.BasketVouchers", new[] { "BasketId" });
            AlterColumn("dbo.Vouchers", "VoucherCode", c => c.String());
            DropColumn("dbo.Products", "CostPrice");
        }
    }
}
