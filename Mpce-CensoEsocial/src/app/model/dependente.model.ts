export class Dependente {
    constructor(
        public iCodigo: number = null,
        public iCodTrabalhador: number = null,
        public sNomeDependente: string = null,
        public iTipoDependente: number = null, 
        public dtNasc: string = null,
        public sCPFDependente: string = null,
        public sDepTrabIRRF: string = null,
        public sDepIncapaFisMentTrab: string = null,
        public iDependentePensao: number = null,
        public sResponsavel: string = null,
        public sTelefoneResp: string = null
    ) {

    }
}