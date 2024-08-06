using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AppClientes.model;

namespace AppClientes
{
    public class ClienteRepositorio
    {
        public List<Cliente> Clientes { get; private set; } = new List<Cliente>();

        public void GravarDadosClientes()
        {
            string diretorio = "dados";
            string caminhoArquivo = Path.Combine(diretorio, "clientes.txt");

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            var json = JsonSerializer.Serialize(Clientes);
            File.WriteAllText(caminhoArquivo, json);
        }

        public void LerDadosClientes()
        {
            string diretorio = "dados";
            string caminhoArquivo = Path.Combine(diretorio, "clientes.txt");

            if (!Directory.Exists(diretorio))
            {
                Directory.CreateDirectory(diretorio);
            }

            if (File.Exists(caminhoArquivo))
            {
                var dados = File.ReadAllText(caminhoArquivo);

                Console.WriteLine("caminhoArquivo");
                Console.WriteLine(caminhoArquivo);
                Console.WriteLine(dados);
                var clientesArquivo = JsonSerializer.Deserialize<List<Cliente>>(dados);
                if (clientesArquivo != null)
                {
                    Clientes.AddRange(clientesArquivo);
                }
            }
        }

        public void ExcluirCliente()
        {
            Console.Clear();
            Console.Write("Informe o código do cliente: ");
            var codigo = Console.ReadLine();

            if (int.TryParse(codigo, out int id))
            {
                var cliente = Clientes.FirstOrDefault(p => p.Id == id);

                if (cliente == null)
                {
                    Console.WriteLine("Cliente não encontrado! [Enter]");
                    Console.ReadKey();
                    return;
                }

                ImprimirCliente(cliente);
                Console.Write("Deseja realmente excluir o cliente? (S/N): ");
                var resposta = Console.ReadLine();
                if (resposta?.ToUpper() != "S")
                {
                    Console.WriteLine("Operação cancelada! [Enter]");
                    Console.ReadKey();
                    return;
                }

                Clientes.Remove(cliente);
                GravarDadosClientes();

                Console.WriteLine("Cliente removido com sucesso! [Enter]");
            }
            else
            {
                Console.WriteLine("Código inválido! [Enter]");
            }

            Console.ReadKey();
        }

        public void EditarCliente()
        {
            Console.Clear();
            Console.Write("Informe o código do cliente: ");
            var codigo = Console.ReadLine();

            if (int.TryParse(codigo, out int id))
            {
                var cliente = Clientes.FirstOrDefault(p => p.Id == id);

                if (cliente == null)
                {
                    Console.WriteLine("Cliente não encontrado! [Enter]");
                    Console.ReadKey();
                    return;
                }

                ImprimirCliente(cliente);
                try
                {
                    Console.Write("Nome do cliente: ");
                    var nome = Console.ReadLine();

                    Console.Write("Data de nascimento: ");
                    var dataNascimento = DateOnly.Parse(Console.ReadLine());

                    Console.Write("Desconto: ");
                    var desconto = decimal.Parse(Console.ReadLine());

                    cliente.Nome = nome;
                    cliente.DataNascimento = dataNascimento;
                    cliente.Desconto = desconto;
                    cliente.CadastradoEm = DateTime.Now;

                    GravarDadosClientes();

                    Console.WriteLine("Cliente alterado com sucesso! [Enter]");
                    ImprimirCliente(cliente);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao editar cliente, verifique se os dados foram inseridos corretamente. ERROR: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("Código inválido! [Enter]");
            }

            Console.ReadKey();
        }

        public void CadastrarCliente()
        {
            Console.Clear();

            try
            {
                Console.Write("Nome do cliente: ");
                var nome = Console.ReadLine();

                Console.Write("Data de nascimento: ");
                var dataNascimento = DateOnly.Parse(Console.ReadLine());

                Console.Write("Desconto: ");
                var desconto = decimal.Parse(Console.ReadLine());

                var cliente = new Cliente
                {
                    Id = Clientes.Count + 1,
                    Nome = nome,
                    DataNascimento = dataNascimento,
                    Desconto = desconto,
                    CadastradoEm = DateTime.Now
                };

                Clientes.Add(cliente);
                GravarDadosClientes();

                Console.WriteLine("Cliente cadastrado com sucesso! [Enter]");
                ImprimirCliente(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao cadastrar cliente, verifique se os dados foram inseridos corretamente. ERROR: " + e.Message);
            }

            Console.ReadKey();
        }

        public void ImprimirCliente(Cliente cliente)
        {
            Console.WriteLine("ID.............: " + cliente.Id);
            Console.WriteLine("Nome...........: " + cliente.Nome);
            Console.WriteLine("Desconto.......: " + cliente.Desconto.ToString("0.00"));
            Console.WriteLine("Data Nascimento: " + cliente.DataNascimento);
            Console.WriteLine("Data Cadastro..: " + cliente.CadastradoEm);
            Console.WriteLine("------------------------------------");
        }

        public void ExibirClientes()
        {
            Console.Clear();
            foreach (var cliente in Clientes)
            {
                ImprimirCliente(cliente);
            }

            Console.ReadKey();
        }
    }
}
