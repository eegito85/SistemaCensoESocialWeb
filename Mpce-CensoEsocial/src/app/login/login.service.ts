import { StorageService } from './../shared/storage.service';
import { Http } from '@angular/http';
import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../config/api.config';
import { FormGroup } from '@angular/forms';
import { Trabalhador } from '../model/trabalhador.model';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class LoginService {

  jwtHelper: JwtHelperService = new JwtHelperService();

  constructor(private http: HttpClient,
               private storage: StorageService, private router: Router, 
               private _avRoute: ActivatedRoute) { }

  insert(usuario: FormGroup) {
    return this.http.post(`${API_URL}/login/`, usuario, {observe: "response", responseType: "text"});
  }

  getTrabalhador(cpf: string) {
    return this.http.get<Trabalhador>(`${API_URL}/trabalhador/trabalhador/${cpf}`);
  }

  getTipoTrabalhador(cpf: string) {
    return this.http.get<number>(`${API_URL}/trabalhador/iTipo/${cpf}`);
  }

verificaUsuario(cpf: string) {
    return this.http.get<number>(`${API_URL}/trabalhador/inicio/${cpf}`);
  }

  redirectUnauthorized() {
    this.router.navigate(['']);
  }

  successfullLogin(token: string){
    //console.log('Token carregado: '+ token);
    /*
    let tok = token;
    let usuarioStorage: any = {
      token: tok,
      cpf: this.jwtHelper.decodeToken(tok).sub
    };*/
    this.storage.setUsuarioLogado(token);
  }

  setCpf(cpf: string) {
    this.storage.setCpf(cpf);
  }
  /*
  logout() {
    this.storage.setUsuarioLogado(null);
    this.notifier.emit(false);
  }
  */  
  logout() {
    this.storage.setUsuarioLogado(null);
    this.storage.setCpf('');
    this.redirectUnauthorized();
  }
  
  isAuthenticated(): boolean {
    let usr  = this.storage.getUsuarioLogado();
    if (usr != null) {
      return true;
    } else {
      return false;
    }
  }  
}

