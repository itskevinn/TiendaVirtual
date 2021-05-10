import { ProfesionalVentaService } from './../../../services/profesional-venta.service';
import { ProfesionalVenta } from './../../../models/profesionalVenta';
import { LiderAvaluo } from './../../../models/liderAvaluo';
import { LiderAvaluoService } from './../../../services/lider-avaluo.service';
import { Rol } from './../../../models/rol';
import { UsuarioService } from './../../../services/usuario.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Usuario } from 'src/app/models/usuario';
import { MessageService } from 'primeng/api';
import { RolService } from 'src/app/services/rol.service';
import { Persona } from 'src/app/models/persona';


@Component({
  selector: 'app-usuario-registro',
  templateUrl: './usuario-registro.component.html',
  styleUrls: ['./usuario-registro.component.css']
})
export class UsuarioRegistroComponent implements OnInit {
  roles: Rol[] = []
  usuario: Usuario;
  persona: Persona;
  nombreRol: string;
  formGroupUsuario: FormGroup;
  formGroupPersona: FormGroup;
  idRol: number;
  constructor(
    private liderAvaluoService: LiderAvaluoService, private profesionalVentaService: ProfesionalVentaService, private formBuilder: FormBuilder, private rolService: RolService, private usuarioService: UsuarioService, private messageService: MessageService
  ) { }
  ngOnInit(): void {
    this.obtenerRoles();
    this.crearFormulario();
  }
  obtenerRoles() {
    this.rolService.gets().subscribe((r) => {
      this.roles = r;
    })
  }
  crearFormulario() {
    this.usuario = new Usuario();
    this.usuario.rol = new Rol();
    this.usuario.nombreUsuario = '';
    this.usuario.contrasena = '';
    this.persona = new Persona();
    this.persona.apellido = '';
    this.persona.nombre = '';
    this.usuario.idRol = 0
    this.formGroupUsuario = this.formBuilder.group({
      nombreUsuario: [this.usuario.nombreUsuario, Validators.required],
      contrasena: [
        this.usuario.contrasena,
        Validators.required,
      ],
    })
    this.formGroupPersona = this.formBuilder.group({
      nombre: [this.persona.nombre, Validators.required],
      apellido: [this.persona.apellido, Validators.required]
    })
  }

  onSubmit() {
    if (this.formGroupUsuario.invalid) {
      this.mostrarToast("Aviso", "Complete todos los datos", "info");
    } else {
      this.registrar();
    }
  }

  get control() {
    return this.formGroupUsuario.controls;
  }
  cambiarRol(e) {
    let idRolString: string = e.target.value;
    let rolPropiedadesString = idRolString.split(" - ");
    let idRol = parseInt(rolPropiedadesString[0]);
    this.nombreRol = rolPropiedadesString[1];
    this.idRol = idRol;
  }
  registrar() {
    this.usuario = this.formGroupUsuario.value;
    this.usuario.rol = new Rol();
    this.usuario.idRol = this.idRol;
    this.persona = this.formGroupPersona.value;
    this.usuario.rol.idRol = this.idRol;
    this.usuario.rol.nombre = this.nombreRol;
    if (this.usuario.rol.nombre == 'Lider de Avalúos') {
      let liderAvaluo: LiderAvaluo;
      liderAvaluo = new LiderAvaluo();
      liderAvaluo.usuario = this.usuario;
      liderAvaluo.usuario.persona = this.persona;
      console.log(liderAvaluo);
      this.liderAvaluoService.post(liderAvaluo).subscribe((l) => {
        console.log(l);
        this.mostrarToast("Lider registrado", "Lider registrado exitosamente", "success")
      })
    }
    if (this.usuario.rol.nombre == 'Profesional de Ventas') {
      let profesionalVenta: ProfesionalVenta;
      profesionalVenta = new ProfesionalVenta();
      profesionalVenta.usuario = this.usuario;
      profesionalVenta.usuario.persona = this.persona;
      console.log(profesionalVenta);
      this.profesionalVentaService.post(profesionalVenta).subscribe((p) => {
        console.log(p);
        this.mostrarToast("Profesional registrado", "Profesional registrado exitosamente", "success")
      })
    }
  }
  mostrarToast(titulo: string, mensaje: string, tipo: string) {
    this.messageService.add({ key: 'tl', severity: tipo, summary: titulo, detail: mensaje, life: 2000 });
  }
}
