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
    public class EquipeController : Controller
    {
       

        Equipe equipeModel = new Equipe();

        

        public IActionResult Index()
        {
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }

        public IActionResult Cadastrar(IFormCollection form)
        {
             
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse(form["idEquipe"]);
            novaEquipe.Nome = form ["Nome"];
            
             var file    = form.Files[0];
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

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
                novaEquipe.Imagem   = file.FileName;
            }
            else
            {
                novaEquipe.Imagem   = "padrao.png";
            }

            equipeModel.Create(novaEquipe);

            ViewBag.equipes =  equipeModel.ReadAll();

            return LocalRedirect("~/Equipe");


        }
         [Route("{id}")]
        public IActionResult  Excluir(int id)
        {
          equipeModel.Delete(id);
          return LocalRedirect("~/Equipe");

        }

    }  
}
