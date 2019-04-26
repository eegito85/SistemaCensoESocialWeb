using Mpce.ECensoSocial.Domain.Domain.Entities.Comprovante;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Mpce.CensoEsocial.WebApi.DAO
{
    public class ConsultaDados
    {

        //public string conexao = "Data Source=pgjsrv81;Integrated Security=False;Initial Catalog=db_censo_esocial;User ID=usr_sgp;Password=0987654321;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ComprovanteTrabalhador PegarDadosTrabalhador(int iCodigo, string conexao)
        {
            ComprovanteTrabalhador comprovante = new ComprovanteTrabalhador();


            string sQuery = "";


            if (iCodigo > 0)
            {
                try
                {
                    sQuery = "set dateformat dmy "
                           + "select "
                           + "case "
                           + "	when t.iTipo = 1 or t.iTipo = 2 or t.iTipo = 5 then 'Efetivo' "
                           + "	when t.iTipo = 4 then 'Estagiário' "
                           + "	when t.iTipo = 6 then 'Cedido' "
                           + "end as vinculo, "
                           + "t.sNome as nome, "
                           + "case "
                           + "	when t.iSexo = 1 then 'Masculino' "
                           + "	when t.iSexo = 2 then 'Feminino' "
                           + "end as sexo, "
                           + "case "
                           + "	when t.iEstadoCivil = 1 then 'Solteiro' "
                           + "	when t.iEstadoCivil = 2 then 'Casado' "
                           + "	when t.iEstadoCivil = 3 then 'Divorciado' "
                           + "	when t.iEstadoCivil = 4 then 'Separado' "
                           + "	when t.iEstadoCivil = 5 then 'Viúvo' "
                           + "	when t.iEstadoCivil = 6 then 'União Estável' "
                           + "end as estado_civil, "
                           + "case "
                           + "	when t.iRacaCor = 1 then 'Branca' "
                           + "	when t.iRacaCor = 2 then 'Preta' "
                           + "	when t.iRacaCor = 3 then 'Parda' "
                           + "	when t.iRacaCor = 4 then 'Amarela' "
                           + "	when t.iRacaCor = 5 then 'Indígena' "
                           + "	when t.iRacaCor = 6 then 'Não informado' "
                           + "end as raca_cor, "
                           + "t.sCPF as cpf, "
                           + "t.sNisPisPasep as nis_pis_pasep, "
                           + "case "
                           + "	when t.iGrauInstrucao = 1 then 'Analfabeto, inclusive o que, embora tenha recebido instrução, não se alfabetizou' "
                           + "	when t.iGrauInstrucao = 2 then 'Até o 5º ano incompleto do Ensino Fundamental (antiga 4ª série) ou que se tenha alfabetizado sem ter frequentado escola regular' "
                           + "	when t.iGrauInstrucao = 3 then '5º ano completo do Ensino Fundamental' "
                           + "	when t.iGrauInstrucao = 4 then 'Do 6º ao 9º ano do Ensino Fundamental incompleto (antiga 5ª a 8ª série)' "
                           + "	when t.iGrauInstrucao = 5 then 'Ensino Fundamental Completo' "
                           + "	when t.iGrauInstrucao = 6 then 'Ensino Médio incompleto' "
                           + "	when t.iGrauInstrucao = 7 then 'Ensino Médio completo' "
                           + "	when t.iGrauInstrucao = 8 then 'Educação Superior incompleta' "
                           + "	when t.iGrauInstrucao = 9 then 'Educação Superior completa' "
                           + "	when t.iGrauInstrucao = 10 then 'Pós-Graduação completa' "
                           + "	when t.iGrauInstrucao = 11 then 'Mestrado completo' "
                           + "	when t.iGrauInstrucao = 12 then 'Doutorado completo.' "
                           + "end as grau_instrucao, "
                           + "case "
                           + "	when t.iPrimeiroEmprego = 1 then 'Sim' "
                           + "	when t.iPrimeiroEmprego = 2 then 'Não' "
                           + "end as prim_emp, "
                           + "case "
                           + "	when t.sCodiNomeTravTrans is not null then t.sCodiNomeTravTrans else '' "
                           + "end as nome_social_trans, "
                           + "case "
                           + "	when t.dtDataNasc is not null then CONVERT(char,t.dtDataNasc,103) else '' "
                           + "end as data_nasc, "
                           + "t.sUfNasc as uf_nasc, "
                           + "(select m.sCidade from tblMunicipio m where m.iCodigo = t.iCodMunicipioNasc) as naturalidade, "
                           + "(select p.sPais from tblPais p where p.iCodigo = t.iPaisNasc) as pais_nasc, "
                           + "(select p.sPais from tblPais p where p.iCodigo = t.iNacionalidade) as nacionalidade, "
                           + "t.sNomeMae as nome_mae, "
                           + "t.sNomePai as nome_pai, "
                           + "t.sNumCTPS as num_ctps, "
                           + "t.sNumSerieCTPS as serie_ctps, "
                           + "t.sUfCTPS as uf_ctps, "
                           + "t.sNumRG as rg, "
                           + "t.sEmissaoRG as org_emissor, "
                           + "case "
                           + "	when t.dtExpedRG is not null then CONVERT(char,t.dtExpedRG,103) else '' "
                           + "end as data_exped_rg, "
                           + "t.sNumCNH as num_cnh, "
                           + "case "
                           + "	when t.dtExpedCNH is not null then CONVERT(char,t.dtExpedCNH,103) else '' "
                           + "end as data_exped_cnh, "
                           + "t.sUfCNH as uf_cnh, "
                           + "case "
                           + "	when t.dtValidadeCNH is not null then CONVERT(char,t.dtValidadeCNH,103) else '' "
                           + "end as data_validade_cnh, "
                           + "case "
                           + "	when t.iCatCNH = 1 then 'A' "
                           + "	when t.iCatCNH = 2 then 'B' "
                           + "	when t.iCatCNH = 3 then 'C' "
                           + "	when t.iCatCNH = 4 then 'D' "
                           + "	when t.iCatCNH = 5 then 'E' "
                           + "	when t.iCatCNH = 6 then 'AB' "
                           + "	when t.iCatCNH = 7 then 'AC' "
                           + "	when t.iCatCNH = 8 then 'AD' "
                           + "	when t.iCatCNH = 9 then 'AE' "
                           + "end as cat_cnh, "
                           + "case "
                           + "	when t.dtPrimeiraHab is not null then CONVERT(char,t.dtPrimeiraHab,103) else '' "
                           + "end as data_primeira_cnh,"
                           + "case "
                           + "	when t.dtExpedOC is not null then CONVERT(char,t.dtExpedOC,103) else '' "
                           + "end as data_exped_oc, "
                           + "case "
                           + "	when t.dtValidadeOC is not null then CONVERT(char,t.dtValidadeOC,103) else '' "
                           + "end as data_validade_oc, "
                           + "t.sNumRegOC as num_reg_oc, "
                           + "t.sEmissaoOC as org_emissor_oc, "
                           + "(select sDescricao from tblTipoLogradouro where sCodigo = t.sTipoLogradouro) as tipo_logradouro, "
                           + "t.sLogradouro as logradouro, "
                           + "t.sNumero as numero, "
                           + "t.sComplemento as complemento, "
                           + "t.sBairro as bairro, "
                           + "t.sCEP as cep, "
                           + "t.sUfRes as uf_res, "
                           + "(select m.sCidade from tblMunicipio m where m.iCodigo = t.iCodMunicipioRes) as cidade_res, "
                           + "case  "
                           + "	when t.sDefFisica = 'S' then 'Sim' "
                           + "	when t.sDefFisica = 'N' then 'Não' "
                           + "end as def_fisica, "
                           + "case "
                           + "	when t.sDefVisual = 'S' then 'Sim' "
                           + "	when t.sDefVisual = 'N' then 'Não' "
                           + "end as def_visual, "
                           + "case "
                           + "	when t.sDefAuditiva = 'S' then 'Sim' "
                           + "	when t.sDefAuditiva = 'N' then 'Não' "
                           + "end as def_auditiva, "
                           + "case "
                           + "	when t.sDefMental = 'S' then 'Sim' "
                           + "	when t.sDefMental = 'N' then 'Não' "
                           + "end as def_mental, "
                           + "case "
                           + "	when t.sDefIntelectual = 'S' then 'Sim' "
                           + "	when t.sDefIntelectual = 'N' then 'Não' "
                           + "end as def_intelectual, "
                           + "case "
                           + "	when t.sRecebeBeneficioPrev = 'S' then 'Sim' "
                           + "	when t.sRecebeBeneficioPrev = 'N' then 'Não' "
                           + "end as rec_beneficio_prev, "
                           + "t.sTelefone1 as telefone, "
                           + "t.sTelefone2 as celular, "
                           + "t.sEmail as email, "
                           + "t.sEmail2 as email_aut "
                           + "from tblTrabalhador t "
                           + "where t.iCodigo = @iCodigo";

                    SqlConnection sqlConnection1 = new SqlConnection(conexao);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    cmd.CommandText = sQuery;
                    cmd.Parameters.AddWithValue("@iCodigo", iCodigo);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comprovante.iCodComprovante = 1;
                        comprovante.iCodigo = iCodigo;
                        comprovante.sVinculo = reader["vinculo"].ToString();
                        comprovante.sNome = reader["nome"].ToString();
                        comprovante.sSexo = reader["sexo"].ToString();
                        comprovante.sEstadoCivil = reader["estado_civil"].ToString();
                        comprovante.sRacaCor = reader["raca_cor"].ToString();
                        comprovante.sCPF = reader["cpf"].ToString();
                        comprovante.sNisPisPasep = reader["nis_pis_pasep"].ToString();
                        comprovante.sGrauInstrucao = reader["grau_instrucao"].ToString();
                        comprovante.sPrimeiroEmprego = reader["prim_emp"].ToString();
                        comprovante.sNomeTravTrans = reader["nome_social_trans"].ToString();
                        comprovante.dtDataNasc = (reader["data_nasc"].ToString());
                        comprovante.sUfNasc = reader["uf_nasc"].ToString();
                        comprovante.sMunicipioNasc = reader["naturalidade"].ToString();
                        comprovante.sPaisNasc = reader["pais_nasc"].ToString();
                        comprovante.sNacionalidade = reader["nacionalidade"].ToString();
                        comprovante.sNomeMae = reader["nome_mae"].ToString();
                        comprovante.sNomePai = reader["nome_pai"].ToString();
                        comprovante.sNumCTPS = reader["num_ctps"].ToString();
                        comprovante.sNumSerieCTPS = reader["serie_ctps"].ToString();
                        comprovante.sUfCTPS = reader["uf_ctps"].ToString();
                        comprovante.sNumRG = reader["rg"].ToString();
                        comprovante.sEmissaoRG = reader["org_emissor"].ToString();
                        comprovante.dtExpedRG = (reader["data_exped_rg"].ToString());
                        comprovante.sNumCNH = reader["num_cnh"].ToString();
                        comprovante.dtExpedCNH = (reader["data_exped_cnh"].ToString());
                        comprovante.sUfCNH = reader["uf_cnh"].ToString();
                        comprovante.dtValidadeCNH = (reader["data_validade_cnh"].ToString());
                        comprovante.sCatCNH = reader["cat_cnh"].ToString();
                        comprovante.dtPrimeiraHab = (reader["data_primeira_cnh"].ToString());
                        comprovante.dtExpedOC = (reader["data_exped_oc"].ToString());
                        comprovante.dtValidadeOC = (reader["data_validade_oc"].ToString());
                        comprovante.sNumRegOC = reader["num_reg_oc"].ToString();
                        comprovante.sEmissaoOC = reader["org_emissor_oc"].ToString();
                        comprovante.sTipoLogradouro = reader["tipo_logradouro"].ToString();
                        comprovante.sLogradouro = reader["logradouro"].ToString();
                        comprovante.sNumero = reader["numero"].ToString();
                        comprovante.sComplemento = reader["complemento"].ToString();
                        comprovante.sBairro = reader["bairro"].ToString();
                        comprovante.sCEP = reader["cep"].ToString();
                        comprovante.sUfRes = reader["uf_res"].ToString();
                        comprovante.sMunicipioRes = reader["cidade_res"].ToString();
                        comprovante.sDefFisica = reader["def_fisica"].ToString();
                        comprovante.sDefVisual = reader["def_visual"].ToString();
                        comprovante.sDefAuditiva = reader["def_auditiva"].ToString();
                        comprovante.sDefMental = reader["def_mental"].ToString();
                        comprovante.sDefIntelectual = reader["def_intelectual"].ToString();
                        comprovante.sRecebeBeneficioPrev = reader["rec_beneficio_prev"].ToString();
                        comprovante.sTelefone1 = reader["telefone"].ToString();
                        comprovante.sTelefone2 = reader["celular"].ToString();
                        comprovante.sEmail = reader["email"].ToString();
                        comprovante.sEmail2 = reader["email_aut"].ToString();
                    }
                    sqlConnection1.Close();
                }
                catch (Exception ex) { throw ex; }
            }

            return comprovante;
        }

        public ComprovanteCedido PegarDadosCedido(int iCodigo, string conexao)
        {
            ComprovanteCedido comprovante = new ComprovanteCedido();


            string sQuery = "";


            if (iCodigo > 0)
            {
                try
                {
                    sQuery = "select "
                           + " sCNPJEmpCed as cnpj_emp_ced, "
                           + " sMatriculaTrab as matricula, "
                           + " case "
                           + " 	when dtAdmissao is not null then CONVERT(char,dtAdmissao,103) else '' "
                           + " end as data_admissao, "
                           + " case "
                           + " 	when iTipoRegTrab = 1 then 'CLT(celetista)' "
                           + " 	when iTipoRegTrab = 2 then 'ESTATUTÁRIO' "
                           + " end as tipo_reg_trab, "
                           + " case "
                           + " 	when iTipoRegPrev = 1 then 'RGPS' "
                           + " 	when iTipoRegPrev = 2 then 'RPPS' "
                           + " 	when iTipoRegPrev = 3 then 'Regime de Previdência Social no Exterior' "
                           + " end as tipo_reg_prev, "
                           + " case "
                           + " 	when iOnusCessReq = 1 then 'RGPS' "
                           + " 	when iOnusCessReq = 2 then 'RPPS' "
                           + " 	when iOnusCessReq = 3 then 'Regime de Previdência Social no Exterior' "
                           + " end as onus_cess_req, "
                           + " case "
                           + " 	when iCategoria = 101 then 'Empregado Geral inclusive emprego público p/CLT' "
                           + " 	when iCategoria = 301 then 'Servidor Público Titular de Cargo Efetivo' "
                           + " 	when iCategoria = 302 then 'Servidor Ocupante de cargo exclusivo em comissão' "
                           + " 	when iCategoria = 306 then 'Servidor Público Temporário, sujeito a regime adm. especial' "
                           + " end as categoria "
                           + " from tblCedido "
                           + " where iCodTrabalhador = @iCodigo";

                    SqlConnection sqlConnection1 = new SqlConnection(conexao);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    cmd.CommandText = sQuery;
                    cmd.Parameters.AddWithValue("@iCodigo", iCodigo);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comprovante.sCNPJEmpCed = reader["cnpj_emp_ced"].ToString();
                        comprovante.sMatriculaTrab = reader["matricula"].ToString();
                        comprovante.dtAdmissao = (reader["data_admissao"].ToString());
                        comprovante.sTipoRegTrab = reader["tipo_reg_trab"].ToString();
                        comprovante.sTipoRegPrev = reader["tipo_reg_prev"].ToString();
                        comprovante.sOnusCessReq = reader["onus_cess_req"].ToString();
                        comprovante.sCategoria = reader["categoria"].ToString();
                    }
                    sqlConnection1.Close();
                }
                catch { }
            }

            return comprovante;
        }

        public ComprovanteEstagiario PegarDadosEstagiario(int iCodigo, string conexao)
        {
            ComprovanteEstagiario comprovante = new ComprovanteEstagiario();


            string sQuery = "";


            if (iCodigo > 0)
            {
                try
                {
                    sQuery = " select                                            "
                           + " case                                              "
                           + " 	when iNaturezaEstagio = 1 then 'Obrigatório'     "
                           + " 	when iNaturezaEstagio = 2 then 'Não Obrigatório' "
                           + " end as natureza_estagio,                          "
                           + " sCNPJInst as cnpj_inst,                           "
                           + " sNomeSupervisor as nome_supervisor,               "
                           + " sRazaoSocialInst as rz_social_inst,               "
                           + " sLogradouroInst as logradouro_inst,               "
                           + " sNumInst as numero_inst,                          "
                           + " sBairroInst as bairro_inst,                       "
                           + " (select m.sCidade from tblMunicipio m where m.iCodigo = iCodCidadeInst) as cidade_inst,  "
                           + " sCepInst as cep_inst,                                     "
                           + " sUfInst as uf_inst,                                       "
                           + " case                                                      "
                           + " 	when iAreaAtuacao = 1 then 'Direito'                     "
                           + " 	when iAreaAtuacao = 2 then 'Administração'               "
                           + " 	when iAreaAtuacao = 3 then 'Economia'                    "
                           + " 	when iAreaAtuacao = 4 then 'Ciências Contábeis'          "
                           + " 	when iAreaAtuacao = 5 then 'Comunicação Social'          "
                           + " 	when iAreaAtuacao = 6 then 'Serviço Social'              "
                           + " 	when iAreaAtuacao = 7 then 'Psicologia'                  "
                           + " 	when iAreaAtuacao = 8 then 'Engenharia'                  "
                           + " 	when iAreaAtuacao = 9 then 'Arquitetura'                 "
                           + " 	when iAreaAtuacao = 10 then 'Ciências da Computação'     "
                           + " 	when iAreaAtuacao = 11 then 'Sistemas de Informação'     "
                           + " 	when iAreaAtuacao = 12 then 'Biblioteconomia'            "
                           + " 	when iAreaAtuacao = 13 then 'Ciências Atuariais'         "
                           + " 	when iAreaAtuacao = 14 then 'Estatística e Edificações'  "
                           + " end as area_atuacao "
                           + " from tblEstagiario "
                           + " where iCodTrabalhador = @iCodigo";

                    SqlConnection sqlConnection1 = new SqlConnection(conexao);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    cmd.CommandText = sQuery;
                    cmd.Parameters.AddWithValue("@iCodigo", iCodigo);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        comprovante.sNaturezaEstagio = reader["natureza_estagio"].ToString();
                        comprovante.sCNPJInst = reader["cnpj_inst"].ToString();
                        comprovante.sNomeSupervisor = reader["nome_supervisor"].ToString();
                        comprovante.sRazaoSocialInst = reader["rz_social_inst"].ToString();
                        comprovante.sLogradouroInst = reader["logradouro_inst"].ToString();
                        comprovante.sNumInst = reader["numero_inst"].ToString();
                        comprovante.sBairroInst = reader["bairro_inst"].ToString();
                        comprovante.sCidadeInst = reader["cidade_inst"].ToString();
                        comprovante.sCepInst = reader["cep_inst"].ToString();
                        comprovante.sUfInst = reader["uf_inst"].ToString();
                        comprovante.sAreaAtuacao = reader["area_atuacao"].ToString();
                    }
                    sqlConnection1.Close();
                }
                catch { }
            }

            return comprovante;
        }


        public List<ComprovanteDependente> PegarDadosDependentes(int iCodigo, string conexao)
        {
            List<ComprovanteDependente> dependentes = new List<ComprovanteDependente>();
            
            string sQuery = "";

            if (iCodigo > 0)
            {
                try
                {
                    sQuery = "set dateformat dmy "
                           + "Select "
                           + "Case "
                           + "when iTipoDependente = 1 then 'Cônjuge' "
                           + "when iTipoDependente = 2 then 'Companheiro(a) com o(a) qual tenha filho ou viva há mais de 5 (cinco) anos ou possua Declaração de União Estável' "
                           + "when iTipoDependente = 3 then 'Filho(a) ou enteado(a)' "
                           + "when iTipoDependente = 4 then 'Filho(a) ou enteado(a), universitário(a) ou cursando escola técnica de 2º grau' "
                           + "when iTipoDependente = 6 then 'Irmão(ã), neto(a) ou bisneto(a) sem arrimo dos pais, do(a) qual detenha a guarda judicial' "
                           + "when iTipoDependente = 7 then 'Irmão(ã), neto(a) ou bisneto(a) sem arrimo dos pais, universitário(a) ou cursando escola técnica de 2° grau, do(a) qual detenha a guarda judicial' "
                           + "when iTipoDependente = 9 then 'Pais, avós e bisavós; 10: Menor pobre do qual detenha a guarda judicial' "
                           + "when iTipoDependente = 11 then 'A pessoa absolutamente incapaz, da qual seja tutor ou curador' "
                           + "when iTipoDependente = 12 then 'Ex-cônjuge' "
                           + "when iTipoDependente = 99 then 'Agregado/Outros' "
                           + "End As tipo, "
                           + "sNomeDependente as nome, "
                           + "CONVERT(char,dtNasc,103) as data, "
                           + "sCPFDependente as cpf, "
                           + "Case "
                           + "when sDepTrabIRRF = 'S' then 'Sim' "
                           + "when sDepTrabIRRF = 'N' then 'Não' "
                           + "End As depIrrf, "
                           + "Case "
                           + "when sDepIncapaFisMentTrab = 'S' then 'Sim' "
                           + "when sDepIncapaFisMentTrab = 'N' then 'Não' "
                           + "End As depIncapaz, "
                           + "Case "
                           + "when iDependentePensao = 1 then 'Sim' "
                           + "when iDependentePensao = 0 then 'Não' "
                           + "End As depPensao, "
                           + "sResponsavel as responsavel, "
                           + "sTelefoneResp as telefone "
                           + "From tblDependente Where iCodTrabalhador = @iCodigo";

                    SqlConnection sqlConnection1 = new SqlConnection(conexao);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    cmd.CommandText = sQuery;
                    cmd.Parameters.AddWithValue("@iCodigo", iCodigo);
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ComprovanteDependente comprovante = new ComprovanteDependente();
                        comprovante.sTipoDependente = reader["tipo"].ToString();
                        comprovante.sNomeDependente = reader["nome"].ToString();
                        comprovante.dtNasc = reader["data"].ToString();
                        comprovante.sCPFDependente = reader["cpf"].ToString();
                        comprovante.sDepTrabIRRF = reader["depIrrf"].ToString();
                        comprovante.sDepIncapaFisMentTrab = reader["depIncapaz"].ToString();
                        comprovante.sDependentePensao = reader["depPensao"].ToString();
                        comprovante.sResponsavel = reader["responsavel"].ToString();
                        comprovante.sTelefoneResp = reader["telefone"].ToString();

                        dependentes.Add(comprovante);
                    }
                    sqlConnection1.Close();
                }
                catch { }
            }

            return dependentes;
        }

    }
}
