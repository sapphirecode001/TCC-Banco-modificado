using MySql.Data.MySqlClient;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;

namespace Site_SmartComfort.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string _conexaoMySQL;

        public ProdutoRepository(IConfiguration conf)
        {
            // Obtenha a string de conexão como uma string, não como uma instância de MySqlConnection
            _conexaoMySQL = conf.GetConnectionString("ConexaoMySQL");
        }

        public void AtualizarProduto(Produto produto)
        {
            throw new NotImplementedException();
        }

        public void CadastrarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("Insert into tbProdutoAutomacao  (CodBar, NomePro, PrecoPro, QtdEstoquePro, GarantiaPro, MarcaPro," +
                    " ModeloPro, PesoPro, AlturaPro, LarguraPro, ComprimentoPro, ImgUrlPro, IdFunc, IdCategoria, IdVoltagem) " +
                    "values (@CodBar, @NomePro, @PrecoPro, @QtdEstoquePro, @GarantiaPro, @MarcaPro, @ModeloPro, @PesoPro, @AlturaPro, " +
                    "@LarguraPro, @ComprimentoPro, @ImgUrlPro, @IdFunc, @IdCategoria, @IdVoltagem)", conexao);

                cmd.Parameters.Add("@nomePro", MySqlDbType.VarChar).Value = produto.NomePro;
                cmd.Parameters.Add("@PrecoPro", MySqlDbType.Decimal).Value = produto.PrecoPro;
                cmd.Parameters.Add("@QtdEstoquePro", MySqlDbType.Int64).Value = produto.QtdEstoquePro;
                cmd.Parameters.Add("@GarantiaPro", MySqlDbType.DateTime).Value = produto.GarantiaPro;
                cmd.Parameters.Add("@ImgUrlPro", MySqlDbType.Decimal).Value = produto.ImgUrlPro;
                cmd.Parameters.Add("@Voltagem", MySqlDbType.Int64).Value = produto.Voltagem;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            throw new NotImplementedException();
        }

        public Produto ObterProduto(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> ObterTodosProdutos()
        {
            throw new NotImplementedException();
        }
    }
}
