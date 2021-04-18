using System.ComponentModel.DataAnnotations;

namespace WebApplicationDapper.Entities
{
    public class Produtos
    {
        [Key]
        [Display(Name = "Id")]
        public int ProdutoId { get; set; }

        [Display(Name = "Nome do Produto")]
        [StringLength(25, ErrorMessage = "O nome deve ter entre 1 e 100 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Estoque")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior ou igual a 1")]
        public int Estoque { get; set; }

        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
    }
}
