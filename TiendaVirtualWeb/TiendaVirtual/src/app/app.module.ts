import { MessageService } from 'primeng/api';
import { UsuarioRegistroComponent } from './pages/usuario/usuario-registro/usuario-registro.component';
import { UsuarioConsultaComponent } from './pages/usuario/usuario-consulta/usuario-consulta.component';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { DropdownModule } from 'primeng/dropdown';
import { AppComponent } from './app.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ToastModule } from 'primeng/toast';
import { ProveedorRegistroComponent } from './pages/proveedor/proveedor-registro/proveedor-registro.component';
import { ProveedorConsultaComponent } from './pages/proveedor/proveedor-consulta/proveedor-consulta.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    UsuarioConsultaComponent,
    UsuarioRegistroComponent,
    ProveedorRegistroComponent,
    ProveedorConsultaComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    ToastModule,
    DropdownModule,
    BrowserModule,
    BrowserAnimationsModule,
  ],
  providers: [MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
