import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Rol } from 'src/app/models/rol';
import { RolService } from 'src/app/services/rol.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-rol',
  templateUrl: './rol.component.html',
  styleUrls: ['./rol.component.css']
})
export class RolComponent implements OnInit {

  roles: Rol[] = []
  rol: Rol;
  formGroup: FormGroup;
  idRol: number;
  constructor(
    private formBuilder: FormBuilder, private rolService: RolService, private messageService: MessageService
  ) { }
  ngOnInit(): void {
    this.obtenerRoles();
    this.rol = new Rol();
    this.crearFormulario();
  }
  obtenerRoles() {
    this.rolService.gets().subscribe((r) => {
      this.roles = r;
    })
  }
  crearFormulario() {
    this.rol.nombre = ''
    this.formGroup = this.formBuilder.group({
      nombre: [this.rol.nombre, Validators.required],
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
  registrar() {
    this.rol = this.formGroup.value;
    this.rolService.post(this.rol).subscribe((r) => {
      this.rol = r;
      this.mostrarToast("Rol registrado", "Rol registrado con éxito", "success");
    })
  }
  mostrarToast(titulo: string, mensaje: string, tipo: string) {
    this.messageService.add({ key: 'tl', severity: tipo, summary: titulo, detail: mensaje, life: 2000 });
  }
}
