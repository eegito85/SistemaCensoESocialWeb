import { TrabalhadorConsulta } from './../model/trabalhadorConsulta.models';
import { Component, OnInit } from '@angular/core';
import { ConsultaService } from './consulta.service';
import { DatePipe } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { EstagiarioConsulta } from '../model/estagiarioConsulta.modules';
import { CedidoConsulta } from '../model/cedidoConsulta.modules';
import { Arquivo } from '../model/arquivo.model';

@Component({
  selector: 'app-consulta',
  templateUrl: './consulta.component.html',
  styleUrls: ['./consulta.component.css']
})
export class ConsultaComponent implements OnInit {

  trabalhador: TrabalhadorConsulta = new TrabalhadorConsulta();
  iCodigo: any;
  dependentes: any;
  estagiario: EstagiarioConsulta = new EstagiarioConsulta();
  cedido: CedidoConsulta = new CedidoConsulta();
  arquivos: Arquivo[];
  
  constructor(private consultaService: ConsultaService, public datepipe: DatePipe,
    private router: Router, private _avRoute: ActivatedRoute) { }

  ngOnInit() {
    this.iCodigo = this._avRoute.snapshot.params["iCodTrabalhador"];
    this.consultaService.get(this.iCodigo).subscribe(
      response => {
        if(response != null) {
          this.trabalhador = response;
          if(this.trabalhador.sVinculo == 'Efetivo') {
            this.getDependentes();
          }
          else if(this.trabalhador.sVinculo == 'Estagiário') {
            this.getEstagiario();
          }
          else if(this.trabalhador.sVinculo == 'Cedido') {
            this.getCedido();
          }
          this.carregarArquivos(this.trabalhador.sCPF);
        }
      },
      error => { console.log(error) }
    )
  } 

  getDependentes() {
    this.consultaService.getDependentes(this.iCodigo).subscribe(
      response => {
        this.dependentes = response;
      },
      error => { console.log(error) }
    )
  }

  getEstagiario() {
    this.consultaService.getEstagiario(this.iCodigo).subscribe(
      response => {
        if(response != null) {
          this.estagiario = response;
        }
      },
      error => { console.log(error) }
    )
  }

  getCedido() {
    this.consultaService.getCedido(this.iCodigo).subscribe(
      response => {
        if(response != null) {
          this.cedido = response;
        }
      },
      error => { console.log(error) }
    )
  }

  carregarArquivos(cpf: any) {
    this.arquivos = new Array<Arquivo>();
    this.consultaService.getArquivos(cpf).subscribe(
      response => {
        this.arquivos = response;
      },
      error => { console.log(error) }
    );  
  } 
  
  baixar(arquivo: Arquivo) {
    this.consultaService.baixar(arquivo.iCodigo,arquivo.sNomeArquivo)
    .subscribe( resp => {
      let extensao = arquivo.sNomeArquivo.split('.').pop();
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
      let file = new Blob([resp],{type: tipo});
      let fileURL = URL.createObjectURL(file);
      window.open(fileURL, '_blank');
    },
      error => {
        alert('Falha ao fazer download');
        console.log(error);
      }
    )
  }

  retornar() {
    history.go(-1);
  }

}
