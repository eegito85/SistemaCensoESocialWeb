import { DependenteService } from './../dependente/dependente.service';
import { ArquivoService } from './arquivo.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { StorageService } from '../shared/storage.service';
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Arquivo } from '../model/arquivo.model';



@Component({
  selector: 'app-arquivos',
  templateUrl: './arquivos.component.html',
  styleUrls: ['./arquivos.component.css']
})
export class ArquivosComponent implements OnInit {

  user: any;
  @ViewChild('content') modal: any;
  arquvosForm: any;
  nomeArquivo: string
  arquivos: Arquivo[];
  loading: boolean;
  vrstipo: string = 'CPF';
  vrmessage: string;
  vrmessage1: string;
  vrmessage2: string;
  vrmsgstatico: string;
  vrmmsgobs: string;
  vrRes: string;
  vrTipoDependente: string = '0';

  constructor(public modalService: NgbModal, public arquivoService: ArquivoService,
    private sts: StorageService,
    private fb: FormBuilder, public DependenteService: DependenteService) { 
    }

  ngOnInit() {
    this.vrmessage = '';
    this.vrmessage1 = '';
    this.vrmessage2 = '';
    this.vrmsgstatico = '';
    this.vrmmsgobs = ''; 
    this.loading = true;
    this.creatFormGroup();
    this.arquivoService.notifier.subscribe(
      string => {
        this.arquivos = new Array<Arquivo>();
        this.arquivoService.get(this.sts.getCpf()).subscribe(
          response => {
            this.arquivos = response;
            this.loading = false;
            this.vrRes = string.split('+');
            if(this.vrRes[1] )
            this.vrTipoDependente = this.vrRes[1];
            console.log(this.vrRes[0]);
            this.vrmsgstatico = '';
            this.vrmessage = '';
            this.vrmessage1 = '';
            this.vrmmsgobs = '';

            this.arquvosForm.controls['sTipo'].setValue(this.vrRes[0]);
            if(this.vrRes[0] == 'Comprovante de Dependência') {
              this.vrmsgstatico = 'Documentos a serem anexados:';
              this.vrmessage = 'Certidão de casamento';
              this.vrmessage1 = 'Comprovante de rendimentos (caso não exerça atividade remunerada pode ser declaração de próprio punho)';
              this.vrmmsgobs = 'Obs: escanear todos os documentos em um só arquivo pdf';

            }
            else if(this.vrRes[1] == '2' && this.vrRes[0] == '') {
              this.vrmsgstatico = 'Documentos a serem anexados:';
              this.vrmessage = 'Declaração de união estável';
              this.vrmessage1 = 'Certidão de nascimento ou documento de identidade do filho';
              this.vrmessage2 = 'Comprovante de rendimentos (caso não exerça atividade remunerada pode ser declaração de próprio punho)';
              this.vrmmsgobs = 'Obs: escanear todos os documentos em um só arquivo pdf';
            }
            else if(this.vrRes[1] == '3' && this.vrRes[0] == '') {
              this.vrmsgstatico = 'Documentos a serem anexados:';
              this.vrmessage = 'Certidão de casamento ou documento de união estável do servidor/membro com o genitor ou a genitora do(a) filho(a) ou enteado(a)';
              this.vrmessage1 = 'Certidão de nascimento ou documento de identidade do(a) filho(a) ou enteado(a)';
              this.vrmmsgobs = 'Obs: escanear todos os documentos em um só arquivo pdf';
            }
            else if(this.vrRes[1] == '4' && this.vrRes[0] == '') {
              this.vrmsgstatico = 'Documentos a serem anexados:';
              this.vrmessage = 'Certidão de casamento ou documento de união estável do servidor/membro com o genitor ou a genitora do(a) filho(a) ou enteado(a)';
              this.vrmessage1 = 'Certidão de nascimento ou documento de identidade do(a) filho(a) ou enteado(a)';
              this.vrmessage2 = 'Comprovante de matrícula';
              this.vrmmsgobs = 'Obs: escanear todos os documentos em um só arquivo pdf';
            }
            else if(this.vrRes[1] == '6' && this.vrRes[0] == '') {
              this.vrmsgstatico = 'Documentos a serem anexados:';
              this.vrmessage = 'Comprovante de identidade';
              this.vrmessage1 = 'Guarda judicial';
              this.vrmmsgobs = 'Obs: escanear todos os documentos em um só arquivo pdf';
            }
            else if(this.vrRes[1] == '7' && this.vrRes[0] == '') {
              this.vrmsgstatico = 'Documentos a serem anexados:';
              this.vrmessage = 'Comprovante de identidade';
              this.vrmessage1 = 'Guarda judicial';
              this.vrmessage2 = 'Comprovante de matrícula';
              this.vrmmsgobs = 'Obs: escanear todos os documentos em um só arquivo pdf';
            }
            else if(this.vrRes[1] == '9' && this.vrRes[0] == '') {
              this.vrmsgstatico = 'Documentos a serem anexados:';
              this.vrmessage = 'Comprovante da relação de parentesco';
              this.vrmessage1 = 'Comprovante de rendimentos (caso não exerça atividade remunerada pode ser declaração de próprio punho)';
              this.vrmmsgobs = 'Obs: escanear todos os documentos em um só arquivo pdf';
            }
            else if(this.vrRes[1] == '10' && this.vrRes[0] == '') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'Guarda judicial';
            }
            else if(this.vrRes[1] == '11' && this.vrRes[0] == '') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'Termo tutela ou curatela';
            }
            else if(this.vrRes[1] == '99' && this.vrRes[0] == '') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'Comprovante de dependência';
            }
            else if(this.vrRes[0] == 'CPF do Dependente') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'CPF do Dependente';
            }
            else if(this.vrRes[0] == 'Certidão de Casamento / Comprovação de Divórcio') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'Certidão de Casamento / Comprovação de Divórcio';
            }
            else if(this.vrRes[0] == 'CPF') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'CPF';
            }
            else if(this.vrRes[0] == 'NIS/PIS/PASEP') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'NIS/PIS/PASEP';
            }
            else if(this.vrRes[0] == 'Certificado/Declaração Escolar') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'Certificado/Declaração Escolar';
            }
            else if(this.vrRes[0] == 'CTPS') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'CTPS';
            }
            else if(this.vrRes[0] == 'RG') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'RG';
            }
            else if(this.vrRes[0] == 'CNH') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'CNH';
            }
            else if(this.vrRes[0] == 'Comprovante de Residência') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'Comprovante de Residência';
            }
            else if(this.vrRes[0] == 'CNPJ da Empresa Cedente') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'CNPJ da Empresa Cedente';
            }
            else if(this.vrRes[0] == 'CNPJ da Empresa Cedente') {
              this.vrmsgstatico = 'Documento a ser anexado:';
              this.vrmessage = 'CNPJ da Empresa Cedente';
            }
          },
          error => { console.log(error) }
        );  
        this.openModal();
      },
      error => { console.log(error) }
    );

   
  }

  recarregar() {
    this.loading = true;
    this.arquivos = new Array<Arquivo>();
    this.arquivoService.get(this.sts.getCpf()).subscribe(
      response => {
        this.arquivos = response;
        this.loading = false;
      },
      error => { console.log(error) }
    );  
  }

  openModal() {
    this.modalService.open(this.modal, { centered: true, size: 'lg' });
  }

  onSubmit() {
    this.loading = true;
    this.user = this.sts.getUsuarioLogado();
    this.arquvosForm.value.sCPF = this.sts.getCpf()
    this.arquivoService.insert(this.arquvosForm.value)
      .subscribe((resp) => {
        this.loading = false;
        if(resp == 1) {
          if(this.arquvosForm.value.sTipo == 'CPF do Dependente') {
            this.DependenteService.arquivoEmitter.emit(this.arquvosForm.value.sTipo);
          }
          if(this.arquvosForm.value.sTipo == 'Comprovante de Dependência') {
            console.log('Tipo: '+this.arquvosForm.value.sTipo);
            this.DependenteService.arquivoEmitter.emit('stDirrf');
          }
          alert('Dados salvo com sucesso !');
          this.recarregar();
        }
        else {
          alert('Falha ao anexar o arquivo');
        }
      },
        error => {
          this.loading = false;
          alert('Falha ao salvar dados');
          console.log(error);
        }
      )
  }

  creatFormGroup() {
    this.arquvosForm = this.fb.group({
      iCodigo: 0,
      sTipo: ['', [Validators.required]],
      sCPF: this.sts.getCpf(),
      sArquivo: ['', [Validators.required]],
      sNomeArquivo: ['', [Validators.required]]
    })
  }


  onFileChange(event) {
    let reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      let extensao = file.name.split('.').pop();
      if(extensao == 'pdf' || extensao == 'PDF') {
        reader.readAsDataURL(file);
        reader.onload = () => {
          this.arquvosForm.patchValue({
            sArquivo: reader.result,
            sNomeArquivo: file.name
          });
          //console.log(this.arquvosForm.value)
        };
      }
      else {
        alert('Arquivo inválido, anexar somente PDF');
      }
    }
  }

  baixar(arquivo: Arquivo) {
    this.arquivoService.baixar(arquivo.iCodigo,arquivo.sNomeArquivo)
    .subscribe( resp => {
      let extensao = arquivo.sNomeArquivo.split('.').pop();
      let tipo;
      //console.log(extensao[0]);
      if(extensao == 'pdf' || extensao == 'PDF') {
        tipo = "application/pdf";
      }
      else if(extensao == 'jpg') {
        tipo = "image/jpg";
      }
      else if(extensao == 'png') {
        tipo = "image/png";
      }
      else if(extensao == 'gif') {
        tipo = "image/gif";
      }
      let file = new Blob([resp],{type: tipo});
      let fileURL = URL.createObjectURL(file);
      window.open(fileURL, '_blank');

      //alert('Dados salvo com sucesso !');
    },
      error => {
        alert('Falha ao fazer download');
        console.log(error);
      }
    )
  }

  excluirArquivo(id: number) {
    if(confirm('Deseja realmente excluir esse documento?')) {
      this.loading = true;
      this.arquivoService.excluirArquivo(id)
      .subscribe(
        resp => {
          this.loading = false;
          alert('Documento excluido com sucesso!');
          this.recarregar();
        },
        error => {
          this.loading = false;
          alert('Falha ao excluir o documento');
          console.log(error);
        }
      )
    }
  }

  isValid(filtro: string): boolean {
    return this.arquvosForm.controls[filtro].valid && (this.arquvosForm.controls[filtro].dirty || this.arquvosForm.controls[filtro].touched)
  }

  isInvalid(filtro: string): boolean {
    return this.arquvosForm.controls[filtro].invalid && (this.arquvosForm.controls[filtro].dirty || this.arquvosForm.controls[filtro].touched)
  }

}
