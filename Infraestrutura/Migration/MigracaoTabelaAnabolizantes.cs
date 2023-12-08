using FluentMigrator;

namespace Infraestrutura.Migracoes
{
    [Migration(20231120104200)]
    public class MigracaoTabelaAnabolizantes : Migration
    {
        public override void Up()
        {
            Create.Table("Anabolizantes")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Nome").AsString().NotNullable()
                .WithColumn("Preco").AsDouble().NotNullable()
                .WithColumn("Composicao").AsString().NotNullable()
                .WithColumn("Vencimento").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Anabolizantes");
        }
    }
}
