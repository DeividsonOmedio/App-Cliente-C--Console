using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using AppClientes.interfaces;
using AppClientes.model;

namespace AppClientes.repositorios
{
    public class VendasRepositorio : IVendas
    {
        private readonly IProdutos _produtos = new ProdutoRepositorio();
        private readonly ICliente _clientes = new ClienteRepositorio();
        List<Venda> vendas = new List<Venda>();

        public VendasRepositorio()
        {
            LerDados();
        }

        // cadastro do pedido
        public void Cadastrar()
        {
            Dictionary<Produto, int> produtos = new Dictionary<Produto, int>();
            Console.WriteLine("Cadastrando Venda");
            Console.WriteLine("Digite o id do cliente");
            string idCliente = Console.ReadLine();
            do
            {
            nomeProduto:
                Console.WriteLine("Digite o nome do produto");
                string novoProduto = Console.ReadLine();
                Produto produto = _produtos.BuscarPorNome(novoProduto);
                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado");
                    Thread.Sleep(1000);
                    goto nomeProduto;
                }
                Console.WriteLine("Digite a quantidade do produto");
                int quantidade = int.Parse(Console.ReadLine());
                produtos.Add(produto, quantidade);
                Console.WriteLine("Deseja adicionar mais produtos? (s/n)");
                string resposta = Console.ReadLine();
                if (resposta == "n")
                {
                    break;
                }
            } while (true);
            var cliente = _clientes.BuscarPorId(int.Parse(idCliente));
            var venda = new Venda
            {
                Id = vendas.Count + 1,
                Cliente = cliente,
                Produtos = produtos,
                Status = "Pendente" // Definindo status inicial
            };

            vendas.Add(venda);
            GravarDados();
        }

        public void Editar()
        {
            Console.WriteLine("Digite o id da venda que deseja editar");
            int id = int.Parse(Console.ReadLine());
            var venda = vendas.FirstOrDefault(x => x.Id == id);
            if (venda == null)
            {
                Console.WriteLine("Venda não encontrada");
                return;
            }
            Console.WriteLine(venda.ToString());
            Console.WriteLine("Digite o id do cliente");
            string idCliente = Console.ReadLine();
            Dictionary<Produto, int> produtos = new Dictionary<Produto, int>();
            do
            {
            nomeProduto:
                Console.WriteLine("Digite o nome do produto");
                string novoProduto = Console.ReadLine();
                Produto produto = _produtos.BuscarPorNome(novoProduto);
                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado");
                    Thread.Sleep(1000);
                    goto nomeProduto;
                }
                Console.WriteLine("Digite a quantidade do produto");
                int quantidade = int.Parse(Console.ReadLine());
                produtos.Add(produto, quantidade);
                Console.WriteLine("Deseja adicionar mais produtos? (s/n)");
                string resposta = Console.ReadLine();
                if (resposta == "n")
                {
                    break;
                }
            } while (true);
            var cliente = _clientes.BuscarPorId(int.Parse(idCliente));
            venda.Cliente = cliente;
            venda.Produtos = produtos;

            GravarDados();
        }

        public void Excluir()
        {
            Console.WriteLine("Digite o id da venda que deseja excluir");
            int id = int.Parse(Console.ReadLine());
            var venda = vendas.FirstOrDefault(x => x.Id == id);
            if (venda == null)
            {
                Console.WriteLine("Venda não encontrada");
                return;
            }
            Console.WriteLine(venda.ToString());
            vendas.Remove(venda);
            GravarDados();
        }

        public void Exibir()
        {
            foreach (var venda in vendas)
            {
                Console.WriteLine(venda.ToString());
            }
            Console.ReadKey();
        }

        public void GravarDados()
        {
            string diretorio = "dados";
            string caminhoArquivo = Path.Combine(diretorio, "vendas.txt");

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            var json = JsonSerializer.Serialize(vendas);
            File.WriteAllText(caminhoArquivo, json);
        }

        public void LerDados()
        {
            string diretorio = "dados";
            string caminhoArquivo = Path.Combine(diretorio, "vendas.txt");

            if (File.Exists(caminhoArquivo))
            {
                var dados = File.ReadAllText(caminhoArquivo);
                Console.WriteLine("Dados lidos do arquivo:");
                Console.WriteLine(dados);

                var vendasArquivo = JsonSerializer.Deserialize<List<Venda>>(dados);
                if (vendasArquivo != null)
                {
                    vendas = vendasArquivo;
                }
                else
                {
                    Console.WriteLine("Não foi possível deserializar os dados do arquivo.");
                }
            }
            else
            {
                Console.WriteLine("Arquivo de dados não encontrado. Um novo arquivo será criado.");
            }
        }

        public void Vender()
        {
            Console.WriteLine("Digite o id da venda a ser concluída");
            int id = int.Parse(Console.ReadLine());
            var venda = vendas.FirstOrDefault(x => x.Id == id);

            if (venda == null)
            {
                Console.WriteLine("Venda não encontrada");
                return;
            }

            venda.Status = "Vendido";
            GravarDados();

            Console.WriteLine($"Venda {venda.Id} concluída com sucesso!");
        }

        public void VisualizarProximo()
        {
            var proximaVenda = vendas.FirstOrDefault(x => x.Status == "Pendente");

            if (proximaVenda == null)
            {
                Console.WriteLine("Nenhuma venda pendente.");
                return;
            }

            Console.WriteLine($"Próxima venda a ser concluída:\n{proximaVenda.ToString()}");
        }
    }
}
