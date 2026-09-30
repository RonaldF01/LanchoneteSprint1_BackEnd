namespace SistemaLanchonete;

public class Bebida : ItemCardapio
{
    public string Tamanho { get; set; }

    public Bebida(int codigo, string descricao, decimal precoBase, string tamanho)
        : base(codigo, descricao, precoBase)
    {
        Tamanho = tamanho;
    }

    public override decimal CalcularPrecoTotal()
    {
        decimal multiplicador = Tamanho.Trim().ToLower() switch
        {
            "300ml" => 1.0m,
            "500ml" => 1.25m,
            "1l" => 1.6m,
            _ => 1.0m
        };

        return PrecoBase * multiplicador;
    }
}