import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { API_URL } from '../config/api.config';
import { FormGroup } from '@angular/forms';
import { API_URL } from '../config/api.config';
import { Cedido } from '../model/cedido.model';

@Injectable({
  providedIn: 'root'
})
export class CedidoService {

    constructor(private http: HttpClient) { }

    insert(cedido: FormGroup) {
        return this.http.post(`${API_URL}/cedido/`, cedido);
    }

    getCedidoTrab(codigo: number) {
        return this.http.get<Cedido>(`${API_URL}/cedido/Trabalhador/${codigo}`);
    }    

    getCedido(codigo: number) {
        return this.http.get<Cedido>(`${API_URL}/cedido/${codigo}`);
    }    

    excluirCedido(codigo: number) {
        return this.http.delete(`${API_URL}/cedido/${codigo}`);
    }
    
    update(cedido: Cedido,codigo: number) {
        return this.http.put<Cedido>(`${API_URL}/cedido/${codigo}`, cedido);
    }
    enviarEmail(codigo: any) {
        return this.http.get(`${API_URL}/email/enviar/${codigo}`);
    }
}