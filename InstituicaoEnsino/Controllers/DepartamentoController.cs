using InstituicaoEnsino.Data;
using InstituicaoEnsino.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstituicaoEnsino.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IESContext _context; /// Contexto do banco de dados, utilizado para acessar as entidades de 'Departamento' no banco de dados.

        public DepartamentoController(IESContext context)  ///Construtor que injeta o contexto do banco de dados para interações com as entidades.
        {
            this._context = context;
        }

        // GET: Departamento/Index
        /// Este método lida com a requisição HTTP GET para mostrar a lista de departamentos ordenada pelo nome.
        public async Task<ActionResult> Index()
        {
            return View(await _context.Departamentos.OrderBy(c => c.Nome).ToListAsync()); ///Retorna a lista de departamentos ordenada pelo nome na view.
        }

        // GET: Departamento/Create
        /// Este método lida com a requisição HTTP GET para exibir o formulário de criação de um novo departamento.
        public IActionResult Create()
        {
            return View(); ///Retorna a view para criar um novo departamento.
        }

        [HttpPost]  ///Indica que o método responde a requisições POST.
        [ValidateAntiForgeryToken]  ///Protege contra CSRF (Cross-Site Request Forgery) garantindo que o formulário é válido.
        public async Task<IActionResult> Create([Bind("Nome")] Departamento departamento)  ///Método para criar um novo departamento, recebendo dados do formulário.
        {
            try
            {
                if (ModelState.IsValid)  ///Verifica se o modelo do departamento é válido.
                {
                    _context.Add(departamento);  ///Adiciona o novo departamento ao contexto do banco de dados.
                    await _context.SaveChangesAsync();  ///Salva as mudanças no banco de dados de forma assíncrona.
                    return RedirectToAction(nameof(Index));  ///Redireciona para a ação Index após a criação com sucesso.
                }
            }
            catch (DbUpdateException)  ///Captura erros que podem ocorrer durante a atualização do banco de dados.
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");  ///Adiciona um erro ao ModelState se ocorrer uma falha no banco de dados.
            }
            return View(departamento);  ///Se algo der errado, retorna o departamento para que o usuário possa corrigir os dados.
        }

        // GET: Departamento/Edit/5
        /// Este método lida com a requisição HTTP GET para editar um departamento específico, usando seu ID.
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)  ///Verifica se o ID é nulo. Se for, retorna NotFound.
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);  ///Busca o departamento pelo ID.

            if (departamento == null)  ///Se o departamento não for encontrado, retorna NotFound.
            {
                return NotFound();
            }
            return View(departamento);  ///Retorna a view para editar o departamento.
        }

        [HttpPost]  ///Indica que o método responde a requisições POST.
        [ValidateAntiForgeryToken]  ///Protege contra CSRF.
        public async Task<IActionResult> Edit(long? id, [Bind("DepartamentoID,Nome")] Departamento departamento)
        {
            if (id != departamento.DepartamentoID)  ///Verifica se o ID passado na URL corresponde ao ID do departamento recebido.
            {
                return NotFound();  ///Se os IDs não coincidirem, retorna NotFound.
            }

            if (ModelState.IsValid)  ///Verifica se o modelo é válido.
            {
                try
                {
                    _context.Update(departamento);  ///Atualiza os dados do departamento no contexto do banco de dados.
                    await _context.SaveChangesAsync();  ///Salva as alterações no banco de dados de forma assíncrona.
                }
                catch (DbUpdateConcurrencyException)  ///Captura exceções relacionadas à concorrência no banco de dados.
                {
                    if (!DepartamentoExists(departamento.DepartamentoID))  ///Verifica se o departamento ainda existe.
                    {
                        return NotFound();  ///Se não existir, retorna NotFound.
                    }
                    else
                    {
                        throw;  ///Se não for um erro de existência, relança a exceção.
                    }
                }
                return RedirectToAction(nameof(Index));  ///Redireciona para a página de índice após a edição com sucesso.
            }
            return View(departamento);  ///Se o modelo não for válido, retorna a view com os dados do departamento.
        }

        // Método auxiliar para verificar se o departamento existe no banco de dados.
        private bool DepartamentoExists(long? id)
        {
            return _context.Departamentos.Any(e => e.DepartamentoID == id);  ///Verifica se existe um departamento com o ID fornecido.
        }

        // GET: Departamento/Details/5
        /// Este método lida com a requisição HTTP GET para exibir os detalhes de um departamento específico.
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)  ///Verifica se o ID é nulo, retorna NotFound caso seja.
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);  ///Busca o departamento pelo ID.

            if (departamento == null)  ///Se o departamento não for encontrado, retorna NotFound.
            {
                return NotFound();
            }
            return View(departamento);  ///Retorna a view com os detalhes do departamento.
        }

        // GET: Departamento/Delete/5
        /// Este método lida com a requisição HTTP GET para exibir a confirmação antes de excluir um departamento.
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)  ///Verifica se o ID é nulo, retorna NotFound caso seja.
            {
                return NotFound();
            }

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);  ///Busca o departamento pelo ID.

            if (departamento == null)  ///Se o departamento não for encontrado, retorna NotFound.
            {
                return NotFound();
            }
            return View(departamento);  ///Retorna a view de confirmação de exclusão.
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]  ///Indica que este método é uma ação POST para deletar.
        [ValidateAntiForgeryToken]  ///Protege contra CSRF.
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);  ///Busca o departamento pelo ID.

            _context.Departamentos.Remove(departamento);  ///Remove o departamento do contexto do banco de dados.
            await _context.SaveChangesAsync();  ///Salva as mudanças no banco de dados de forma assíncrona.
            return RedirectToAction(nameof(Index));  ///Redireciona para a ação Index após a exclusão do departamento.
        }
    }

}
