import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { element } from 'protractor';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import * as moment from 'moment';
import { LoginService } from './login.service';
import { Usuario } from '../model/usuario.model';
import { Trabalhador } from '../model/trabalhador.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  iCodTrabalhador: number;
  trabalhador: Trabalhador;
  usuario: Usuario;
  id: number;
  loading: boolean;
  @ViewChild('content') modalConfirm: any;

  constructor(private fb: FormBuilder, private LoginService: LoginService,
    private _avRoute: ActivatedRoute, private router: Router, public modalService: NgbModal) { }

  ngOnInit() {
    this.creatFormGroup();
  }

  getTrabalhador() {
    this.loading = true;
    this.LoginService.getTrabalhador(this.loginForm.value.cpf).subscribe(
      response => {
        this.loading = false;
        this.trabalhador = response;
        this.id = this.trabalhador != null ? this.trabalhador.iCodigo : 0;
        this.router.navigate(['/cadastro', this.id]); 
      },
      error => { console.log(error) }
    );
  }

  getTrabalhadorExistente() {
    this.loading = true;
    this.LoginService.getTrabalhador(this.loginForm.value.cpf).subscribe(
      response => {
        this.loading = false;
        this.trabalhador = response;
        this.id = this.trabalhador != null ? this.trabalhador.iCodigo : 0;
        this.router.navigate(['/consulta/' + this.id]); 
      },
      error => { console.log(error) }
    );
  }

  onSubmit() {
    //console.log(this.loginForm.value)
    this.save(this.loginForm.value);
  }

  logout() {
    this.router.navigate(['']);
  }
 

  save(frm: any) {
    this.LoginService.insert(frm)
      .subscribe( resp => {
        if(resp.body == 'Usuário ou senha inválido!') {
          alert('Usuário ou senha inválido!');
          this.loginForm.reset();
        } 
        else {
          this.LoginService.successfullLogin(resp.body);
          this.LoginService.setCpf(frm.cpf);
          // verificar se o usuario é administrado
          this.LoginService.verificaUsuario(this.loginForm.value.cpf).subscribe(
            response => {
              if(response != null) {
                if(response == 0) {
                  this.carregarTrabalhador();  
                }
                else if(response == 2) {
                  //alert('O(A) SENHOR(A) JÁ RESPONDEU AO CENSO DO ESOCIAL OBRIGADO. VISUALIZE SEUS DADOS');
                  this.getTrabalhadorExistente();
                }
                else {
                  this.openVerticallyCentered(this.modalConfirm);
                }
              }
            }
          )
        }
      },
        error => {
          console.log('Erro: '+JSON.stringify(error));
        }
      )
  }

  redirecionarAdm() {
    this.router.navigate(['/administracao']);
  }

  carregarTrabalhador() {
    this.LoginService.getTipoTrabalhador(this.loginForm.value.cpf).subscribe(
      response => {
        this.loading = false;
        if(response == 0) {
          alert('Vínculo com acesso não autorizado');
          this.loginForm.reset();
        }
        else {
          this.getTrabalhador();       
        }
      },
      error => { console.log(error) }
    );
  }

  creatFormGroup() {
    this.loginForm = this.fb.group({
      iCodigo: 0, 
      cpf: ['',[Validators.required, Validators.minLength(11)]],
      Senha: ['',[Validators.required, Validators.minLength(7)]],
      iCodTrabalhador: [''],
      mensagem: ['']
    })
  }

  isValid(filtro: string): boolean {
    return this.loginForm.controls[filtro].valid && (this.loginForm.controls[filtro].dirty || this.loginForm.controls[filtro].touched)
  }
  
  isInvalid(filtro: string): boolean {
    return this.loginForm.controls[filtro].invalid && (this.loginForm.controls[filtro].dirty || this.loginForm.controls[filtro].touched)
  }

  getMensagem(parametro: string, tamanho: number) {
      return "O campo "+parametro+" é obrigatório e deve conter "+tamanho+" caracteres";
  }

  openVerticallyCentered(content) {
    this.modalService.open(content, { centered: true });
  }      
  
}
//Validators.pattern(/^(\d{3}\.){2}\d{3}\-\d{2}$/)
