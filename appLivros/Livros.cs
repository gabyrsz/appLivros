namespace AppCrudLivros
{
    public class Livros
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public int ano_publicacao { get; set; }
        public string genero { get; set; }
        public int numero_paginas { get; set; }


        public override string ToString()
        {
            return $"Id: {id}, Título: {titulo}, Autor: {autor}, Ano publicação: {ano_publicacao},  Gênero: {genero},  Número de páginas: {numero_paginas}";
        }

    }
}
