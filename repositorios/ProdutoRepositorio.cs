using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AppClientes.interfaces;
using AppClientes.model;

namespace AppClientes.repositorios
{
    public class ProdutoRepositorio : IProdutos
    {
        public List<Produto> Produtos { get; private set; } = new List<Produto>();

        public ProdutoRepositorio()
        {
            LerDados();
        }

        public void Cadastrar()
        {
            List<string> ingredientes = new List<string>();
            Console.WriteLine("Digite o nome do produto: ");
            var nome = Console.ReadLine();

            Console.WriteLine("Digite os ingredientes (pressione Enter para finalizar): ");
            while (true)
            {
                var novoIngrediente = Console.ReadLine();
                if (string.IsNullOrEmpty(novoIngrediente))
                {
                    break;
                }
                ingredientes.Add(novoIngrediente);
            }

            Console.WriteLine("Digite a quantidade: ");
            var quantidade = Console.ReadLine();
            Console.WriteLine("Digite o preço: ");
            var preco = Console.ReadLine();

            var produto = new Produto
            {
                Id = Produtos.Count + 1,
                Nome = nome,
                Ingredientes = ingredientes,
                Quantidade = int.Parse(quantidade),
                Preco = decimal.Parse(preco)
            };

            Produtos.Add(produto);
            GravarDados();
        }

        public void Editar()
        {
            Console.WriteLine("Digite o id do produto que deseja editar: ");
            var id = int.Parse(Console.ReadLine());
            var produto = Produtos.FirstOrDefault(x => x.Id == id);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado");
                return;
            }

            Console.WriteLine("Digite o novo nome do produto: ");
            var novoNome = Console.ReadLine();

            List<string> ingredientes = new List<string>();
            Console.WriteLine("Digite os novos ingredientes (pressione Enter para finalizar): ");
            while (true)
            {
                var novoIngrediente = Console.ReadLine();
                if (string.IsNullOrEmpty(novoIngrediente))
                {
                    break;
                }
                ingredientes.Add(novoIngrediente);
            }

            Console.WriteLine("Digite a nova quantidade: ");
            var novaQuantidade = Console.ReadLine();
            Console.WriteLine("Digite o novo preço: ");
            var novoPreco = Console.ReadLine();

            produto.Nome = novoNome;
            produto.Ingredientes = ingredientes;
            produto.Quantidade = int.Parse(novaQuantidade);
            produto.Preco = decimal.Parse(novoPreco);

            GravarDados();
        }

        public void Excluir()
        {
            Console.WriteLine("Digite o nome do produto que deseja excluir: ");
            var nome = Console.ReadLine();
            var produto = Produtos.FirstOrDefault(x => x.Nome == nome);

            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado");
                return;
            }

            Produtos.Remove(produto);
            Console.WriteLine("Produto removido com sucesso");
            GravarDados();
        }

        public void Exibir()
        {
            Console.Clear();

            foreach (var produto in Produtos)
            {
                Console.WriteLine($"ID: {produto.Id} - Nome: {produto.Nome} - Quantidade: {produto.Quantidade} - Preço: {produto.Preco}");
                Console.WriteLine("Ingredientes:");
                foreach (var ingrediente in produto.Ingredientes)
                {
                    Console.WriteLine($" - {ingrediente}");
                }
                Console.WriteLine("------------------------------------\n");
            }

            Console.ReadKey();
        }

        public void GravarDados()
        {
            string diretorio = "dados";
            string caminhoArquivo = Path.Combine(diretorio, "produtos.txt");

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            var json = JsonSerializer.Serialize(Produtos);
            File.WriteAllText(caminhoArquivo, json);
        }

        public void LerDados()
        {
            string diretorio = "dados";
            string caminhoArquivo = Path.Combine(diretorio, "produtos.txt");

            if (File.Exists(caminhoArquivo))
            {
                var dados = File.ReadAllText(caminhoArquivo);
                var produtosArquivo = JsonSerializer.Deserialize<List<Produto>>(dados);
                if (produtosArquivo != null)
                {
                    Produtos.AddRange(produtosArquivo);
                    if (Produtos.Count == 0)
                    {
                        CriarProdutosIniciais();
                        GravarDados();
                    }
                }
            }
            else
            {
                CriarProdutosIniciais();
                GravarDados();
            }
        }
        private void CriarProdutosIniciais()
        {
            if (Produtos.Count == 0)
            {
                Produtos.Add(new Produto
                {
                    Id = Produtos.Count + 1,
                    Nome = "Classico",
                    Ingredientes = new List<string> { "Pão", "Carne", "Queijo", "Alface", "Tomate" },
                    Quantidade = 50,
                    Preco = 15.99m
                });

                Produtos.Add(new Produto
                {
                    Id = Produtos.Count + 1,
                    Nome = "Cheddar",
                    Ingredientes = new List<string> { "Pão", "Carne", "Cheddar", "Bacon", "Cebola" },
                    Quantidade = 30,
                    Preco = 18.99m
                });

                Produtos.Add(new Produto
                {
                    Id = Produtos.Count + 1,
                    Nome = "Vegetariano",
                    Ingredientes = new List<string> { "Pão Integral", "Hamburguer de Grão de Bico", "Queijo Vegano", "Alface", "Tomate" },
                    Quantidade = 20,
                    Preco = 17.99m
                });

                GravarDados();
            }
        }

        public Produto BuscarPorNome(string nome)
        {
            LerDados();
            var produto = Produtos.FirstOrDefault(x => x.Nome.Equals(nome, StringComparison.CurrentCultureIgnoreCase));
            return produto;
        }

    }
}
