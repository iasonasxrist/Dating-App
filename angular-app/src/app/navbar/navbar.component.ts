import {Component, OnInit} from '@angular/core';
import {RouterLink, RouterLinkActive} from "@angular/router";
import {NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    NgIf,
    FormsModule
  ],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit{
  model : any = {};
  photoUrl: string = "https://google.com";

  ngOnInit() {
  }

  login(){
    console.log('Logging with ', this.model.username, this.model.password)
  }

  loggedIn() {
    // return !!localStorage.getItem('token') || true;
    return true;
  }

  logout() {
    console.log("logged out!")
    // localStorage.removeItem('token');
  }
}
