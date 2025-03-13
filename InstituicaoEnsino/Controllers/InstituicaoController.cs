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
            return View(instituicoes.OrderBy(i => i.Nome)); ///Return a view with a list of 'Instituicoes' ordered by name.
        }

        private static IList<Instituicao> instituicoes = new List<Instituicao>
    {
        new Instituicao() {InstituicaoID = 1, Nome = "UniParaná", Endereco = "Paraná"},  ///List of predefined institutions with ID, Name, and Address.
        new Instituicao() {InstituicaoID = 2, Nome = "UniSanta", Endereco = "Santa Catarina"},
        new Instituicao() {InstituicaoID = 3, Nome = "UniSãoPaulo", Endereco = "São Paulo"},
        new Instituicao() {InstituicaoID = 4, Nome = "UniSulgrandense", Endereco = "Rio Grande do Sul"},
        new Instituicao() {InstituicaoID = 5, Nome = "UniCarioca", Endereco = "Rio de Janeiro"}
    };

        // GET: Create -> This is an HTTP GET request action method that returns the Create view.
        /// ActionResult, an abstract class, is a default implementation of Microsoft.AspNetCore.Mvc.IActionResult.
        public ActionResult Create()
        {
            return View(); ///Return the view for creating a new institution.
        }

        [HttpPost]  ///Indicates that the action handles HTTP POST requests.
        [ValidateAntiForgeryToken]  ///Ensures that the request is from a valid source to prevent CSRF attacks.
        public ActionResult Create(Instituicao instituicao)
        {
            instituicoes.Add(instituicao);  ///Add the newly created institution to the list.
            instituicao.InstituicaoID = instituicoes.Select(i => i.InstituicaoID).Max() + 1; ///Set a new ID by incrementing the maximum ID.
            return RedirectToAction("Index");  ///Redirect to the Index action to display the updated list.
        }

        // GET: Edit -> Action that retrieves and displays the current institution data for editing.
        public ActionResult Edit(long id)
        {
            return View(instituicoes.Where(i => i.InstituicaoID == id).First());  ///Find the institution by ID and return it for editing.
        }

        [HttpPost]  ///Indicates that the action handles HTTP POST requests.
        [ValidateAntiForgeryToken]  ///Ensures that the request is from a valid source to prevent CSRF attacks.
        public ActionResult Edit(Instituicao instituicao)
        {
            instituicoes.Remove(instituicoes.Where(i => i.InstituicaoID == instituicao.InstituicaoID).First());  ///Remove the old version of the institution.
            instituicoes.Add(instituicao);  ///Add the updated institution to the list.
            return RedirectToAction("Index");  ///Redirect to the Index action to display the updated list.
        }

        // GET: Details -> Action to display detailed information about a specific institution.
        public ActionResult Details(long id)
        {
            return View(instituicoes.Where(i => i.InstituicaoID == id).First());  ///Find the institution by ID and return it for detailed view.
        }

        // GET: Delete -> Action to display the confirmation page for deletion of a specific institution.
        public ActionResult Delete(long id)
        {
            return View(instituicoes.Where(i => i.InstituicaoID == id).First());  ///Find the institution by ID and return it for deletion confirmation.
        }

        [HttpPost]  ///Indicates that the action handles HTTP POST requests.
        [ValidateAntiForgeryToken]  ///Ensures that the request is from a valid source to prevent CSRF attacks.
        public ActionResult Delete(Instituicao instituicao)
        {
            instituicoes.Remove(instituicoes.Where(i => i.InstituicaoID == instituicao.InstituicaoID).First());  ///Remove the institution from the list.
            return RedirectToAction("Index");  ///Redirect to the Index action to display the updated list.
        }
    }

    /*
     Obs:
        Uma classe Controller fornece métodos que respondem a requisições HTTP criadas para uma aplicação ASP.NET Core MVC. 
        Cada método de ação no Controller manipula um tipo específico de requisição (GET ou POST) e pode interagir com os dados da aplicação.
        As views retornadas são as interfaces visuais que o usuário verá no navegador.
        As ações podem redirecionar para outras ações ou devolver dados para a visualização.
     */

}
