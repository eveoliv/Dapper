using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplicationDapper.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplicationDapper.Pages.Produtos
{
    public class IndexModel : PageModel
    {
        readonly IProdutosRepository produtosRepository;

        public IndexModel(IProdutosRepository produtosRepository)
        {
            this.produtosRepository = produtosRepository;
        }

        [BindProperty]
        public List<Entities.Produtos> ListaProdutos { get; set; }

        [BindProperty]
        public Entities.Produtos Produto { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            ListaProdutos = produtosRepository.GetProdutos();
        }

        public IActionResult OnPostDelete(int id)
        {
            if (id > 0)
            {
                var count = produtosRepository.Delete(id);
                if (count > 0)
                {
                    Message = "Produto deletado com sucesso!";
                    return RedirectToPage("/Produtos/Index");
                }
            }
            return Page();
        }
    }
}