using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projetoWebApi.Migrations
{
    /// <inheritdoc />
    public partial class PopulaTabelasCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(Nome, Imagem) Values('Bebidas','bebidas.jpg')");
            mb.Sql("Insert into Categorias(Nome, Imagem) Values('Lanches','lanches.jpg')");
            mb.Sql("Insert into Categorias(Nome, Imagem) Values('Sobremesas','sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
        }
    }
}
