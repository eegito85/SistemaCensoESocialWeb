import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
//import { CadastroService } from './../httpclient';
import { LoginService } from './../login/login.service';
import { ArquivoService } from './../arquivos/arquivo.service';
import { Component, OnInit, OnChanges, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from "@angular/forms";
import { Router, ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common'

import { Cidade } from '../model/cidade.model';
import { Municipio } from '../model/municipio.model';
import { TipoLogradouro } from '../model/tipo-logradouro.model';
import { CadastroService } from './cadastro.service';
import { Pais } from '../model/pais.model';
import * as moment from 'moment';
import { $, element } from 'protractor';
import { Trabalhador } from '../model/trabalhador.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrls: ['./cadastro.component.css']
})
export class CadastroComponent implements OnInit {
  tiposLogradouros: any[];
  cidades: Observable<any>;
  municipios: Observable<any>;
  paises: Pais[];
  cadastroForm: FormGroup;
  iCodigo: number;
  trabalhador: Trabalhador = new Trabalhador();
  isAceite: boolean = false;
  loading: boolean;
  vrVinculo: string = '';
  stDependente: boolean = false;
  cdtrabalahdor: any;
  @ViewChild('content') modalConfirm: any;
  @ViewChild('content2') modalConfirmEst: any;
  @ViewChild('content3') modalConfirmCed: any;

  constructor(private cadastroService: CadastroService, private fb: FormBuilder, public datepipe: DatePipe,
    private router: Router, private _avRoute: ActivatedRoute, public arquivoService: ArquivoService, 
  public loginService: LoginService, public modalService: NgbModal) {
  }


  getTiposLogradouro() {
    this.cadastroService.getTiposLogradouro()
      .subscribe(
        response => {
          this.tiposLogradouros = response;
        },
        error => { console.log(error) }

      )
  }

  getCidades() {
    this.cadastroService.getCidades()
      .subscribe((value) => {
        this.cidades = value;
        this.municipios = value;
      });
  }

  getPaises() {
    this.cadastroService.getPaises().subscribe(
      response => {
        this.paises = response;
      }
    )
  }


  ngOnInit() {
    this.loading = true;
    this.getTiposLogradouro();
    //this.getCidadesByUf('Ce');
    //this.getMunicipiosByUf('Ce');
    this.getPaises();

    this.creatFormGroup();

    this.iCodigo = this._avRoute.snapshot.params["iCodTrabalhador"];
    if (this.iCodigo > 0) {
      this.cadastroForm.value.iCodigo = this.iCodigo;
      this.cadastroService.get(this.iCodigo).subscribe(
        response => {
          if(response != null) {
            this.convertDataServidorAngular(response)
            this.cadastroForm.patchValue(response, { onlySelf: true });
            this.trabalhador = this.cadastroForm.value;
            this.getCidadesByUf(this.trabalhador.sUfNasc);
            this.getMunicipiosByUf(this.trabalhador.sUfRes);
            this.cadastroForm.get("chkAceite").setValue(true);
            if(this.trabalhador.iTipo == 1) {
              this.vrVinculo = 'Efetivo';
            }
            else if(this.trabalhador.iTipo == 2) {
              this.vrVinculo = 'Comissionado Exclusivo';
            }
            else if(this.trabalhador.iTipo == 4) {
              this.vrVinculo = 'Estagiário';
              this.cadastroForm.get('sNisPisPasep').clearValidators();
              this.cadastroForm.get('sNisPisPasep').updateValueAndValidity();
            }
            else if(this.trabalhador.iTipo == 5) {
              this.vrVinculo = 'Aposentado';
            }
            else if(this.trabalhador.iTipo == 6) {
              this.vrVinculo = 'Cedido';
            }
            this.loading = false;
          }
          else {
            this.loading = false;
          }
        },
        error => { console.log(error) }
      ).add((resposta) => {
          if(this.trabalhador.iTipo == 1 || this.trabalhador.iTipo == 2 || this.trabalhador.iTipo == 5) {
            this.cadastroService.getDependentes(this.iCodigo).subscribe(
              response => {
                if(response.length > 0) {
                  this.stDependente = true;
                  this.loading = false;
                }
                else {
                  this.stDependente = false;
                  this.loading = false;
                }
              },
              error => {
                this.loading = false;
              }
            )
          }
          else {
            this.stDependente = true;
            this.loading = false;
          }
        }
      );
    }
    else { // codigo 0
      this.getCidadesByUf('Ce');
      this.getMunicipiosByUf('Ce');
      this.cadastroService.getTipoTrabalhador(localStorage.getItem('cpf')).subscribe(
        value => {
          this.cadastroForm.get('iTipo').setValue(value); 
          if(this.cadastroForm.get('iTipo').value == 1) {
            this.vrVinculo = 'Efetivo';
          }
          else if(this.cadastroForm.get('iTipo').value == 2) {
            this.vrVinculo = 'Comissionado Exclusivo';
          }
          else if(this.cadastroForm.get('iTipo').value == 4) {
            this.vrVinculo = 'Estagiário';
            this.cadastroForm.get('sNisPisPasep').clearValidators();
            this.cadastroForm.get('sNisPisPasep').updateValueAndValidity();
          }
          else if(this.cadastroForm.get('iTipo').value == 5) {
            this.vrVinculo = 'Aposentado';
          }
          else if(this.cadastroForm.get('iTipo').value == 6) {
            this.vrVinculo = 'Cedido';
          }
          this.loading = false;
        },
        error => { console.log(error) }
      ) 
      this.loading = false;
    }
  }

  get diagnostic() {
    return JSON.stringify(this.cadastroForm.value);
  }

  redirecionar() {
    this.router.navigate(['/dependente/' + this.iCodigo]);
  }

  redirecionarEstagiario() {
    this.router.navigate(['/estagiario/' + this.iCodigo]);
  }

  redirecionarCedido() {
    this.router.navigate(['/cedido/' + this.iCodigo]);
  }

  confirmarDados() {
    this.router.navigate(['/consulta/' + this.iCodigo]);
  }


  getDependencias(codigo: any) {
    this.iCodigo = codigo
    if (this.cadastroForm.value.iTipo == 1 || this.cadastroForm.value.iTipo == 2 || this.cadastroForm.value.iTipo == 5) {
      this.openVerticallyCentered(this.modalConfirm);
    }
    else if (this.cadastroForm.value.iTipo == 4) {
      this.openVerticallyCentered(this.modalConfirmEst);
    }
    else if (this.cadastroForm.value.iTipo == 6) {
      this.openVerticallyCentered(this.modalConfirmCed);
    }
  }

  setProximo() {
    if (this._avRoute.snapshot.params["iCodTrabalhador"] > 0) {
      if (this.cadastroForm.value.iTipo == 1 || this.cadastroForm.value.iTipo == 2 || this.cadastroForm.value.iTipo == 5) {
        this.router.navigate(['/dependente/' + this.iCodigo]);
      }
      else if (this.cadastroForm.value.iTipo == 4) {
        this.router.navigate(['/estagiario/' + this.iCodigo]);
      }
      else if (this.cadastroForm.value.iTipo == 6) {
        this.router.navigate(['/cedido/' + this.iCodigo]);
      }
    }
  }

  onSubmit() {
    if (this._avRoute.snapshot.params['iCodTrabalhador'] > 0) {
      this.update(this.cadastroForm.value);
    } else {
      this.save(this.cadastroForm.value);
    }
  }

  forceValidateFields() { }

  save(frm) {
    this.loading = true;

    this.cadastroService.insert(frm)
      .subscribe((resp) => {
        this.loading = false;
        //this.getDependencias(resp); // id do tyrabalhador inserido
        this.cdtrabalahdor = resp;
      },
        error => {
          this.loading = false;
          alert('Falha ao salvar dados');
          console.log(error);
        }
      ).add((resposta) => {
        this.getDependencias(this.cdtrabalahdor); // id do tyrabalhador inserido
      }
    )
  }

  update(frm) {
    this.loading = true;
    this.cadastroService.update(frm, this._avRoute.snapshot.params["iCodTrabalhador"])
      .subscribe((resp) => {
        this.loading = false;
        alert('Dados salvo com sucesso !');
      },
        error => {
          this.loading = false;
          alert('Falha ao salvar dados');
          console.log(error);
        }
      )
  }

  convertDataServidorAngular(response: any) {
    if (response.dtDataNasc != null) {
      response.dtDataNasc = this.datepipe.transform(response.dtDataNasc, 'yyyy-MM-dd');
    }

    if (response.dtExpedRG != null) {
      response.dtExpedRG = this.datepipe.transform(response.dtExpedRG, 'yyyy-MM-dd');
    }

    if (response.dtExpedCNH != null) {
      response.dtExpedCNH = this.datepipe.transform(response.dtExpedCNH, 'yyyy-MM-dd');
    }

    if (response.dtValidadeCNH != null) {
      response.dtValidadeCNH = this.datepipe.transform(response.dtValidadeCNH, 'yyyy-MM-dd');
    }

    if (response.dtPrimeiraHab != null) {
      response.dtPrimeiraHab = this.datepipe.transform(response.dtPrimeiraHab, 'yyyy-MM-dd');
    }

    if (response.dtExpedOC != null) {
      response.dtExpedOC = this.datepipe.transform(response.dtExpedOC, 'yyyy-MM-dd');
    }

    if (response.dtValidadeOC != null) {
      response.dtValidadeOC = this.datepipe.transform(response.dtValidadeOC, 'yyyy-MM-dd');
    }

    if (response.dtNasc != null) {
      response.dtNasc = this.datepipe.transform(response.dtNasc, 'yyyy-MM-dd');
    }

    if (response.dtNascBen != null) {
      response.dtNascBen = this.datepipe.transform(response.dtNascBen, 'yyyy-MM-dd');
    }

    if (response.dtNascDependentePensao != null) {
      response.dtNascDependentePensao = this.datepipe.transform(response.dtNascDependentePensao, 'yyyy-MM-dd');
    }

    if (response.dtAdmissao != null) {
      response.dtAdmissao = this.datepipe.transform(response.dtAdmissao, 'yyyy-MM-dd');
    }
  }

  creatFormGroup() {
    this.cadastroForm = this.fb.group({
      iCodigo: 0,
      iTipo: ['', [Validators.required]],
      sNome: ['', [Validators.required, Validators.minLength(5)]],
      iSexo: ['', [Validators.required]],
      iEstadoCivil: ['', [Validators.required]],
      iRacaCor: ['', [Validators.required]],
      sCPF: [localStorage.getItem('cpf'), [Validators.required, Validators.minLength(11)]],
      sNisPisPasep: ['', [Validators.required]],
      iGrauInstrucao: ['', [Validators.required]],
      iPrimeiroEmprego: [2, [Validators.required]],
      sCodiNomeTravTrans: '',
      dtDataNasc: ['', [Validators.required]],
      sUfNasc: ['Ce', [Validators.required]],
      iCodMunicipioNasc: ['', [Validators.required]],
      iPaisNasc: [105, [Validators.required]],
      iNacionalidade: [105, [Validators.required]],
      sNomeMae: ['', [Validators.required]],
      sNomePai: '',
      sNumCTPS: '',
      sNumSerieCTPS: '',
      sUfCTPS: 'Ce',
      sNumRG: ['', [Validators.required]],
      sEmissaoRG: ['', [Validators.required]],
      dtExpedRG: ['', [Validators.required]],
      sNumCNH: '',
      dtExpedCNH: '',
      sUfCNH: 'Ce',
      dtValidadeCNH: '',
      iCatCNH: '',
      dtPrimeiraHab: '',
      dtExpedOC: '',
      dtValidadeOC: '',
      sNumRegOC: '',
      sEmissaoOC: '',
      sTipoLogradouro: ['', [Validators.required]],
      sLogradouro: ['', [Validators.required]],
      sNumero: ['', [Validators.required]],
      sComplemento: '',
      sBairro: ['', [Validators.required]],
      sCEP: ['', [Validators.required]],
      sUfRes: ['Ce', [Validators.required]],
      iCodMunicipioRes: ['', [Validators.required]],
      sDefFisica: ['N', [Validators.required]],
      sDefVisual: ['N', [Validators.required]],
      sDefAuditiva: ['N', [Validators.required]],
      sDefMental: ['N', [Validators.required]],
      sDefIntelectual: ['N', [Validators.required]],
      sRecebeBeneficioPrev: ['N', [Validators.required]],
      sTelefone1: '',
      sTelefone2: ['', [Validators.required]],
      sEmail: ['', [Validators.required]],
      sEmail2: '',
      chkAceite: ['',[Validators.required]]
    })
  }

  equalsTpLogradouro(tp1: any, tp2: any) {
    return tp1.sCodigo === tp2.sCodigo;
  }

  equalsCidade(c1: any, c2: any) {
    return c1.iCodigo === c2.iCodigo;
  }

  equalsPais(c1: any, c2: any) {
    return c1.iCodigo === c2.iCodigo;
  }

  logout() {
    this.loginService.logout();
  }

  abrirFormArquivo(val: string) {
    this.arquivoService.notifier.emit(val);
  }

  verificaExtensao($input) {
    var extPermitidas = ['jpg', 'png', 'gif', 'pdf', 'txt', 'doc', 'docx'];
    var extArquivo = $input.value.split('.').pop();

    if (typeof extPermitidas.find(function (ext) { return extArquivo == ext; }) == 'undefined') {
      alert('Extensão "' + extArquivo + '" não permitida!');
    } else {
      alert('Ok!');
    }
  }

  
  isValid(filtro: string): boolean {
    return this.cadastroForm.controls[filtro].valid && (this.cadastroForm.controls[filtro].dirty || this.cadastroForm.controls[filtro].touched)
  }

  isInvalid(filtro: string): boolean {
    return this.cadastroForm.controls[filtro].invalid && (this.cadastroForm.controls[filtro].dirty || this.cadastroForm.controls[filtro].touched)
  }

  getMaskTelefone(parametro: string): string {
    if(parametro.length <= 10) {
      return "(00) 0000-0000";
    }
    else {
      return "(00) 00000-0000";
    }
  }
  
  ngOnChanges(event): void {
    this.trabalhador.iTipo = event.target.value;
  }

  carregarCidades(event): void {
    //console.log(event.target.value);
    this.getCidadesByUf(event.target.value);
  }

  getCidadesByUf(uf: string) {
    this.cadastroService.getCidadesByUf(uf)
      .subscribe((value) => {
        this.cidades = value; 
      });
  }

  carregarMunicipios(event): void {
    //console.log(event.target.value);
    this.getMunicipiosByUf(event.target.value);
  }

  getMunicipiosByUf(uf: string) {
    this.cadastroService.getCidadesByUf(uf)
      .subscribe((value) => {
        this.municipios = value; 
      });
  }

  openVerticallyCentered(content) {
    this.modalService.open(content, { centered: true });
  }      

  enviarEmail() {
    alert('Você receberá um email com a confirmação dos dados');
    this.cadastroService.enviarEmail(
    // this._avRoute.snapshot.params["iCodTrabalhador"]).subscribe(
      this.cdtrabalahdor).subscribe(
      response => {
        this.logout();
      }
    ) 
  }
}
