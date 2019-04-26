import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { API_URL } from '../config/api.config';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { TrabalhadorConsulta } from '../model/trabalhadorConsulta.models';
import { DependenteConsulta } from '../model/dependenteConsulta.modules';
import { EstagiarioConsulta } from '../model/estagiarioConsulta.modules';
import { CedidoConsulta } from '../model/cedidoConsulta.modules';
import { Arquivo } from '../model/arquivo.model';

@Injectable({
  providedIn: 'root'
})
export class ConsultaService {

  constructor(private http: HttpClient) { }

  get(codigo: any) {
    return this.http.get<TrabalhadorConsulta>(`${API_URL}/consulta/trabalhador/${codigo}`);
  }
    
  getDependentes(codigo: number) {
    return this.http.get<DependenteConsulta[]>(`${API_URL}/consulta/dependentes/${codigo}`);
  }    
  getEstagiario(codigo: any) {
    return this.http.get<EstagiarioConsulta>(`${API_URL}/consulta/estagiario/${codigo}`);
  }

  getCedido(codigo: any) {
    return this.http.get<CedidoConsulta>(`${API_URL}/consulta/cedido/${codigo}`);
  }

  getArquivos(cpf: any) {
    return this.http.get<Arquivo[]>(`${API_URL}/documento/documento/${cpf}`);
  }  

  baixar(id: number, nome: string) {
    let extensao = nome.split('.').pop();
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

    return this.http.get(`${API_URL}/documento/codigo/${id}`, { 
      headers: headers, responseType: "blob" 
    });
  }

}
