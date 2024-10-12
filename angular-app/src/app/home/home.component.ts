import {Component, OnInit} from '@angular/core';
import {NgIf} from "@angular/common";
import {HttpClient} from "@angular/common/http";
import {RegisterComponent} from "../register/register.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    NgIf,
    RegisterComponent
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
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
