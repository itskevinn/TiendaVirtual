import { UsuarioConsultaComponent } from './pages/usuario/usuario-consulta/usuario-consulta.component';
import { UsuarioRegistroComponent } from './pages/usuario/usuario-registro/usuario-registro.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  { path: 'RegistrarUsuario', component: UsuarioRegistroComponent },
  { path: 'Usuarios', component: UsuarioConsultaComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
