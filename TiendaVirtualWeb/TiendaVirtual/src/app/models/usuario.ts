import { Persona } from "./persona";
import { Rol } from "./rol";

export class Usuario {
  idRol: number;
  contrasena: string;
  nombreUsuario: string;
  idUsuario: number;
  rol: Rol;
  persona: Persona
}

