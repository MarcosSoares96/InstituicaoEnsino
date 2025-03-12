using InstituicaoEnsino.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace InstituicaoEnsino.Controllers
{
    public class InstituicaoController : Controller ///Controller class is a base class for an MVC controller with view support.
    {
        /// IActionResult interface defines a contract that represents the result of an action method.
        public IActionResult Index()
        {
            return View(instituicoes.OrderBy(i => i.Nome));
        }

        private static IList<Instituicao> instituicoes = new List<Instituicao> 
        {
            new Instituicao() {InstituicaoID = 1, Nome = "UniParaná", Endereco = "Paraná"},
            new Instituicao() {InstituicaoID = 2, Nome = "UniSanta", Endereco = "Santa Catarina"},
            new Instituicao() {InstituicaoID = 3, Nome = "UniSãoPaulo", Endereco = "São Paulo"},
            new Instituicao() {InstituicaoID = 4, Nome = "UniSulgrandense", Endereco = "Rio Grande do Sul"},
            new Instituicao() {InstituicaoID = 5, Nome = "UniCarioca", Endereco = "Rio de Janeiro"}
        };

        // GET: Createa -> apenas para destacar que a action será atendida por um HTTP GET
        /// ActionResult, an abstract class, is a default implementation of Microsoft.AspNetCore.Mvc.IActionResult.
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Instituicao instituicao)
        {
            instituicoes.Add(instituicao);
            instituicao.InstituicaoID = instituicoes.Select(i => i.InstituicaoID).Max() + 1;
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long id)
        {
            return View(instituicoes.Where(i => i.InstituicaoID == id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Instituicao instituicao)
        {
            instituicoes.Remove(instituicoes.Where(i => i.InstituicaoID == instituicao.InstituicaoID).First());
            instituicoes.Add(instituicao);
            return RedirectToAction("Index");
        }

        public ActionResult Details(long id)
        {
            return View(instituicoes.Where(i => i.InstituicaoID == id).First());
        }

        public ActionResult Delete(long id)
        {
            return View(instituicoes.Where(i => i.InstituicaoID == id).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Instituicao instituicao)
        {
            instituicoes.Remove(instituicoes.Where(i => i.InstituicaoID == instituicao.InstituicaoID).First());
            return RedirectToAction("Index");
        }
    }

    /*
     Obs:
        Uma  classe  Controller  fornece métodos que  respondem  a requisições  HTTP  criadas  para  uma  aplicação  ASP.NET  Core MVC. 
     */
}
