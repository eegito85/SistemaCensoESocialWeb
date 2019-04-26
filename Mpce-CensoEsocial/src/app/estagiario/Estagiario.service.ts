import { Estagiario } from './../model/estagiario.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { API_URL } from '../config/api.config';
import { FormGroup } from '@angular/forms';
import { API_URL } from '../config/api.config';
import { Observable } from '../../../node_modules/rxjs';

@Injectable({
  providedIn: 'root'
})
export class EstagiarioService {

    constructor(private http: HttpClient) { }

    insert(estagiario: FormGroup) {
        return this.http.post(`${API_URL}/estagiario/`, estagiario)
    }

    getEstagiarioTrab(codigo: number) {
        return this.http.get<Estagiario>(`${API_URL}/estagiario/Trabalhador/${codigo}`);
    }    

    getEstagiario(codigo: number) {
        return this.http.get<Estagiario>(`${API_URL}/estagiario/${codigo}`);
    }    

    excluirEstagiario(codigo: number) {
        return this.http.delete(`${API_URL}/estagiario/${codigo}`);
    }    

    update(estagiario: Estagiario,codigo: number) {
        return this.http.put<Estagiario>(`${API_URL}/estagiario/${codigo}`, estagiario);
    }

    getCidades(): Observable<any> {
        return this.http.get<any>(`${API_URL}/municipio`);
    }
    
    getCidadesByUf(uf: string): Observable<any> {
        return this.http.get<any>(`${API_URL}/municipio/uf/${uf}`);
    }
    enviarEmail(codigo: any) {
        return this.http.get(`${API_URL}/email/enviar/${codigo}`);
    }
}