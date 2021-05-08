import { Component, Input, OnInit } from '@angular/core';
import { Location } from '@angular/common'

@Component({
  selector: 'app-page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.css']
})
export class PageHeaderComponent implements OnInit {
  @Input() titulo: string;
  @Input() redireccionamiento: string;
  constructor(private location: Location) { }

  ngOnInit(): void {
  }
  volver() {
    this.location.back();
  }
}
