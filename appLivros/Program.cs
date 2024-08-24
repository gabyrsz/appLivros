using MySql.Data.MySqlClient;

namespace AppCrudPessoas
{
    public class Program
    {
        private static string connectionString = "Server=localhost;Database=db_aulas_2024;Uid=gaby;Pwd=1234567;SslMode=none;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1 - Adicionar livro");
                Console.WriteLine("2 - Listar livros");
                Console.WriteLine("3 - Editar livro");
                Console.WriteLine("4 - Excluir livro");
                Console.WriteLine("5 - Sair");
                Console.Write("Escolha uma opção acima:");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Adicionar();
                        break;
                    case "2":
                        Listar();
                        break;
                    case "3":
                        Editar();
                        break;
                    case "4":
                        Excluir();
                        break;
                    case "5":
                        Console.WriteLine(5);
                        return;

                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;
                }
            }
        }
        static void Adicionar()
        {
            Console.Write("Informe o título do livro:");
            string titulo = Console.ReadLine();
            Console.Write("Informe o autor do livro:");
            string autor = Console.ReadLine();
            Console.Write("Informe o ano de publicação do livro:");
            int ano_publicacao = int.Parse(Console.ReadLine());
            Console.Write("Informe o gênero do livro:");
            string genero =Console.ReadLine();
            Console.Write("Informe o número de páginas do livro:");
            int numero_paginas = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO tb_livros (titulo,autor,ano_publicacao,genero,numero_paginas) VALUES(@titulo,@autor,@ano_publicacaoo,@genero,@numero_paginas)";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@titulo", titulo);
                cmd.Parameters.AddWithValue("@autor", autor);
                cmd.Parameters.AddWithValue("@ano_publicacaoo", ano_publicacao);
                cmd.Parameters.AddWithValue("@genero", genero);
                cmd.Parameters.AddWithValue("@numero_paginas", numero_paginas);
                cmd.ExecuteNonQuery();
            }
            Console.Write("Livro cadastrado com sucesso!\n");
        }

        static void Listar()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id, titulo, autor, ano_publicacao, genero, numero_paginas FROM tb_livros";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id: {reader["id"]}, Título: {reader["titulo"]}, Ano publicacao: {reader["ano_publicacao"]}, Gênero: {reader["genero"]}, Número de páginas: {reader["numero_paginas"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Não exite esse livro cadastrado");
                    }
                }
            }
        }
        static void Excluir()
        {
            Console.WriteLine("Informe o Id do livro que deseja excluir:");
            int idExclusao = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM tb_livros WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@id", idExclusao);

                int linhaAfetada = cmd.ExecuteNonQuery();
                if (linhaAfetada > 0)
                {
                    Console.WriteLine("Livro excluido com sucesso");
                }
                else
                {
                    Console.WriteLine("Livro não encontrado.");
                }
            }
        }
        static void Editar()
        {
            Console.WriteLine("Informe o Id do livro que deseja editar:");
            int idEditar = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM tb_livros WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", idEditar);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.Write("Informe o novo título do livro:");
                        string novo_titulo = Console.ReadLine();
                        Console.Write("Informe o novo autor do livro:");
                        string novo_autor = Console.ReadLine();
                        Console.Write("Informe o novo ano de publicação do livro:");
                        string novo_ano_publicacao =Console.ReadLine();
                        Console.Write("Informe o novo gênero do livro:");
                        string novo_genero = Console.ReadLine();
                        Console.Write("Informe o novo ano de publicação do livro:");
                        string novo_numero_paginas =Console.ReadLine();
                        reader.Close();

                        string queryUpdate = "UPDATE tb_livros SET titulo = @titulo, autor = @autor, ano_publicacao = @ano_publicacao,  genero = @genero,  numero_paginas = @numero_paginas WHERE Id = @Id";
                        cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@titulo", string.IsNullOrWhiteSpace(novo_titulo) ? reader["titulo"] : novo_titulo);
                        cmd.Parameters.AddWithValue("@autor", string.IsNullOrWhiteSpace(novo_autor) ? reader["autor"] : novo_autor);
                        cmd.Parameters.AddWithValue("@ano_publicacao", string.IsNullOrWhiteSpace(novo_ano_publicacao) ? reader["ano_publicacao"] : int.Parse(novo_ano_publicacao));
                        cmd.Parameters.AddWithValue("@genero", string.IsNullOrWhiteSpace(novo_genero) ? reader["genero"] : novo_genero);
                        cmd.Parameters.AddWithValue("@numero_paginas", string.IsNullOrWhiteSpace(novo_numero_paginas) ? reader["numero_paginas"] : int.Parse(novo_numero_paginas));
                        cmd.Parameters.AddWithValue("@Id", idEditar);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Livro atualizado com sucesso.");
                    }
                    else
                    {
                        Console.WriteLine("O Id informado não existe.");
                    }
                }
            }
        }
    }
}