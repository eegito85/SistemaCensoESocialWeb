using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Mpce.CensoEsocial.Autenticacao.DAO
{
    public class Permissao
    {
        IConfiguration _iconfiguration;

        public Permissao(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        // retorna 1 se for administrador, 0 se não for administrador 
        public int AcessoAdmin(string cpf)
        {
            int acesso = 0;
            string conexao = _iconfiguration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            string sQuery = "";
            string retorno = "";

            if (cpf != null || cpf != String.Empty)
            {
                try
                {
                    sQuery = @"SELECT A.sCPF as cpf, T.sCPF as cpftrab, T.iCodigo as codigo FROM dbo.tblTrabalhador t LEFT JOIN dbo.tblAdmin A ON A.SCPF = T.sCPF WHERE T.sCPF = @cpf ";

                    SqlConnection sqlConnection1 = new SqlConnection(conexao);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    cmd.CommandText = sQuery;
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if(reader["cpf"].ToString() != "")
                        {
                            retorno = "1";
                        }
                        else if(reader["cpftrab"].ToString() != "" && Convert.ToInt32(reader["codigo"]) < 1555) //producao: 1891; homologacao: 1555
                        {
                            retorno = "2";
                        }
                        else if (reader["cpftrab"].ToString() != "" && Convert.ToInt32(reader["codigo"]) >= 1555) //producao: 1891; homologacao: 1555
                        {
                            retorno = "0";
                        }
                    }

                    sqlConnection1.Close();


                    if (retorno == "1")
                    {
                        acesso = 1;
                    }
                    else if(retorno == "2")
                    {
                        acesso = 2;
                    }
                    else if (retorno == "0")
                    {
                        acesso = 0;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return acesso;
        }
    }
}
