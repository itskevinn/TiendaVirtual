import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { UsuarioRegistroComponent } from './pages/usuario/usuario-registro/usuario-registro.component';
import { UsuarioConsultaComponent } from './pages/usuario/usuario-consulta/usuario-consulta.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    UsuarioRegistroComponent,
    UsuarioConsultaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
