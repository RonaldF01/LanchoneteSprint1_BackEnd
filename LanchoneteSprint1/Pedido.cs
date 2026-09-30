namespace SistemaLanchonete;

using System;
using System.Collections.Generic;

public class Pedido
{
    private List<ItemCardapio> itens = new List<ItemCardapio>();

    public void AdicionarItem(ItemCardapio item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "Item inválido.");

        itens.Add(item);
    }

    public bool EstaVazio()
    {
        return itens.Count == 0;
    }

    public decimal CalcularTotalPedido()
    {
        decimal total = 0;
        foreach (var item in itens)
        {
            total += item.CalcularPrecoTotal();
        }
        return total;
    }

    public void ExibirResumoPedido()
    {
        if (itens.Count == 0)
        {
            return; 
        }

        Console.WriteLine("\n=========================================");
        Console.WriteLine("           RESUMO DO PEDIDO              ");
        Console.WriteLine("=========================================");

        foreach (var item in itens)
        {
            Console.WriteLine($"[{item.Codigo}] {item.Descricao} - Total: R$ {item.CalcularPrecoTotal():F2}");

            if (item is Lanche lanche && lanche.IngredientesExtras.Count > 0)
            {
                Console.WriteLine($"    Extras: {string.Join(", ", lanche.IngredientesExtras)}");
            }
            else if (item is Bebida bebida)
            {
                Console.WriteLine($"    Tamanho: {bebida.Tamanho}");
            }
        }

        Console.WriteLine("-----------------------------------------");
        Console.WriteLine($"TOTAL A PAGAR: R$ {CalcularTotalPedido():F2}");
        Console.WriteLine("=========================================\n");
    }
}