import {Component, OnInit} from '@angular/core';
import {NgIf} from "@angular/common";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    NgIf
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  registerMode = false;
  constructor() {
  }
  ngOnInit(){

  }

  register(){
    this.registerMode = true;
  }

  cancelRegisterMode(registerMode:boolean){
    this.registerMode = registerMode;
  }
}
