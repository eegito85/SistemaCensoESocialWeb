using Mpce.CensoEsocial.Data.Context;
using Mpce.ECensoSocial.Domain.Domain.Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace Mpce.CensoEsocial.Autenticacao.DAO
{
    public class Acesso
    {
        private readonly AppDbContextSPG _context;

        public Acesso(AppDbContextSPG context)
        {
            _context = context;
        }

        public Pessoa Find(string cpf)
        {
            Pessoa pessoa = _context.Pessoa.Where(p=>p.sCpf == cpf).FirstOrDefault();
            return pessoa;            
        }

        public string MontaSenha(string cpf, DateTime dataNascimento)
        {
            string senha = "";
            string parte1 = cpf.Substring(0, 3);
            string parte2 = dataNascimento.Year.ToString();
            senha = String.Concat(parte1, parte2);

            return senha;
        }

        public int RetornaVinculo(string cpf)
        {
            int codigoVinculo = 0;
            int codigoSituacao = 0;
            int iTipo = 0;
            string conexao = "";
            string sQuery = "";

            if (cpf != null || cpf != String.Empty)
            {
                try
                {
                    conexao = "Data Source=pgjsrv008;Integrated Security=False;Initial Catalog=SGP;User ID=usr_sgp;Password=0987654321;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                    sQuery = @"select pv.iCodVinculo AS codigoVinculo, pv.iCodSituacao codigoSiiuacao from tblPessoa p "
                                                       + " inner join tblPessoaVinculo pv ON p.bStatus = 1 and pv.bStatus = 1 and pv.iCodPessoa = p.iCodPessoa "
                                                       + " inner join tblVinculo v ON v.iCodigo = pv.iCodVinculo "
                                                       + " inner join tblSituacaoFuncional sf ON sf.iCodigo = pv.iCodSituacao and sf.iCodigo NOT IN(2, 9, 11, 15, 16) "
                                                       + " where p.sCpf = @cpf ";
                    //Query Gesse
                    //sQuery = @"SELECT DISTINCT PV.[iCodVinculo] AS codigoVinculo, SF.iCodigo AS codigoSiiuacao FROM [SGP].[dbo].[tblPessoa] PES "
                    //              + " INNER JOIN [SGP].[dbo].[tblPessoaVinculo] PV  ON PES.bStatus = 1 AND PV.bStatus = 1 AND GETDATE() between PV.dtInicio and dbo.dataNull(PV.dtFim) AND PV.iCodPessoa = PES.iCodPessoa"
                    //              + " INNER JOIN [SGP].[dbo].[tblVinculo] VINC ON VINC.iCodigo = PV.iCodVinculo "
                    //              + " INNER JOIN [SGP].[dbo].[tblSituacaoFuncional] SF ON SF.iCodigo = PV.iCodSituacao AND SF.iCodigo NOT IN (2, 9, 11, 15, 16) "
                    //              + " AND PES.sCpf = @cpf ";


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
                        codigoVinculo = int.Parse(reader["codigoVinculo"].ToString());
                        codigoSituacao = int.Parse(reader["codigoSiiuacao"].ToString());
                    }

                    sqlConnection1.Close();

                    if (codigoSituacao == 12) { iTipo = 5; }
                    else
                    {
                        switch (codigoVinculo)
                        {
                            case 1:
                                iTipo = 1;
                                break;
                            case 5:
                                iTipo = 1;
                                break;
                            case 7:
                                iTipo = 1;
                                break;
                            case 8:
                                iTipo = 1;
                                break;
                            case 14:
                                iTipo = 1;
                                break;
                            case 4:
                                iTipo = 4;
                                break;
                            case 2:
                                iTipo = 2;
                                break;
                            case 6:
                                iTipo = 6;
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iTipo;
        }

    }
}
