import { Component, OnInit, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { DatePipe } from '@angular/common'
import { ArquivoService } from './../arquivos/arquivo.service';
import { DependenteService } from './dependente.service';
import * as moment from 'moment';
import { Dependente } from '../model/dependente.model';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'app-dependente',
  templateUrl: './dependente.component.html',
  styleUrls: ['./dependente.component.css']
})
export class DependenteComponent implements OnInit {
  dependenteForm: FormGroup;
  iCodTrabalhador: number; 
  dependentes: Dependente[] = new Array<Dependente>();
  dependente: Dependente;
  loading: boolean;
  stObg: boolean = false;

  constructor(private fb: FormBuilder, private DependenteService: DependenteService, 
    private _avRoute: ActivatedRoute, private router: Router, public datepipe: DatePipe,
    public arquivoService: ArquivoService, public loginService: LoginService) { }

  ngOnInit() {
    this.loading = true;
    this.iCodTrabalhador = this._avRoute.snapshot.params["iCodTrabalhador"];
    this.creatFormGroup();  
    this.DependenteService.arquivoEmitter.subscribe(
      value => {
        console.log('Retorno do arquivo: '+value);
        if(value == 'CPF do Dependente') {
          this.dependenteForm.get("stCpf").setValue(true);
        }
        else if(value == 'stDirrf') {
          this.dependenteForm.get("stDirrf").setValue(true);
        }
      }
    ) 
    this.DependenteService.getDependentes(this.iCodTrabalhador).subscribe(
      response => {
        if(response != null) {
          this.dependentes = response;
          this.loading = false;
          console.log(response);
        }
      },
      error => {
        this.loading = false;
        console.log(error);
      }
    )
  }

  onSubmit() {
    if(this.dependenteForm.value.iCodigo > 0) {
      this.update(this.dependenteForm.value);  
    }
    else {
      this.save(this.dependenteForm.value);
    }
  }

  getAnterior() {
    this.router.navigate(['/cadastro/'+this._avRoute.snapshot.params["iCodTrabalhador"]]);
  }

  getDependente(codigo: number) {
    this.DependenteService.getDependente(codigo).subscribe(
      response => {
        this.dependente = response;
        if(response.dtNasc != null) {
          response.dtNasc = this.datepipe.transform(response.dtNasc, 'yyyy-MM-dd');
        }
        this.dependenteForm.patchValue(response, { onlySelf: true });
        this.dependenteForm.controls['stCpf'].setValue(true);
        this.dependenteForm.controls['stDirrf'].setValue(true);
      },
      error => {
        console.log(error);
      }
    )
  }

  excluirDependente(codigo: number) {
    if(confirm('Deseja realmente excluir esse registro?')) {
      this.DependenteService.excluirDependente(codigo).subscribe(
        response => {
          console.log(response);
          this.ngOnInit();
        }  
      )
    }
  } 
    
  save(frm) {
    this.loading = true;
    this.DependenteService.insert(frm)
    .subscribe((resp) => {
        this.loading = false;
        alert('Dados salvos com sucesso!');
        this.ngOnInit();
      }, 
      error => {
        this.loading = false;
        alert('Falha ao salvar dados');
        console.log(error);
      }
    )
  }

  update(frm) {
    this.loading = true;
    this.dependente = frm;
    this.DependenteService.update(this.dependente,this.dependente.iCodigo)
    .subscribe((resp) => {
        this.loading = false;
        alert('Dados salvos com sucesso!');
        this.ngOnInit();
      }, 
      error => {
        console.log(error);
      }
    )
  }

  creatFormGroup() {
    this.dependenteForm = this.fb.group({
      iCodigo: 0,
      iTipoDependente: ['', [Validators.required]],
      sNomeDependente: ['', [Validators.required]],
      dtNasc: ['', [Validators.required]],
      sCPFDependente: ['',[Validators.required,Validators.minLength(11)]],
      sDepTrabIRRF: ['', [Validators.required]],
      sDepIncapaFisMentTrab: ['', [Validators.required]],
      iCodTrabalhador: this.iCodTrabalhador,
      iDependentePensao: ['', [Validators.required]],
      sResponsavel: [''],
      sTelefoneResp: [''],
      stCpf: ['', [Validators.required]],
      stDirrf: ['']
    }) 
  }

  concluir() {
    this.DependenteService.enviarEmail(this._avRoute.snapshot.params["iCodTrabalhador"])
    .subscribe((response) => {
        alert('Dados Censo concluído com sucesso!, Você receberá um email com a confirmação dos dados');
        this.logout();
      },
      error => {
        console.log(error);
      }
    )
  }

  logout() {
    this.loginService.logout();
    this.router.navigate(['']);    
  }  

  abrirFormArquivo(val: string){
    if(this.dependenteForm.value.iTipoDependente == '') {
      alert('Selecione o Tipo de Dependente');
      this.dependenteForm.controls['sDepTrabIRRF'].setValue('');
    }
    else {
      if(val == '') {
        this.arquivoService.notifier.emit(val+'+'+this.dependenteForm.value.iTipoDependente);
      }
      else {
        this.arquivoService.notifier.emit(val);
      }
    }
  }

  abrirFormArquivoIrrf(val: string){
    if(this.dependenteForm.value.iTipoDependente == '') {
      alert('Selecione o Tipo de Dependente');
      this.dependenteForm.controls['sDepTrabIRRF'].setValue('');
    }
    else if(this.dependenteForm.value.iTipoDependente == 12) {
      alert('Ex-cônjugue não entra para dedução de IRRF');
      this.dependenteForm.controls['sDepTrabIRRF'].setValue('N');
    }
    else {
      if(val == '') {
        this.arquivoService.notifier.emit(val+'+'+this.dependenteForm.value.iTipoDependente);
      }
      else {
        this.arquivoService.notifier.emit(val);
      }
    }
  }

  isValid(filtro: string): boolean {
    return this.dependenteForm.controls[filtro].valid && (this.dependenteForm.controls[filtro].dirty || this.dependenteForm.controls[filtro].touched)
  }

  isInvalid(filtro: string): boolean {
    return this.dependenteForm.controls[filtro].invalid && (this.dependenteForm.controls[filtro].dirty || this.dependenteForm.controls[filtro].touched)
  }

  verificaIdade(): void {
    
    //console.log("Data: "+this.dependenteForm.value.dtNasc);
    var date1 = new Date(this.dependenteForm.value.dtNasc);
    //console.log('Data nascimento: '+date1.getTime());
    var date2 = new Date(Date.now());
    //console.log('Data atual: '+date2.getTime());
    var ret = (date2.getTime()-date1.getTime())/(86400000*365.25);
    //console.log('Idade: '+ ret);
    var val: number = Math.floor(ret);
    if(val >= 8) {
      this.dependenteForm.controls.sCPFDependente.setValidators([Validators.required, Validators.minLength(11)]);
      this.dependenteForm.controls.sCPFDependente.updateValueAndValidity();
      this.stObg = true;
    }
    else {
      this.dependenteForm.controls.sCPFDependente.setValidators([]);
      this.dependenteForm.controls.sCPFDependente.updateValueAndValidity();
      this.stObg = false;
    }
  } 

  callUpload(): void {
    //console.log('Cpf: '+ this.dependenteForm.value.sCPFDependente.length);
    if(this.dependenteForm.value.sCPFDependente.length == 11) {
      this.abrirFormArquivo('CPF do Dependente');
    }
  }

  callUploadIrf(): void {
    if(this.dependenteForm.value.sDepTrabIRRF == 'S') {
      this.dependenteForm.controls.stDirrf.setValidators([Validators.required]);
      this.dependenteForm.controls.stDirrf.updateValueAndValidity();
      this.abrirFormArquivoIrrf('Comprovante de Dependência');
    }
    else {
      this.dependenteForm.controls.stDirrf.setValidators([]);
      this.dependenteForm.controls.stDirrf.updateValueAndValidity();
    }
  }

  get diagnostic() {
    return JSON.stringify(this.dependenteForm.value);
  }


}


