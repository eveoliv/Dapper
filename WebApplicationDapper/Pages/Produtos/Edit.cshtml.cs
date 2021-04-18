using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationDapper.Repository;

namespace WebApplicationDapper.Pages.Produtos
{
    public class EditModel : PageModel
    {
        readonly IProdutosRepository produtosRepository;

        public EditModel(IProdutosRepository produtosRepository)
        {
            this.produtosRepository = produtosRepository;
        }

        [BindProperty]
        public Entities.Produtos Produto { get; set; }

        public void OnGet(int id)
        {
            Produto = produtosRepository.Get(id);
        }

        public IActionResult OnPost()
        {
            var produto = Produto;
            if (ModelState.IsValid)
            {
                var count = produtosRepository.Edit(produto);
                if (count > 0)
                {
                    return RedirectToPage("/Produtos/Index");
                }
            }
            return Page();
        }
    }
}