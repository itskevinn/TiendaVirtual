import { Factura } from './factura';
import { Usuario } from './usuario';
export class Interesado {
  rol: string;
  usuario: Usuario;
  facturas: Factura[];
  idUsuario: string;
  id: string;
}