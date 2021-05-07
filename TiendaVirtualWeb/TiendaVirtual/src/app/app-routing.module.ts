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


const routes: Routes = [
  { path: 'RegistrarUsuario', component: UsuarioRegistroComponent },
  { path: 'Usuarios', component: UsuarioConsultaComponent },
  { path: 'Proveedores', component: ProveedorConsultaComponent },
  { path: 'RegistrarProveedor', component: ProveedorRegistroComponent },
  { path: 'Productos', component: ProductoConsultaComponent },
  { path: 'RegistrarProducto', component: ProductoRegistroComponent },
  { path: 'Compras', component: CompraConsultaComponent },
  { path: 'RegistrarCompra', component: CompraRegistroComponent },
  { path: 'Ventas', component: VentaConsultaComponent },
  { path: 'RegistrarVenta', component: VentaRegistroComponent },
  { path: 'Carrito', component: CarritoDeComprasComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
