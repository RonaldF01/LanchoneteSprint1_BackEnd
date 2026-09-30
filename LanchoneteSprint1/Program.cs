using System;
using System.Collections.Generic;

namespace SistemaLanchonete;

class Program
{

    static List<Lanche> cardapioLanches = new List<Lanche>();
    static List<Bebida> cardapioBebidas = new List<Bebida>();
    static List<(int Codigo, string Nome, decimal Preco)> cardapioExtras = new List<(int, string, decimal)>();

    static void Main(string[] args)
    {
        InicializarCardapio();

        Pedido pedidoAtual = new Pedido();
        bool executando = true;
        int contadorCodigoItemPedido = 1;

        while (executando)
        {
            Console.WriteLine("===== MENU - SISTEMA DE LANCHONETE =====");
            Console.WriteLine("1 - Ver Cardápio e Adicionar Lanche");
            Console.WriteLine("2 - Ver Cardápio e Adicionar Bebida");
            Console.WriteLine("3 - Ver Resumo do Pedido");
            Console.WriteLine("4 - Finalizar Pedido e Sair");
            Console.Write("Opção desejada: ");

            try
            {
                string opcao = Console.ReadLine() ?? "";

                switch (opcao)
                {
                    case "1":
                        AdicionarLancheDoCardapio(pedidoAtual, ref contadorCodigoItemPedido);
                        break;
                    case "2":
                        AdicionarBebidaDoCardapio(pedidoAtual, ref contadorCodigoItemPedido);
                        break;
                    case "3":
                        if (pedidoAtual.EstaVazio())
                            Console.WriteLine("\n>>> O pedido está vazio.\n");
                        else
                            pedidoAtual.ExibirResumoPedido();
                        break;
                    case "4":
                        if (pedidoAtual.EstaVazio())
                        {
                            Console.WriteLine("\n>>> O pedido está vazio! Adicione itens antes de finalizar.\n");
                        }
                        else
                        {
                            pedidoAtual.ExibirResumoPedido();
                            Console.WriteLine("Pedido finalizado com sucesso!");
                            executando = false;
                        }
                        break;
                    default:
                        Console.WriteLine("\n[Erro] Opção inválida. Escolha entre 1 e 4.\n");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Erro Inesperado] {ex.Message}\n");
            }
        }
    }

    static void InicializarCardapio()
    {

        cardapioLanches.Add(new Lanche(1, "X-Burguer Tradicional", 15.00m));
        cardapioLanches.Add(new Lanche(2, "X-Bacon Especial", 18.50m));
        cardapioLanches.Add(new Lanche(3, "Cachorro-Quente Prensa", 12.00m));


        cardapioBebidas.Add(new Bebida(10, "Refrigerante Guaraná", 6.00m, "300ml"));
        cardapioBebidas.Add(new Bebida(11, "Suco Natural de Laranja", 8.00m, "300ml"));
        cardapioBebidas.Add(new Bebida(12, "Água Mineral", 4.00m, "300ml"));

        cardapioExtras.Add((20, "Bacon Extra", 3.50m));
        cardapioExtras.Add((21, "Cheddar Cremoso", 2.50m));
        cardapioExtras.Add((22, "Ovo Frito", 2.00m));
        cardapioExtras.Add((23, "Hambúrguer Extra", 6.00m));
    }

    static void AdicionarLancheDoCardapio(Pedido pedido, ref int codigoPedido)
    {
        Console.WriteLine("\n--- LANCHES DISPONÍVEIS NO CARDÁPIO ---");
        foreach (var l in cardapioLanches)
        {
            Console.WriteLine($"[Código {l.Codigo}] {l.Descricao} - Preço Base: R$ {l.PrecoBase:F2}");
        }

        int codigoEscolhido = LerIntValido("Digite o CÓDIGO do lanche desejado: ");

        Lanche modeloLanche = cardapioLanches.Find(l => l.Codigo == codigoEscolhido);

        if (modeloLanche == null)
        {
            Console.WriteLine("[Erro] Código de lanche não encontrado no cardápio!\n");
            return;
        }

        Lanche novoLanche = new Lanche(codigoPedido++, modeloLanche.Descricao, modeloLanche.PrecoBase);

        Console.Write("Deseja adicionar ingredientes extras? (S para Sim / Qualquer tecla para Não): ");
        string resposta = (Console.ReadLine() ?? "").Trim().ToUpper();

      
        while (resposta == "S")
        {
            Console.WriteLine("\n--- EXTRAS DISPONÍVEIS ---");
            foreach (var extra in cardapioExtras)
            {
                Console.WriteLine($"[Código {extra.Codigo}] {extra.Nome} - R$ {extra.Preco:F2}");
            }

            int codigoExtra = LerIntValido("Digite o CÓDIGO do extra desejado: ");
            int indexExtra = cardapioExtras.FindIndex(e => e.Codigo == codigoExtra);

            if (indexExtra != -1)
            {
                var extraEscolhido = cardapioExtras[indexExtra];
                novoLanche.AdicionarExtra(extraEscolhido.Nome, extraEscolhido.Preco);
                Console.WriteLine($"-> {extraEscolhido.Nome} adicionado ao lanche!\n");
            }
            else
            {
                Console.WriteLine("[Erro] Código de extra não encontrado no cardápio.\n");
            }

            Console.Write("Deseja adicionar mais um extra? (S/N): ");
            resposta = (Console.ReadLine() ?? "").Trim().ToUpper();
        }

        pedido.AdicionarItem(novoLanche);
        Console.WriteLine($"-> {novoLanche.Descricao} adicionado ao pedido com sucesso!\n");
    }

    static void AdicionarBebidaDoCardapio(Pedido pedido, ref int codigoPedido)
    {
        Console.WriteLine("\n--- BEBIDAS DISPONÍVEIS NO CARDÁPIO ---");
        foreach (var b in cardapioBebidas)
        {
            Console.WriteLine($"[Código {b.Codigo}] {b.Descricao} - Preço Base: R$ {b.PrecoBase:F2}");
        }

        int codigoEscolhido = LerIntValido("Digite o CÓDIGO da bebida desejada: ");

        Bebida modeloBebida = cardapioBebidas.Find(b => b.Codigo == codigoEscolhido);

        if (modeloBebida == null)
        {
            Console.WriteLine("[Erro] Código de bebida não encontrado no cardápio!\n");
            return;
        }

        string tamanho = LerTamanhoValido();

        Bebida novaBebida = new Bebida(codigoPedido++, modeloBebida.Descricao, modeloBebida.PrecoBase, tamanho);
        pedido.AdicionarItem(novaBebida);

        Console.WriteLine($"-> {novaBebida.Descricao} ({tamanho}) adicionada ao pedido com sucesso!\n");
    }

  

    static int LerIntValido(string mensagem)
    {
        while (true)
        {
            Console.Write(mensagem);
            if (int.TryParse(Console.ReadLine(), out int valor))
                return valor;

            Console.WriteLine("[Erro] Digite um número inteiro válido.\n");
        }
    }

    static string LerTamanhoValido()
    {
        while (true)
        {
            Console.Write("Escolha o tamanho da bebida (300ml, 500ml, 1L): ");
            string entrada = (Console.ReadLine() ?? "").Trim().ToLower();

            if (entrada == "300ml" || entrada == "500ml" || entrada == "1l")
                return entrada == "1l" ? "1L" : entrada;

            Console.WriteLine("[Erro] Digite exatamente 300ml, 500ml ou 1L.\n");
        }
    }
}