using InstituicaoEnsino.Data;
using InstituicaoEnsino.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace InstituicaoEnsino.Controllers
{
    public class InstituicaoController : Controller /// Classe base do controlador para uma aplicação MVC com suporte a views.
    {
        private readonly IESContext _context; /// Contexto do banco de dados utilizado para realizar operações de acesso a dados para a entidade 'Instituicao'.

        public InstituicaoController(IESContext context)  /// Construtor que injeta o contexto do banco de dados para interações com a tabela de 'Instituicoes'.
        {
            _context = context;
        }

        /// A interface IActionResult define um contrato que representa o resultado de um método de ação.
        public async Task<ActionResult> Index()
        {
            return View(await _context.Instituicoes.ToListAsync()); /// Retorna a view com a lista de 'Instituicoes' recuperada do banco de dados.
        }

        // GET: Create -> Método que lida com a requisição HTTP GET para exibir o formulário de criação de uma nova instituição.
        public IActionResult Create()
        {
            return View(); /// Retorna a view para criar uma nova instituição.
        }

        [HttpPost]  /// Indica que este método lida com requisições HTTP POST.
        [ValidateAntiForgeryToken]  /// Protege contra ataques CSRF (Cross-Site Request Forgery) garantindo que o formulário tenha origem legítima.
        public async Task<IActionResult> Create([Bind("Nome,Endereco")] Instituicao instituicao)
        {
            try
            {
                if (ModelState.IsValid)  /// Verifica se o modelo da instituição é válido (se todos os campos obrigatórios foram preenchidos corretamente).
                {
                    _context.Add(instituicao);  /// Adiciona a nova instituição ao contexto do banco de dados.
                    await _context.SaveChangesAsync();  /// Salva as alterações no banco de dados de forma assíncrona.
                    return RedirectToAction(nameof(Index));  /// Redireciona para a ação Index após a criação da instituição.
                }
            }
            catch (DbUpdateException)  /// Captura exceções que ocorrem durante a atualização do banco de dados.
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");  /// Adiciona um erro ao ModelState se houver falha ao salvar no banco de dados.
            }
            return View(instituicao);  /// Se o modelo não for válido ou ocorrer erro, retorna a view com a instituição para o usuário corrigir.
        }

        // GET: Edit -> Método que lida com a requisição HTTP GET para exibir os dados de uma instituição existente para edição.
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)  /// Verifica se o ID fornecido é nulo. Se for, retorna NotFound.
            {
                return NotFound();
            }

            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);  /// Busca a instituição no banco de dados pelo ID.

            if (instituicao == null)  /// Se a instituição não for encontrada, retorna NotFound.
            {
                return NotFound();
            }
            return View(instituicao);  /// Retorna a view com os dados da instituição para edição.
        }

        [HttpPost]  /// Indica que o método lida com requisições HTTP POST.
        [ValidateAntiForgeryToken]  /// Protege contra CSRF.
        public async Task<IActionResult> Edit(long? id, [Bind("InstituicaoID,Nome,Endereco")] Instituicao instituicao)
        {
            if (id != instituicao.InstituicaoID)  /// Verifica se o ID fornecido na URL corresponde ao ID do objeto de instituição recebido.
            {
                return NotFound();  /// Se os IDs não coincidirem, retorna NotFound.
            }

            if (ModelState.IsValid)  /// Verifica se o modelo da instituição é válido.
            {
                try
                {
                    _context.Update(instituicao);  /// Atualiza os dados da instituição no contexto do banco de dados.
                    await _context.SaveChangesAsync();  /// Salva as alterações no banco de dados de forma assíncrona.
                }
                catch (DbUpdateConcurrencyException)  /// Captura exceções relacionadas à concorrência de dados no banco de dados.
                {
                    if (!InstituicaoExists(instituicao.InstituicaoID))  /// Verifica se a instituição ainda existe no banco.
                    {
                        return NotFound();  /// Se a instituição não existir, retorna NotFound.
                    }
                    else
                    {
                        throw;  /// Se for outro tipo de erro, relança a exceção.
                    }
                }
                return RedirectToAction(nameof(Index));  /// Redireciona para a ação Index após a edição bem-sucedida da instituição.
            }
            return View(instituicao);  /// Se o modelo não for válido, retorna a view para o usuário corrigir os dados.
        }

        // Método auxiliar para verificar se a instituição existe no banco de dados.
        private bool InstituicaoExists(long? id)
        {
            return _context.Instituicoes.Any(e => e.InstituicaoID == id);  /// Verifica se existe uma instituição com o ID fornecido no banco de dados.
        }

        // GET: Details -> Método que lida com a requisição HTTP GET para exibir os detalhes de uma instituição específica.
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)  /// Verifica se o ID fornecido é nulo. Se for, retorna NotFound.
            {
                return NotFound();
            }

            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);  /// Busca a instituição no banco de dados pelo ID.

            if (instituicao == null)  /// Se a instituição não for encontrada, retorna NotFound.
            {
                return NotFound();
            }
            return View(instituicao);  /// Retorna a view com os detalhes da instituição.
        }

        // GET: Delete -> Método que lida com a requisição HTTP GET para exibir a confirmação antes de excluir uma instituição.
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)  /// Verifica se o ID fornecido é nulo. Se for, retorna NotFound.
            {
                return NotFound();
            }

            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);  /// Busca a instituição no banco de dados pelo ID.

            if (instituicao == null)  /// Se a instituição não for encontrada, retorna NotFound.
            {
                return NotFound();
            }
            return View(instituicao);  /// Retorna a view de confirmação de exclusão para o usuário.
        }

        // POST: Instituicao/Delete/5 -> Método que lida com a requisição HTTP POST para excluir uma instituição.
        [HttpPost, ActionName("Delete")]  /// Indica que este método é um POST para exclusão da instituição.
        [ValidateAntiForgeryToken]  /// Protege contra CSRF.
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);  /// Busca a instituição no banco de dados pelo ID.

            _context.Instituicoes.Remove(instituicao);  /// Remove a instituição do contexto do banco de dados.
            await _context.SaveChangesAsync();  /// Salva as alterações no banco de dados de forma assíncrona.
            return RedirectToAction(nameof(Index));  /// Redireciona para a ação Index após a exclusão da instituição.
        }
    }

    /*
      Observações:
        A classe Controller fornece métodos que respondem a requisições HTTP e interagem com as views e dados da aplicação.
        Cada ação (método) pode responder a tipos específicos de requisições (GET ou POST) e realizar operações de leitura ou escrita no banco de dados.
        As views são utilizadas para apresentar a interface de usuário.
        A proteção contra CSRF é garantida usando o atributo [ValidateAntiForgeryToken] em ações que recebem dados do usuário.
     */

}
