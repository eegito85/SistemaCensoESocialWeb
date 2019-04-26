import { AdministracaoComponent } from './administracao/administracao.component';
import { ConsultaComponent } from './consulta/consulta.component';
import { AuthGuard } from './guards/auth.guard';

import { HttpModule } from '@angular/http';
import { DatePipe } from '@angular/common';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { DataTablesModule } from 'angular-datatables';

import { AppComponent } from './app.component';
import { CadastroComponent } from './cadastro/cadastro.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { DependenteComponent } from './dependente/dependente.component';
import { EstagiarioComponent } from './estagiario/estagiario.component';
import { CedidoComponent } from './cedido/cedido.component';
import { LoginComponent } from './login/login.component';
import { AppRoutingModule } from './router.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {CalendarModule} from 'primeng/calendar';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { AutoCompleteModule } from 'primeng/autocomplete';
import {InputMaskModule} from 'primeng/inputmask';
import { NgSelectModule } from '@ng-select/ng-select';
import { ArquivosComponent } from './arquivos/arquivos.component';
import {NgxMaskModule} from 'ngx-mask';
import { CpfCnpjModule } from 'ng2-cpf-cnpj';
import { AuthInterceptorProvider } from './interceptors/auth.interceptor';
import {NgxPaginationModule} from 'ngx-pagination';
import {StringFilterPipe} from './string-filter.pipe';
import { Ng2SearchPipeModule } from 'ng2-search-filter';

@NgModule({
  declarations: [
    AppComponent,
    CadastroComponent,
    DependenteComponent,
    EstagiarioComponent,
    CedidoComponent,
    LoginComponent,
    ArquivosComponent,
    ConsultaComponent,
    AdministracaoComponent,
    StringFilterPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgbModule.forRoot(),
    CalendarModule,
    BrowserModule,
    BrowserAnimationsModule,
    AutoCompleteModule,
    InputMaskModule,
    NgSelectModule,
    NgxMaskModule.forRoot(),
    CpfCnpjModule,
    FormsModule,
    DataTablesModule,
    NgxPaginationModule,
    Ng2SearchPipeModule
  ],
  providers: [DatePipe, AuthGuard, AuthInterceptorProvider],
  bootstrap: [AppComponent]

})
export class AppModule { }
