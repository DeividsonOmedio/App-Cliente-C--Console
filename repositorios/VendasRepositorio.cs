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
            Dictionary<string, int> produtos = new Dictionary<string, int>();
            Console.WriteLine("Cadastrando Venda");
            Console.WriteLine("Digite o id do cliente");
            string idCliente = Console.ReadLine();
            do
            {
            nomeProduto:
                Console.WriteLine("Digite o nome do produto");
                string novoProduto = Console.ReadLine();
                Produto produto = _produtos.BuscarPorNome(novoProduto);
                Console.WriteLine(produto);
                Console.ReadKey();
                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado");
                    Thread.Sleep(1000);
                    goto nomeProduto;
                }
                Console.WriteLine("Digite a quantidade do produto");
                int quantidade = int.Parse(Console.ReadLine());
                produtos.Add(produto.Nome, quantidade);
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
                Status = "Pendente" // Definindo status inicial
            };

            vendas.Add(venda);
            Console.WriteLine(venda.ToString());
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
            Dictionary<string, int> produtos = new Dictionary<string, int>();
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
            // Console.Clear();
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
            Console.WriteLine($"Dados gravados em {caminhoArquivo}: {json}"); // Mensagem de depuração
        }

        public void LerDados()
        {
            string diretorio = "dados";
            string caminhoArquivo = Path.Combine(diretorio, "vendas.txt");

            if (File.Exists(caminhoArquivo))
            {
                var dados = File.ReadAllText(caminhoArquivo);
                Console.WriteLine($"Dados lidos do arquivo {caminhoArquivo}: {dados}"); // Mensagem de depuração

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
                    Console.WriteLine("Não foi possível deserializar os dados do arquivo.");
                    CriarVendasIniciais();
                    GravarDados();
                }
            }
            else
            {
                Console.WriteLine("Arquivo não encontrado. Criando vendas iniciais.");
                CriarVendasIniciais();
                GravarDados();
            }
        }

        public void CriarVendasIniciais()
        {
            // Cria um cliente fictício para teste
            var cliente = _clientes.BuscarPorId(1); // Assume que o cliente com ID 1 já existe

            // Cria um dicionário de produtos fictícios para teste
            var produto1 = _produtos.BuscarPorNome("Hamburguer Vegetariano"); // Assume que o produto "Produto A" existe
            var produto2 = _produtos.BuscarPorNome("Hamburguer Cheddar"); // Assume que o produto "Produto B" existe

            // Verifica se os produtos foram encontrados
            if (produto1 == null || produto2 == null)
            {
                Console.WriteLine("Produtos fictícios não encontrados. Certifique-se de que os produtos existem.");
                return;
            }

            // Cria a primeira venda com status "Pendente"
            var vendaPendente = new Venda
            {
                Id = vendas.Count + 1,
                Cliente = cliente.Nome,
                DataVenda = DateTime.Now,
                Produtos = new Dictionary<string, int>
        {
            { produto1.Nome, 2 } // Adiciona 2 unidades do produto1
        },
                Status = "Pendente"
            };

            // Cria a segunda venda com status "Finalizada"
            var vendaFinalizada = new Venda
            {
                Id = vendas.Count + 2,
                Cliente = cliente.Nome,
                DataVenda = DateTime.Now,
                Produtos = new Dictionary<string, int>
        {
            { produto2.Nome, 3 } // Adiciona 3 unidades do produto2
        },
                Status = "Finalizada"
            };

            // Adiciona as vendas à lista e grava os dados
            vendas.Add(vendaPendente);
            vendas.Add(vendaFinalizada);
            GravarDados();

            Console.WriteLine("Vendas iniciais criadas com sucesso!");
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
