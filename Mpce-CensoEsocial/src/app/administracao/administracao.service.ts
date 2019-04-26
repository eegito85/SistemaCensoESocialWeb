import { StorageService } from './../shared/storage.service';
import { Trabalhador } from './../model/trabalhador.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../config/api.config';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdministracaoService {

  constructor(private http: HttpClient, private storage: StorageService) { }

  get() {
    return this.http.get<Trabalhador[]>(`${API_URL}/trabalhador`, this.storage.getUsuarioLogado());
  }
  update(descricao: string,status: number,codigo: number) {
    return this.http.put<Trabalhador>(`${API_URL}/trabalhador/atualizar/${codigo}/${descricao}/${status}`, Trabalhador);
  }

  enviarEmail(descricao: string,email: string) {
    return this.http.get(`${API_URL}/email/alteracao/${email}/${descricao}`);
  }

  getTrabalhador(cpf: string) {
    return this.http.get<Trabalhador>(`${API_URL}/trabalhador/trabalhador/${cpf}`);
  }

}
