import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { ChipsModule } from 'primeng/chips';
import { ProveedorConsultaComponent } from './pages/proveedor/proveedor-consulta/proveedor-consulta.component';
import { MessageService } from 'primeng/api';
import { UsuarioRegistroComponent } from './pages/usuario/usuario-registro/usuario-registro.component';
import { UsuarioConsultaComponent } from './pages/usuario/usuario-consulta/usuario-consulta.component';
import { RouterModule } from '@angular/router';
import { PanelModule } from 'primeng/panel';
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
import { CardModule } from 'primeng/card';
import { ProveedorRegistroComponent } from './pages/proveedor/proveedor-registro/proveedor-registro.component';
import { ProductoConsultaComponent } from './pages/producto/producto-consulta/producto-consulta.component';
import { ProductoRegistroComponent } from './pages/producto/producto-registro/producto-registro.component';
import { VentaConsultaComponent } from './pages/venta/venta-consulta/venta-consulta.component';
import { CompraConsultaComponent } from './pages/compra/compra-consulta/compra-consulta.component';
import { CompraRegistroComponent } from './pages/compra/compra-registro/compra-registro.component';
import { VentaRegistroComponent } from './pages/venta/venta-registro/venta-registro.component';
import { ProductoCardComponent } from './components/producto-card/producto-card.component';
import { PageHeaderComponent } from './components/page-header/page-header.component';
import { CarritoDeComprasComponent } from './components/carrito-de-compras/carrito-de-compras.component';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import { MenuItem } from 'primeng/api';
import { LoginComponent } from './pages/login/login.component';
import { InicioComponent } from './pages/inicio/inicio.component';
import { EditarProductoComponent } from './pages/producto/editar-producto/editar-producto.component';
import { ProveedorEdicionComponent } from './pages/proveedor/proveedor-edicion/proveedor-edicion.component';
import { UsuarioEdicionComponent } from './pages/usuario/usuario-edicion/usuario-edicion.component';
import { ProductoDetalleComponent } from './pages/producto/producto-detalle/producto-detalle.component';
import { ProveedorDetalleComponent } from './pages/proveedor/proveedor-detalle/proveedor-detalle.component';
import { UsuarioDetalleComponent } from './pages/usuario/usuario-detalle/usuario-detalle.component';
import { BackButtonComponent } from './components/back-button/back-button.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    UsuarioConsultaComponent,
    UsuarioRegistroComponent,
    ProveedorRegistroComponent,
    ProveedorConsultaComponent,
    ProductoConsultaComponent,
    ProductoRegistroComponent,
    VentaConsultaComponent,
    CompraConsultaComponent,
    CompraRegistroComponent,
    VentaRegistroComponent,
    ProductoCardComponent,
    PageHeaderComponent,
    CarritoDeComprasComponent,
    LoginComponent,
    InicioComponent,
    EditarProductoComponent,
    ProveedorEdicionComponent,
    UsuarioEdicionComponent,
    ProductoDetalleComponent,
    ProveedorDetalleComponent,
    UsuarioDetalleComponent,
    BackButtonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ButtonModule,
    MenuModule,
    RouterModule,
    PanelModule,
    InputNumberModule,
    InputTextareaModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    ToastModule,
    DropdownModule,
    BrowserModule,
    ChipsModule,
    BrowserAnimationsModule,
    CardModule
  ],
  providers: [MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
