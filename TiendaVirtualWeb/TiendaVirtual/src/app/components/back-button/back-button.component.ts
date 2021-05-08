import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-back-button',
  templateUrl: './back-button.component.html',
  styles: [
  ]
})
export class BackButtonComponent implements OnInit {

  constructor(private location: Location) { }

  ngOnInit(): void {
  }
  volver() {
    this.location.back();
  }
}
