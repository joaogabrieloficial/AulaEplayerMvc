using System;
using System.Collections.Generic;
using System.IO;


namespace AulaE_playerModel.Models
{
    public class Noticias :   INoticias

    {
          
        public int IdNoticia  { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Imagem { get; set; }

        private const string PATH ="Database/noticias.csv";

        public Noticias(){
            CreateFolderAndFile(PATH);
        }

        public void Create(Noticias n)
        {
            string [] linha = {PrepararLinha(n) };
           File.AppendAllLines(PATH,linha);
        }
         private string PrepararLinha(Noticias n)
        {
            return $"{n.IdNoticia};{n.Titulo};{n.Texto};{n.Imagem}";
        }

        public void Delete(int IdNoticia)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
                linhas.RemoveAll(x => x.Split(";")[0] == IdNoticia.ToString());

                RewriteCSV(PATH, linhas);
        }

        public List<Noticias> ReadAll()
        {
           List<Noticias> noticias = new List<Noticias>();
            string[] linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");
                Noticias noticia = new Noticias();
                noticia.IdNoticia = Int32.Parse(linha[0]);
                noticia.Titulo = linha[1];
                noticia.Texto = linha[2];
                noticia.Imagem = linha [3];

                noticias.Add(noticia);
            }
            return noticias;
        }

        public void Update(Noticias n)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
                linhas.RemoveAll(x => x.Split(";")[0] == n.IdNoticia.ToString());
                linhas.Add(PrepararLinha(n));
                
                RewriteCSV(PATH, linhas);
        }



        





         public void CreateFolderAndFile(string _path){

            string folder   = _path.Split("/")[0];
          

            if(!Directory.Exists(folder)){
                Directory.CreateDirectory(folder);
            }

            if(!File.Exists(_path)){
                File.Create(_path).Close();
            }
        }
         public List<string> ReadAllLinesCSV(string PATH){
            
            List<string> linhas = new List<string>();
            using(StreamReader file = new StreamReader(PATH))
            {
                string linha;
                while((linha = file.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }
            return linhas;
        }

        public void RewriteCSV(string PATH, List<string> linhas)
        {
            using(StreamWriter output = new StreamWriter(PATH))
            {
                foreach (var item in linhas)
                {
                    output.Write(item + "\n");
                }
            }
        }
   
    }
}