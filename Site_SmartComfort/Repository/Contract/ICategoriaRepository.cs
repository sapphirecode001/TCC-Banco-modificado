using Site_SmartComfort.Models;

namespace Site_SmartComfort.Repository.Contract
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> ObterTodosCategorias();
        Produto ObterCategoria(int Id);

        void AtualizarCategoria(Categoria categoria);

        void CadastrarCategoria(Categoria categoria);
        void Excluir(int Id);

    }
}
