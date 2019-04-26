import { Arquivo } from './../model/arquivo.model';
import { HttpClient } from '@angular/common/http';
import { Injectable, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { API_URL } from '../config/api.config';
import { ArquivosComponent } from './arquivos.component';

import { HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'my-auth-token'
  })
}

@Injectable({
  providedIn: 'root'
})

export class ArquivoService {

  notifier: EventEmitter<string> = new EventEmitter<string>();
  vrparam: string;

  constructor(private http: HttpClient) { }

  get(cpf: any) {
    return this.http.get<Arquivo[]>(`${API_URL}/documento/documento/${cpf}`);
  }

  baixar(id: number,nome: string) {
    let arrext = nome.split('.');
    let arrextensao = arrext.reverse();
    let extensao = arrextensao[0];
    let tipo;
    //console.log(extensao[0]);
    if(extensao == 'pdf') {
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
    let headers = new HttpHeaders();
    headers = headers.set("Accept", tipo);

    return this.http.get(`${API_URL}/documento/codigo/${id}`, { headers: headers, responseType: "blob" });
  }
  insert(arquivo: FormGroup) {
    return this.http.post(`${API_URL}/documento/`, arquivo, httpOptions);
  }

  update(arquivo: Arquivo, codigo: number) {
    return this.http.put<Arquivo>(`${API_URL}/documento/${codigo}`, arquivo);
  }

  excluirArquivo(codigo: number) {
    return this.http.delete(`${API_URL}/documento/${codigo}`);
  }  

}
