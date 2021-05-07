import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MensajeService {
  subject = new Subject();
  constructor() { }
  enviarMensaje(detalle) {
    this.subject.next(detalle);
  }
  recibirMensaje() {
    return this.subject.asObservable();
  }
}
