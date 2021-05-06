import { ProductoRegistroComponent } from './pages/producto/producto-registro/producto-registro.component';
import { ProductoConsultaComponent } from './pages/producto/producto-consulta/producto-consulta.component';
import { ProveedorRegistroComponent } from './pages/proveedor/proveedor-registro/proveedor-registro.component';
import { ProveedorConsultaComponent } from './pages/proveedor/proveedor-consulta/proveedor-consulta.component';
import { UsuarioConsultaComponent } from './pages/usuario/usuario-consulta/usuario-consulta.component';
import { UsuarioRegistroComponent } from './pages/usuario/usuario-registro/usuario-registro.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  { path: 'RegistrarUsuario', component: UsuarioRegistroComponent },
  { path: 'Usuarios', component: UsuarioConsultaComponent },
  { path: 'Proveedores', component: ProveedorConsultaComponent },
  { path: 'RegistrarProveedor', component: ProveedorRegistroComponent },
  { path: 'Productos', component: ProductoConsultaComponent },
  { path: 'RegistrarProducto', component: ProductoRegistroComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
