namespace SistemaLanchonete;

using System.Collections.Generic;

public class Lanche : ItemCardapio
{
    public List<string> IngredientesExtras { get; private set; } = new List<string>();
    public List<decimal> PrecosExtras { get; private set; } = new List<decimal>();

    public Lanche(int codigo, string descricao, decimal precoBase)
        : base(codigo, descricao, precoBase)
    {
    }

    public void AdicionarExtra(string ingrediente, decimal preco)
    {
        IngredientesExtras.Add(ingrediente);
        PrecosExtras.Add(preco);
    }

    public override decimal CalcularPrecoTotal()
    {
        decimal totalExtras = 0;
        foreach (var preco in PrecosExtras)
        {
            totalExtras += preco;
        }
        return PrecoBase + totalExtras;
    }
}