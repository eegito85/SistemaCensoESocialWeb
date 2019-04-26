import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import * as moment from 'moment';
import { EstagiarioService } from './Estagiario.service';
import { Estagiario } from '../model/estagiario.model';
import { Observable } from '../../../node_modules/rxjs';
import { LoginService } from '../login/login.service';

@Component({
  selector: 'app-estagiario',
  templateUrl: './estagiario.component.html',
  styleUrls: ['./estagiario.component.css']
})
export class EstagiarioComponent implements OnInit {
  estagiarioForm: FormGroup;
  iCodTrabalhador: number; 
  estagiario: Estagiario;
  cidades: Observable<any>;
  loading: boolean;
  
  constructor(private fb: FormBuilder, private EstagiarioService: EstagiarioService, 
    private _avRoute: ActivatedRoute, private router: Router, 
    public loginService: LoginService) { }

  ngOnInit() {
    this.loading = true;
    this.getCidadesByUf('Ce');
    this.iCodTrabalhador = this._avRoute.snapshot.params["iCodTrabalhador"];
    this.creatFormGroup(); 
    this.EstagiarioService.getEstagiarioTrab(this.iCodTrabalhador).subscribe(
      response => {
        if(response != null) {
          this.estagiario = response;
          this.estagiarioForm.patchValue(response, { onlySelf: true });
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

  getCidades() {
    this.EstagiarioService.getCidades()
      .subscribe((value) => {
        this.cidades = value;
        console.log(this.cidades);
      });
  }

  onSubmit() {
    if(this.estagiarioForm.value.iCodigo > 0) {
      this.update(this.estagiarioForm.value);
    }
    else {
      this.save(this.estagiarioForm.value);
    }
  }

  getAnterior() {
    this.router.navigate(['/cadastro/'+this._avRoute.snapshot.params["iCodTrabalhador"]]);
  }
  
  getEstagiario(codigo: number) {
    this.EstagiarioService.getEstagiario(codigo).subscribe(
      response => {
        this.estagiario = response;
        this.estagiarioForm.patchValue(response, { onlySelf: true })
      },
      error => {
        console.log(error);
      }
    )
  }

  save(frm: FormGroup) {
    this.loading = true;
    this.EstagiarioService.insert(frm)
    .subscribe((resp) => {
      this.loading = false;
      this.concluir();
      }, 
      error => {
        this.loading = false;
        alert('Falha ao salvar dados');
        console.log(error);
      }
    )
  }

  concluir() {
    this.EstagiarioService.enviarEmail(this._avRoute.snapshot.params["iCodTrabalhador"])
    .subscribe((response) => {
        alert('Dados Censo concluído com sucesso!, Você receberá um email com a confirmação dos dados');
        this.logout();
      }
    )
  }

  update(frm) {
    this.loading = true;
    this.estagiario = frm
    this.EstagiarioService.update(this.estagiario,this.estagiario.iCodigo)
    .subscribe((resp) => {
      this.loading = false;
      alert('Dados salvos com sucesso!');
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
    this.estagiarioForm = this.fb.group({
      iCodigo: 0,
      iNaturezaEstagio: [2, [Validators.required]],
      iCodCidadeInst: ['', [Validators.required]],
      iAreaAtuacao: ['', [Validators.required]],
      sRazaoSocialInst: ['', [Validators.required]],
      sCNPJInst: ['', [Validators.required,Validators.minLength(14)]],
      sLogradouroInst: ['', [Validators.required]],
      sNomeSupervisor: ['', [Validators.required]],
      sNumInst: ['', [Validators.required]],
      sBairroInst: ['', [Validators.required]],
      sCepInst: '',
      sUfInst: ['', [Validators.required]],
      iCodTrabalhador: this.iCodTrabalhador
    }) 
  }

  logout() {
    this.loginService.logout();
  }

  isValid(filtro: string): boolean {
    return this.estagiarioForm.controls[filtro].valid && (this.estagiarioForm.controls[filtro].dirty || this.estagiarioForm.controls[filtro].touched)
  }

  isInvalid(filtro: string): boolean {
    return this.estagiarioForm.controls[filtro].invalid && (this.estagiarioForm.controls[filtro].dirty || this.estagiarioForm.controls[filtro].touched)
  }

  carregarCidades(event): void {
    console.log(event.target.value);
    this.getCidadesByUf(event.target.value);
  }

  getCidadesByUf(uf: string) {
    this.EstagiarioService.getCidadesByUf(uf)
      .subscribe((value) => {
        this.cidades = value; 
      });
  }

}
