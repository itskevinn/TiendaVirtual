import { Producto } from "./producto";

export class Proveedor {
  tipoDocumento: string;
  documento: string;
  nombre: string;
  productos: Producto[];
  idProveedor: number;
}
