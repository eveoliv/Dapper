using Microsoft.AspNetCore.Mvc;
using WebApplicationDapper.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationDapper.Pages.Produtos
{
    public class AddModel : PageModel
    {
        readonly IProdutosRepository produtosRepository;

        public AddModel(IProdutosRepository produtosRepository)
        {
            this.produtosRepository = produtosRepository;
        }

        [BindProperty]
        public Entities.Produtos Produto { get; set; }

        [TempData]
        public string Message { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var count = produtosRepository.Add(Produto);
                if (count > 0)
                {
                    Message = "Novo produto incluido com sucesso!";
                    return RedirectToPage("/Produtos/Index");
                }
            }
            return Page();
        }
    }
}