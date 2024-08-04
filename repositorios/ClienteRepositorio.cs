using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppClientes
{
    public class ClienteRepositorio
    {
        public List<Cliente> clientes = new List<Cliente>();

        public void GravarDadosClientes()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(clientes);
            File.WriteAllText("../dados/clientes.txt", json);
        }

        public void LerDadosClientes()
        {
            if (File.Exists("../dados/clientes.txt"))
            {
                var dados = File.ReadAllText("../dados/clientes.txt");
                var clientesArquivo = System.Text.Json.JsonSerializer.Deserialize<List<Cliente>>(dados);
                clientes.AddRange(clientesArquivo);
            }
        }

        public void ExcluirCliente()
        {
            Console.Clear();
            Console.Write("Informe o código do cliente: ");
            var codigo = Console.ReadLine();

            var cliente = clientes.FirstOrDefault(p => p.Id == int.Parse(codigo));

            if (cliente == null)
            {
                Console.WriteLine("Cliente não encontrado! [Enter]");
                Console.ReadKey();
                return;
            }

            ImprimirCliente(cliente);
            Console.Write("Deseja realmente excluir o cliente? (S/N): ");
            var resposta = Console.ReadLine();
            if (resposta.ToUpper() != "S")
            {
                Console.WriteLine("Operação cancelada! [Enter]");
                Console.ReadKey();
                return;
            }

            clientes.Remove(cliente);

            Console.WriteLine("Cliente removido com sucesso! [Enter]");

            Console.ReadKey();
        }

        public void EditarCliente()
        {
            Console.Clear();
            Console.Write("Informe o código do cliente: ");
            var codigo = Console.ReadLine();

            var cliente = clientes.FirstOrDefault(p => p.Id == int.Parse(codigo));

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
                Console.Write(Environment.NewLine);

                Console.Write("Data de nascimento: ");
                var dataNascimento = DateOnly.Parse(Console.ReadLine());
                Console.Write(Environment.NewLine);

                Console.Write("Desconto: ");
                var desconto = decimal.Parse(Console.ReadLine());
                Console.Write(Environment.NewLine);

                cliente.Nome = nome;
                cliente.DataNascimento = dataNascimento;
                cliente.Desconto = desconto;
                cliente.CadastradoEm = DateTime.Now;


                Console.WriteLine("Cliente Alterado com sucesso! [Enter]");
                ImprimirCliente(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao cadastrar cliente, Verifique se os dados foram inseridos corretamente. ERROR: " + e.Message);
                Console.ReadKey();
                return;
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
                Console.Write(Environment.NewLine);

                Console.Write("Data de nascimento: ");
                var dataNascimento = DateOnly.Parse(Console.ReadLine());
                Console.Write(Environment.NewLine);

                Console.Write("Desconto: ");
                var desconto = decimal.Parse(Console.ReadLine());
                Console.Write(Environment.NewLine);

                var cliente = new Cliente();
                cliente.Id = clientes.Count + 1;
                cliente.Nome = nome;
                cliente.DataNascimento = dataNascimento;
                cliente.Desconto = desconto;
                cliente.CadastradoEm = DateTime.Now;
                clientes.Add(cliente);

                Console.WriteLine("Cliente cadastrado com sucesso! [Enter]");
                ImprimirCliente(cliente);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao cadastrar cliente, Verifique se os dados foram inseridos corretamente. ERROR: " + e.Message);
                Console.ReadKey();
                return;
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
            foreach (var cliente in clientes)
            {
                ImprimirCliente(cliente);
            }

            Console.ReadKey();
        }
    }
}