import { AuthGuard } from './guards/auth.guard';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//import { LoginComponent } from './component/login/login.component';
import { DependenteComponent } from './dependente/dependente.component';
import { CadastroComponent } from './cadastro/cadastro.component';
import { EstagiarioComponent } from './estagiario/estagiario.component';
import { CedidoComponent } from './cedido/cedido.component';
import { LoginComponent } from './login/login.component';
import { ConsultaComponent } from './consulta/consulta.component';
import { AdministracaoComponent } from './administracao/administracao.component';

const routes: Routes = [
  { path: 'cadastro/:iCodTrabalhador', component: CadastroComponent, canActivate: [AuthGuard]},
  { path: 'dependente', component: DependenteComponent, canActivate: [AuthGuard] },
  { path: 'dependente/:iCodTrabalhador', component: DependenteComponent, canActivate: [AuthGuard] },
  { path: 'estagiario', component: EstagiarioComponent, canActivate: [AuthGuard] },
  { path: 'estagiario/:iCodTrabalhador', component: EstagiarioComponent, canActivate: [AuthGuard] },
  { path: 'cedido', component: CedidoComponent, canActivate: [AuthGuard] },
  { path: 'cedido/:iCodTrabalhador', component: CedidoComponent, canActivate: [AuthGuard] },
  { path: 'consulta/:iCodTrabalhador', component: ConsultaComponent, canActivate: [AuthGuard] },
  { path: 'administracao', component: AdministracaoComponent, canActivate: [AuthGuard] },
  { path: '', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
