using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mpce.ECensoSocial.Domain.Domain.Entities
{
    public class Trabalhador
    {
      
        [Key]
        public int iCodigo { get; set; }
        public int iTipo { get; set; }
        public String sCPF { get; set; }
        public String sNisPisPasep { get; set; }
        public String sNome { get; set; }
        public int iSexo { get; set; }
        public int iRacaCor { get; set; }
        public int iEstadoCivil { get; set; }
        public int iGrauInstrucao { get; set; }
        public int iPrimeiroEmprego { get; set; }
        public String sCodiNomeTravTrans { get; set; }
        public DateTime? dtDataNasc { get; set; }
        public int iCodMunicipioNasc { get; set; }
        public String sUfNasc { get; set; }
        public int iPaisNasc { get; set; }
        public int iNacionalidade { get; set; }
        public String sNomeMae { get; set; }
        public String sNomePai { get; set; }
        public String sNumCTPS { get; set; }
        public String sNumSerieCTPS { get; set; }
        public String sUfCTPS { get; set; }
        public String sNumRG { get; set; }
        public String sEmissaoRG { get; set; }
        public DateTime? dtExpedRG { get; set; }
        public String sNumRegOC { get; set; }
        public String sEmissaoOC { get; set; }
        public DateTime? dtExpedOC { get; set; }
        public DateTime? dtValidadeOC { get; set; }
        public String sNumCNH { get; set; }
        public DateTime? dtExpedCNH { get; set; }
        public String sUfCNH { get; set; }
        public DateTime? dtValidadeCNH { get; set; }
        public DateTime? dtPrimeiraHab { get; set; }
        public int? iCatCNH { get; set; }
        public string sTipoLogradouro { get; set; }
        public String sLogradouro { get; set; }
        public String sNumero { get; set; }
        public String sComplemento { get; set; }
        public String sBairro { get; set; }
        public String sCEP { get; set; }
        public int iCodMunicipioRes { get; set; }
        public String sUfRes { get; set; }
        public String sDefFisica { get; set; }
        public String sDefVisual { get; set; }
        public String sDefAuditiva { get; set; }
        public String sDefMental { get; set; }
        public String sDefIntelectual { get; set; }
        public String sRecebeBeneficioPrev { get; set; }
        public String sTelefone1 { get; set; }
        public String sTelefone2 { get; set; }
        public String sEmail { get; set; }
        public String sEmail2 { get; set; }
        public String sDescricao { get; set; }
        public int? iVerificado { get; set; }
        public DateTime? dtCadastro { get; set; }
        public DateTime? dtUltimaAtualizacao { get; set; }
        public  ICollection<Dependente> Dependentes { get; set; }
        public ICollection<Estagiario> Estagiarios { get; set; }
        public ICollection<Cedido> Cedidos { get; set; }
        //public ICollection<TipoLogradouro> TiposLogradouro { get; set; }

    }
}
