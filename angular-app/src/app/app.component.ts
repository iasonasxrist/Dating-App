import {Component, OnInit} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {NavbarComponent} from "./navbar/navbar.component";
import {HomeComponent} from "./home/home.component";
import {AuthService} from "./_services/auth.service";
import {JwtHelperService} from "@auth0/angular-jwt";
import {User} from "./_models/user";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title = 'Dating-App';
  jwtHelper = new JwtHelperService();

constructor(private authService : AuthService) {
}
  ngOnInit() {
 const token = localStorage.getItem('token')
     if (token){
       this.authService.decodedToken = this.jwtHelper.decodeToken(token)
     }
     const user:User = JSON.parse(localStorage.getItem('user'));
     if (user){
       this.authService.currentUser = user;
       this.authService.changeMemberPhoto(user.photoUrl);
     }
  }
}
