import { StorageService } from './../shared/storage.service';
import { Trabalhador } from './../model/trabalhador.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../config/api.config';
import { TipoLogradouro } from '../model/tipo-logradouro.model';
import { Cidade } from '../model/cidade.model';
import { Pais } from '../model/pais.model';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { Dependente } from '../model/dependente.model';

@Injectable({
  providedIn: 'root'
})
export class CadastroService {

  constructor(private http: HttpClient, private storage: StorageService) { }
  
  getTiposLogradouro() {
    return this.http.get<TipoLogradouro[]>(`${API_URL}/tipologradouro`);
  }

  getCidades(): Observable<any> {
    return this.http.get<any>(`${API_URL}/municipio`);
  }

  getCidadesByUf(uf: string): Observable<any> {
    return this.http.get<any>(`${API_URL}/municipio/uf/${uf}`);
  }

  getPaises() {
    return this.http.get<Pais[]>(`${API_URL}/pais`);
  }

  get(codigo: any) {
    return this.http.get<Trabalhador>(`${API_URL}/trabalhador/${codigo}`, this.storage.getUsuarioLogado());
  }
 
  insert(trabalhador: FormGroup) {
    return this.http.post(`${API_URL}/trabalhador/`, trabalhador)
  }
  
  update(trabalhador: Trabalhador,codigo: number) {
    return this.http.put<Trabalhador>(`${API_URL}/trabalhador/${codigo}`, trabalhador);
  }

  getTipoTrabalhador(cpf: string) {
    return this.http.get<number>(`${API_URL}/trabalhador/iTipo/${cpf}`);
  }

  getDependentes(codigo: number) {
    return this.http.get<Dependente[]>(`${API_URL}/dependente/Trabalhador/${codigo}`);
  }    

  enviarEmail(codigo: any) {
    return this.http.get(`${API_URL}/email/enviar/${codigo}`);
  }

}
