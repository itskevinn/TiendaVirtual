import { UsuarioService } from './../../../services/usuario.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Usuario } from 'src/app/models/usuario';
import { MessageService } from 'primeng/api';


@Component({
  selector: 'app-usuario-registro',
  templateUrl: './usuario-registro.component.html',
  styleUrls: ['./usuario-registro.component.css']
})
export class UsuarioRegistroComponent implements OnInit {
  roles: string[] = ["Interesado", "Lider de Avalúos", "Profesional de Ventas"]
  usuario: Usuario;
  formGroup: FormGroup;
  rol: string;
  constructor(
    private formBuilder: FormBuilder, private usuarioService: UsuarioService, private messageService: MessageService
  ) { }
  ngOnInit(): void {
    this.usuario = new Usuario()
    this.crearFormulario()
  }
  crearFormulario() {
    this.usuario._usuario = ''
    this.usuario.rol = ''
    this.usuario.contrasena = ''
    this.usuario.id = ''
    this.formGroup = this.formBuilder.group({
      _usuario: [this.usuario._usuario, Validators.required],
      contrasena: [
        this.usuario.contrasena,
        Validators.required,
      ],
    })
  }
  onSubmit() {
    if (this.formGroup.invalid) {
      this.mostrarToast("Aviso", "Complete todos los datos", "info");

    } else {
      this.registrar();
    }
  }

  get control() {
    return this.formGroup.controls;
  }
  cambiarRol(e) {
    this.rol = e.target.value;
  }
  registrar() {
    this.usuario = this.formGroup.value;
    this.usuario.rol = this.rol;
    this.usuarioService.post(this.usuario).subscribe((r) => {
      if (r != null) {
        this.usuario = r;
        this.formGroup.reset();
        this.mostrarToast("Registro exitoso", "Usuario registrado exitosamente", "success");
      }
    });
  }
  mostrarToast(titulo: string, mensaje: string, tipo: string) {
    this.messageService.add({ key: 'tl', severity: tipo, summary: titulo, detail: mensaje, life: 2000 });
  }
}
