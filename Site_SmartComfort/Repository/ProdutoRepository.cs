using MySql.Data.MySqlClient;
using Site_SmartComfort.Models;
using Site_SmartComfort.Repository.Contract;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("Update tbProdutoAutomacao set CodBar=@CodBar, NomePro=@NomePro, PrecoPro=@PrecoPro, QtdEstoquePro=@QtdEstoquePro, GarantiaPro=@GarantiaPro, ImgUrlPro=@ImgUrlPro, Voltagem=@Voltagem, IdCategoria=@RefCategoria.IdCategoria" +
                    " Where Id=@Id ", conexao);

                cmd.Parameters.Add("@Id", MySqlDbType.Int64).Value = produto.Id;
                cmd.Parameters.Add("@CodBar", MySqlDbType.Decimal).Value = produto.CodBar;
                cmd.Parameters.Add("@nomePro", MySqlDbType.VarChar).Value = produto.NomePro;
                cmd.Parameters.Add("@PrecoPro", MySqlDbType.Decimal).Value = produto.PrecoPro;
                cmd.Parameters.Add("@QtdEstoquePro", MySqlDbType.Int64).Value = produto.QtdEstoquePro;
                cmd.Parameters.Add("@GarantiaPro", MySqlDbType.DateTime).Value = produto.GarantiaPro;
                cmd.Parameters.Add("@ImgUrlPro", MySqlDbType.VarChar).Value = produto.ImgUrlPro;
                cmd.Parameters.Add("@Voltagem", MySqlDbType.Int64).Value = produto.Voltagem;
                cmd.Parameters.Add("@IdCategoria", MySqlDbType.VarChar).Value = produto.RefCategoria.IdCategoria;
                cmd.ExecuteNonQuery();
                conexao.Close();
              
            }
        }

        public void CadastrarProduto(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into tbProdutoAutomacao  (CodBar, NomePro, PrecoPro, QtdEstoquePro, GarantiaPro, ImgUrlPro, Voltagem, IdCategoria) " +
                    "values (@CodBar, @NomePro, @PrecoPro, @QtdEstoquePro, @GarantiaPro, @ImgUrlPro, @Voltagem, @IdCategoria)", conexao);

               // cmd.Parameters.Add("@Id", MySqlDbType.Int64).Value = produto.Id;
                cmd.Parameters.Add("@CodBar", MySqlDbType.Decimal).Value = produto.CodBar;
                cmd.Parameters.Add("@nomePro", MySqlDbType.VarChar).Value = produto.NomePro;
                cmd.Parameters.Add("@PrecoPro", MySqlDbType.Decimal).Value = produto.PrecoPro;
                cmd.Parameters.Add("@QtdEstoquePro", MySqlDbType.Int64).Value = produto.QtdEstoquePro;
                cmd.Parameters.Add("@GarantiaPro", MySqlDbType.DateTime).Value = produto.GarantiaPro;
                cmd.Parameters.Add("@ImgUrlPro", MySqlDbType.VarChar).Value = produto.ImgUrlPro;
                cmd.Parameters.Add("@Voltagem", MySqlDbType.VarChar).Value = produto.Voltagem;
                cmd.Parameters.Add("@IdCategoria", MySqlDbType.Int64).Value = produto.RefCategoria.IdCategoria;

                cmd.ExecuteNonQuery();
                conexao.Close();
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("delete from tbProdutoAutomacao where Id=@Id", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);
               int i = cmd.ExecuteNonQuery();
                conexao.Close();

            }
        }

        public Produto ObterProduto(int Id)
        {
            List<Produto> Prolist = new List<Produto>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbProdutoAutomacao as t1 inner join tbCategoria as t2 on t1.Id = t2.IdCategoria where Id=@Id", conexao);
                cmd.Parameters.AddWithValue("@Id", Id);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;

                Produto produto = new Produto();
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read()) {
                    produto.Id = Convert.ToInt32(dr["Id"]);
                    produto.CodBar = Convert.ToInt64(dr["CodBar"]);
                    produto.NomePro = (string)(dr["NomeCategoria"]);
                    produto.PrecoPro = Convert.ToDecimal(dr["PrecoPro"]);
                    produto.QtdEstoquePro = Convert.ToInt32(dr["QtdEstoquePro"]);
                    produto.GarantiaPro = Convert.ToDateTime(dr["GarantiaPro"]);
                    produto.Voltagem = (string)(dr["Voltagem"]);
                    produto.ImgUrlPro = (string)(dr["ImgUrlPro"]);
                            produto.RefCategoria = new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(dr["Id"]),
                                NomeCategoria = (string)(dr["NomeCategoria"]),
                            };
                }
                return produto;
            }
        }

        public IEnumerable<Produto> ObterTodosProdutos()
        {
            List<Produto> Prolist = new List<Produto>();
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbProdutoAutomacao as t1 inner join tbCategoria as t2 on t1.Id = t2.IdCategoria;", conexao);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    Prolist.Add(
                        new Produto
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            CodBar = Convert.ToInt64(dr["CodBar"]),
                            NomePro = (string)(dr["NomeCategoria"]),
                            PrecoPro = Convert.ToDecimal(dr["PrecoPro"]),
                            QtdEstoquePro = Convert.ToInt32(dr["QtdEstoquePro"]),
                            GarantiaPro = Convert.ToDateTime(dr["GarantiaPro"]),
                            Voltagem = (string)(dr["Voltagem"]),
                            ImgUrlPro = (string)(dr["ImgUrlPro"]),
                            RefCategoria = new Categoria()
                            {
                                IdCategoria = Convert.ToInt32(dr["IdCategoria"]), 
                                NomeCategoria = (string)(dr["NomeCategoria"]),
                            }
                        }

                        );
                }
                return Prolist;
            }
    }
    }
}
