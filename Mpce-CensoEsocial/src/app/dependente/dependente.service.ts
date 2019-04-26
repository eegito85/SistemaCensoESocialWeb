import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { API_URL } from '../config/api.config';
import { FormGroup } from '@angular/forms';
import { API_URL } from '../config/api.config';
import { Dependente } from '../model/dependente.model';

@Injectable({
  providedIn: 'root'
})
export class DependenteService {
    arquivoEmitter: EventEmitter<string> = new EventEmitter();

    constructor(private http: HttpClient) { }

    insert(dependente: FormGroup) {
        return this.http.post(`${API_URL}/dependente/`, dependente)
    }

    getDependentes(codigo: number) {
        return this.http.get<Dependente[]>(`${API_URL}/dependente/Trabalhador/${codigo}`);
    }    

    getDependente(codigo: number) {
        return this.http.get<Dependente>(`${API_URL}/dependente/${codigo}`);
    }    

    excluirDependente(codigo: number) {
        return this.http.delete(`${API_URL}/dependente/${codigo}`);
    }    

    update(dependente: Dependente,codigo: number) {
        return this.http.put<Dependente>(`${API_URL}/dependente/${codigo}`, dependente);
    }
    enviarEmail(codigo: any) {
        return this.http.get(`${API_URL}/email/enviar/${codigo}`);
    }
        
}