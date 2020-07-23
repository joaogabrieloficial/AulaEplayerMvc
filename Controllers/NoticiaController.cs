using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AulaE_playerModel.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AulaE_playerModel.Controllers
{
    public class NoticiaController : Controller
    {
       

        Noticias noticiaModel = new Noticias();

        

        public IActionResult Index()
        {
            ViewBag.Noticias = noticiaModel.ReadAll();
            return View();
        }

        public IActionResult Cadastrar(IFormCollection form)
        {
             
            Noticias novaNoticia = new Noticias();
            novaNoticia.IdNoticia = Int32.Parse(form["idNoticia"]);
            novaNoticia.Titulo = form ["Titulo"];
            novaNoticia.Texto = form ["Texto"];
            
             var file    = form.Files[0];
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticias");

            if(file != null)
            {    /// Cria diretório se não existe
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                novaNoticia.Imagem   = file.FileName;
            }
            else
            {
                novaNoticia.Imagem   = "padrao.png";
            }


            noticiaModel.Create(novaNoticia);

            ViewBag.equipes =  noticiaModel.ReadAll();

            return LocalRedirect("~/Noticia");


        }


          [Route("{id}")]
        public IActionResult  Excluir(int id)
        {
          noticiaModel.Delete(id);
          return LocalRedirect("~/Noticia");

        }
    }  
}
