import { AutentificacionLiderAvaluosGuard } from './guard/autentificacion-lider-avaluos.guard';
import { InicioComponent } from './pages/inicio/inicio.component';
import { AutentificacionGuard } from './guard/autentificacion.guard';
import { LoginComponent } from './pages/login/login.component';
import { CarritoDeComprasComponent } from './components/carrito-de-compras/carrito-de-compras.component';
import { VentaConsultaComponent } from './pages/venta/venta-consulta/venta-consulta.component';
import { CompraRegistroComponent } from './pages/compra/compra-registro/compra-registro.component';
import { CompraConsultaComponent } from './pages/compra/compra-consulta/compra-consulta.component';
import { ProductoRegistroComponent } from './pages/producto/producto-registro/producto-registro.component';
import { ProductoConsultaComponent } from './pages/producto/producto-consulta/producto-consulta.component';
import { ProveedorRegistroComponent } from './pages/proveedor/proveedor-registro/proveedor-registro.component';
import { ProveedorConsultaComponent } from './pages/proveedor/proveedor-consulta/proveedor-consulta.component';
import { UsuarioConsultaComponent } from './pages/usuario/usuario-consulta/usuario-consulta.component';
import { UsuarioRegistroComponent } from './pages/usuario/usuario-registro/usuario-registro.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VentaRegistroComponent } from './pages/venta/venta-registro/venta-registro.component';
import { AutentificacionVentasGuard } from './guard/autentificacion-ventas.guard';


const routes: Routes = [
  { path: 'Inicio', component: InicioComponent, canActivate: [AutentificacionGuard] },
  { path: 'RegistrarUsuario', component: UsuarioRegistroComponent, canActivate: [AutentificacionLiderAvaluosGuard] },
  { path: 'Usuarios', component: UsuarioConsultaComponent, canActivate: [AutentificacionLiderAvaluosGuard] },
  { path: 'Proveedores', component: ProveedorConsultaComponent, canActivate: [AutentificacionLiderAvaluosGuard] },
  { path: 'RegistrarProveedor', component: ProveedorRegistroComponent, canActivate: [AutentificacionLiderAvaluosGuard] },
  { path: 'Productos', component: ProductoConsultaComponent, canActivate: [AutentificacionGuard] },
  { path: 'RegistrarProducto', component: ProductoRegistroComponent, canActivate: [AutentificacionLiderAvaluosGuard] },
  { path: 'Compras', component: CompraConsultaComponent, canActivate: [AutentificacionVentasGuard] },
  { path: 'RegistrarCompra', component: CompraRegistroComponent, canActivate: [AutentificacionVentasGuard] },
  { path: 'Ventas', component: VentaConsultaComponent, canActivate: [AutentificacionVentasGuard] },
  { path: 'RegistrarVenta', component: VentaRegistroComponent, canActivate: [AutentificacionVentasGuard] },
  { path: 'Login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
