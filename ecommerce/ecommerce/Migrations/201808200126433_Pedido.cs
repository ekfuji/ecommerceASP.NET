namespace ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pedido : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        PedidoId = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PedidoId);
            
            CreateTable(
                "dbo.PedidoItemVenda",
                c => new
                    {
                        Pedido_PedidoId = c.Int(nullable: false),
                        ItemVenda_ItemVendaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pedido_PedidoId, t.ItemVenda_ItemVendaId })
                .ForeignKey("dbo.Pedido", t => t.Pedido_PedidoId, cascadeDelete: true)
                .ForeignKey("dbo.ItemVenda", t => t.ItemVenda_ItemVendaId, cascadeDelete: true)
                .Index(t => t.Pedido_PedidoId)
                .Index(t => t.ItemVenda_ItemVendaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PedidoItemVenda", "ItemVenda_ItemVendaId", "dbo.ItemVenda");
            DropForeignKey("dbo.PedidoItemVenda", "Pedido_PedidoId", "dbo.Pedido");
            DropIndex("dbo.PedidoItemVenda", new[] { "ItemVenda_ItemVendaId" });
            DropIndex("dbo.PedidoItemVenda", new[] { "Pedido_PedidoId" });
            DropTable("dbo.PedidoItemVenda");
            DropTable("dbo.Pedido");
        }
    }
}
