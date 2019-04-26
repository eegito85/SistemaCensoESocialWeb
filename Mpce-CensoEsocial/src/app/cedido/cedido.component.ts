import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common'

import { Cedido } from './../model/cedido.model';
import { CedidoService } from './cedido.service';
import { ArquivoService } from '../arquivos/arquivo.service';
import { LoginService } from '../login/login.service';



@Component({
  selector: 'app-cedido',
  templateUrl: './cedido.component.html',
  styleUrls: ['./cedido.component.css']
})

export class CedidoComponent implements OnInit {
  cedidoForm: FormGroup;
  iCodTrabalhador: number; 
  cedido: Cedido;
  loading: boolean;

  constructor(private fb: FormBuilder, private CedidoService: CedidoService, 
    private _avRoute: ActivatedRoute, private router: Router, public datepipe: DatePipe, 
    public arquivoService: ArquivoService, public loginService: LoginService) { }

  ngOnInit() {
    this.loading = true;
    this.iCodTrabalhador = this._avRoute.snapshot.params["iCodTrabalhador"];
    this.creatFormGroup(); 
    if(this.iCodTrabalhador > 0) {
      this.CedidoService.getCedidoTrab(this.iCodTrabalhador).subscribe (
        response => {
          this.cedido = response;
          if(response != null) {
            if(response.dtAdmissao != null) {
              response.dtAdmissao = this.datepipe.transform(response.dtAdmissao, 'yyyy-MM-dd');
            }
            this.cedidoForm.patchValue(response, { onlySelf: true })
            this.loading = false;
          }
          else {
            this.loading = false;
          }
        },
        error => {
          this.loading = false;
          console.log(error);
        }
      )
    }
    else {
      this.loading = false;
    }
  }

  onSubmit() {
    if(this.cedidoForm.value.iCodigo > 0) {
      this.update(this.cedidoForm.value);
    }
    else {
      this.save(this.cedidoForm.value);
    }
  }

  save(frm: FormGroup) {
    this.loading = true;
    this.CedidoService.insert(frm)
    .subscribe((resp) => {
      this.loading = false;
        this.envairEmail();
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
    this.cedido = frm
    this.CedidoService.update(this.cedido, this.cedido.iCodigo)
    .subscribe((resp) => {
      this.loading = false;
      alert('Dados Censo alterados com sucesso!');
      this.logout();
      }, 
      error => {
        this.loading = false;
        alert('Falha ao salvar dados');
        console.log(error);
      }
    )
  }

  creatFormGroup() {
    this.cedidoForm = this.fb.group({
      iCodigo: 0,
      iCategoria: ['', [Validators.required]],
      sCNPJEmpCed: ['', [Validators.required, Validators.minLength(14)]],
      sMatriculaTrab: ['', [Validators.required]],
      dtAdmissao: '',
      iTipoRegTrab: ['', [Validators.required]],
      iTipoRegPrev: ['', [Validators.required]],
      iOnusCessReq: ['', [Validators.required]],
      iCodTrabalhador: this.iCodTrabalhador
    }) 
  }

  logout() {
    this.loginService.logout();    
  }

  envairEmail() {
    this.CedidoService.enviarEmail(this._avRoute.snapshot.params["iCodTrabalhador"])
    .subscribe((response) => {
        alert('Dados Censo concluído com sucesso!, Você receberá um email com a confirmação dos dados');
        this.loginService.logout();
      }
    )
  }

  abrirFormArquivo(val: string){
    this.arquivoService.notifier.emit(val);
  }

  getAnterior() {
    this.router.navigate(['/cadastro/'+this._avRoute.snapshot.params["iCodTrabalhador"]]);
  }

  isValid(filtro: string): boolean {
    return this.cedidoForm.controls[filtro].valid && (this.cedidoForm.controls[filtro].dirty || this.cedidoForm.controls[filtro].touched)
  }

  isInvalid(filtro: string): boolean {
    return this.cedidoForm.controls[filtro].invalid && (this.cedidoForm.controls[filtro].dirty || this.cedidoForm.controls[filtro].touched)
  }
}
