export class Cedido {
    constructor(
        public iCodigo: number = null,
        public iCategoria: number = null,
        public sCNPJEmpCed: string = null,
        public sMatriculaTrab: string = null,
        public dtAdmissao: string = null,
        public iTipoRegTrab: number = null,
        public iTipoRegPrev: number = null,
        public iOnusCessReq: number = null
    ) {

    }
}