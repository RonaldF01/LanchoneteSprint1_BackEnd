namespace SistemaLanchonete;

public abstract class ItemCardapio
{
    public int Codigo { get; set; }
    public string Descricao { get; set; }
    public decimal PrecoBase { get; set; }

    public ItemCardapio(int codigo, string descricao, decimal precoBase)
    {
        Codigo = codigo;
        Descricao = descricao;
        PrecoBase = precoBase;
    }

    public abstract decimal CalcularPrecoTotal();
}