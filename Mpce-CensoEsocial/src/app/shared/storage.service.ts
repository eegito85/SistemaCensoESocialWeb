import { Injectable } from '@angular/core';
import { STORAGE_KEYS, STORAGE_CPF } from '../config/storage-keys';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }
  public getUsuarioLogado(){
    let usr = localStorage.getItem(STORAGE_KEYS.usuarioLogado);
    if (usr == null){
      return null;
    }else {
      return JSON.parse(usr);
    }

  }

  setUsuarioLogado(obj: any){
    if (obj == null) {
      localStorage.removeItem(STORAGE_KEYS.usuarioLogado);
    }
    else {
      localStorage.setItem(STORAGE_KEYS.usuarioLogado,JSON.stringify(obj));
    }

  }

  public setCpf(cpf: string) {
    if(cpf == null) {
      localStorage.removeItem(STORAGE_CPF.cpfLogado);
    }
    else {
      localStorage.setItem(STORAGE_CPF.cpfLogado, cpf);
    }
  }

  public getCpf() {
    let cpf = localStorage.getItem(STORAGE_CPF.cpfLogado);
    if (cpf == null){
      return null;
    }else {
      return cpf;
    }    
  }


}
