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

        public void Cadastrar()
        {
            Dictionary<string, int> produtos = new Dictionary<string, int>();
            decimal total = 0;
            Console.WriteLine("Cadastrando Venda");
        cliente:
            Console.WriteLine("Digite o id do cliente, 0 Listar Clientes ou 9 para Cadastrar novo Cliente");
            string idCliente = Console.ReadLine();
            if (idCliente == "9")
            {
                _clientes.Cadastrar();
                goto cliente;
            }
            if (idCliente == "0")
            {
                _clientes.Exibir();
                goto cliente;
            }
            do
            {
            nomeProduto:
                Console.WriteLine("Digite o nome do produto ou 0 Listar Produtos");
                string novoProduto = Console.ReadLine();
                if (novoProduto == "0")
                {
                    _produtos.Exibir();
                    goto nomeProduto;
                }
                Produto produto = _produtos.BuscarPorNome(novoProduto);
                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado");
                    Thread.Sleep(1000);
                    goto nomeProduto;
                }
                Console.WriteLine("Digite a quantidade do produto");
                int quantidade = int.Parse(Console.ReadLine());
                produtos.Add(produto.Nome, quantidade);
                total += produto.Preco * quantidade;
                Console.WriteLine("Deseja adicionar mais produtos? (s/n)");
                string resposta = Console.ReadLine();
                if (resposta == "n")
                {
                    break;
                }
            } while (true);
            Console.WriteLine("Venda sendo cadastrada");
            var cliente = _clientes.BuscarPorId(int.Parse(idCliente));
            var venda = new Venda
            {
                Id = vendas.Count + 1,
                Cliente = cliente.Nome,
                Produtos = produtos,
                DataVenda = DateTime.Now,
                ValorTotal = total,
                Status = "Pendente"
            };

            vendas.Add(venda);
            GravarDados();
            Console.Clear();
            Console.WriteLine("Venda Cadastrada com Sucesso\n");
            Console.WriteLine(venda.ToString());
            Console.WriteLine("\nDeseja Realizar o Pagamento? (s/n)");
            string respostaPagamento = Console.ReadLine();
            if (respostaPagamento == "s")
            {
                Vender(venda.ValorTotal);
            }

        }

        public void Editar()
        {
            Dictionary<string, int> produtos = new Dictionary<string, int>();
            decimal total = 0;
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
                produtos.Add(produto.Nome, quantidade);
                total += produto.Preco * quantidade;
                Console.WriteLine("Deseja adicionar mais produtos? (s/n)");
                string resposta = Console.ReadLine();
                if (resposta == "n")
                {
                    break;
                }
            } while (true);
            var cliente = _clientes.BuscarPorId(int.Parse(idCliente));
            venda.Cliente = cliente.Nome;
            venda.Produtos = produtos;
            venda.ValorTotal = total;

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
            LerDados();
            Console.Clear();
            Console.WriteLine("Vendas cadastradas");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Total de vendas: " + vendas.Count);
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
                var vendasArquivo = JsonSerializer.Deserialize<List<Venda>>(dados);
                if (vendasArquivo.Count < 1)
                {
                    CriarVendasIniciais();
                    GravarDados();
                }
                else if (vendasArquivo != null)
                {
                    vendas = vendasArquivo;
                }
                else
                {
                    CriarVendasIniciais();
                    GravarDados();
                }
            }
            else
            {
                CriarVendasIniciais();
                GravarDados();
            }
        }

        public void CriarVendasIniciais()
        {
            var cliente1 = _clientes.BuscarPorId(1);
            var cliente2 = _clientes.BuscarPorId(2);


            var produto1 = _produtos.BuscarPorNome("Hamburguer Vegetariano");
            var produto2 = _produtos.BuscarPorNome("Hamburguer Cheddar");

            if (produto1 == null || produto2 == null)
            {
                Console.WriteLine("Produtos fictícios não encontrados. Certifique-se de que os produtos existem.");
                return;
            }

            var vendaPendente = new Venda
            {
                Id = vendas.Count + 1,
                Cliente = cliente1.Nome,
                DataVenda = DateTime.Now,
                ValorTotal = 2 * produto1.Preco,
                Produtos = new Dictionary<string, int>
        {
            { produto1.Nome, 2 }
        },
                Status = "Pendente"
            };

            var vendaFinalizada = new Venda
            {
                Id = vendas.Count + 2,
                Cliente = cliente2.Nome,
                DataVenda = DateTime.Now,
                ValorTotal = 3 * produto2.Preco,
                Produtos = new Dictionary<string, int>
        {
            { produto2.Nome, 3 }
        },
                Status = "Finalizada"
            };

            vendas.Add(vendaPendente);
            vendas.Add(vendaFinalizada);
            GravarDados();
        }


        public void Vender(decimal? valorTotal)
        {
            Venda vendaAtual = null;
            if (valorTotal == null)
            {
                Console.WriteLine("Digite o id da venda a ser concluída");
                int id = int.Parse(Console.ReadLine());
                vendaAtual = vendas.FirstOrDefault(x => x.Id == id);

                if (vendaAtual == null)
                {
                    Console.WriteLine("Venda não encontrada");
                    return;
                }
            }

            if (vendaAtual == null)
            {
                int id = vendas.Max(x => x.Id);
                vendaAtual = vendas.FirstOrDefault(x => x.Id == id);
                if (vendaAtual == null)
                {
                    Console.WriteLine("Venda não encontrada");
                    return;
                }
            }
            Console.WriteLine($"\nValor a Pagar {vendaAtual.ValorTotal}\n");
            Console.WriteLine("Pagamento Realizado? (s/n)");
            string resposta = Console.ReadLine();
            if (resposta == "n")
            {
                Console.WriteLine("Pagamento não realizado");
                return;
            }

            vendaAtual.Status = "Vendido";
            GravarDados();

            Console.WriteLine($"Venda {vendaAtual.Id} concluída com sucesso!");
        }


        public Venda? VisualizarProximo()
        {
            var proximaVenda = vendas.FirstOrDefault(x => x.Status == "Pendente");

            if (proximaVenda == null)
            {
                Console.WriteLine("Nenhuma venda pendente.");
                return null;
            }

            return proximaVenda;
        }
    }
}
