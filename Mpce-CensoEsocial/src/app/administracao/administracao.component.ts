import * as $ from 'jquery';
import { DatePipe } from '@angular/common';
import { Trabalhador } from './../model/trabalhador.model';
import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { AdministracaoService } from './administracao.service';
import { Router, ActivatedRoute } from '@angular/router';
import { LoginService } from '../login/login.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import {ChangeDetectionStrategy, Input} from '@angular/core';


@Component({
  selector: 'app-administracao',
  templateUrl: './administracao.component.html',
  styleUrls: ['./administracao.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})
export class AdministracaoComponent implements OnInit {
  @Input() trabalhadores: any[];
  page: number = 1;
  dtOptions: DataTables.Settings = { };
  trabalhador: Trabalhador = new Trabalhador();
  stVr: number;
  administracaoForm: FormGroup;
  filter: string = '';

  constructor(private administracaoService: AdministracaoService, private router: Router,
    private fb: FormBuilder, private _avRoute: ActivatedRoute,
    public loginService: LoginService, public datepipe: DatePipe) { }

  ngOnInit() {
    this.dtOptions = {
      // Use this attribute to enable the responsive extension
      pagingType: 'full_numbers',
      responsive: true,
      'language': {
          'emptyTable':     'Nenhum registro encontrado',
          'info':           'Mostrando de START até END de TOTAL registros',
          'infoEmpty':      'Mostrando 0 até 0 de 0 registros',
          'infoFiltered':   '(Filtrados de MAX registros)',
          'infoPostFix':    '',
          'thousands':      ',',
          'lengthMenu':     '',
          'loadingRecords': 'Carregando...',
          'processing':     'Processando...',
          'search':         'Pesquisar:',
          'zeroRecords':    'Nenhum registro encontrado',
          'paginate': {
              'first':      'Primeiro',
              'last':       'Último',
              'next':       'Próximo',
              'previous':   'Anterior'
          },
          'aria': {
              'sortAscending':  ': Ordenar colunas de forma ascendente',
              'sortDescending': ': Ordenar colunas de forma descendente'
          }
      }

  };
    this.creatFormGroup();
    this.administracaoService.get().subscribe(
      response => {
        this.newMethod(response);
      },
      error => { console.log(error)}
      )}

  pesquisar() {
    this.administracaoService.getTrabalhador(this.administracaoForm.get('sCPF').value).subscribe(
     response => {
       this.trabalhadores = [];
       this.trabalhadores.push(response);
       this.newMethod(this.trabalhadores);
     },
     error => { console.log(error)}
    )
  }

  private newMethod(response) {
    this.trabalhadores = response;
  }

  visualizar(cod: number) {
    this.router.navigate(['/consulta/' + cod]);
  }

  logout() {
    this.loginService.logout();
  }

  salvar(trabalhador: Trabalhador) {
    let descricao = trabalhador.sDescricao == '' ? ' ' : trabalhador.sDescricao; 
    this.administracaoService.update(descricao, trabalhador.iVerificado, trabalhador.iCodigo).subscribe(
      response => {
          alert('Alteração realizada com sucesso');
          this.ngOnInit();
      },
      error => { console.log('Erro: '+ error)}
    )
  }

  setVerificacao(val: number) {
    this.stVr = val;
  }

  enviarEmail(trabalhador: Trabalhador) {
    this.administracaoService.enviarEmail(trabalhador.sDescricao,trabalhador.sEmail).subscribe (
      response => {
        alert('Email enviado com sucesso!');
        this.ngOnInit();
      },
      error => { console.log('Erro: '+ error)}
    )}

  creatFormGroup() {
    this.administracaoForm = this.fb.group({
      sCPF: ['']
    })
  }

}
