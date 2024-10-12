import {environment} from "../../environments/environment";
import {User} from "../_models/user";
import {BehaviorSubject, map} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable({
  providedIn: "root"
})
export class AuthService {
  baseUrl = environment.apiUrl + "/auth";
  jwtHelper = new JwtHelperService();
  decodedToken : any;
  currentUser : User;
  photoUrl = new BehaviorSubject<string>("../../assets/user.png")
  currentPhotUrl = this.photoUrl.asObservable();

  constructor(private http: HttpClient) {
  }

  changeMemberPhoto(photoUrl: string){
    this.photoUrl.next(photoUrl);
  }
  login(model: any) {
    return this.http.post(this.baseUrl + 'login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            sessionStorage.setItem('token', user.token);
            sessionStorage.setItem('user', JSON.stringify(user.user));
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            this.currentUser = user.user;
            this.changeMemberPhoto(this.currentUser.photoUrl);
          }
        })
      );
  }

  register(user: User) {
    return this.http.post(this.baseUrl + 'register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  roleMatch(allowedRoles: Array<String>): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<String>;
    allowedRoles.forEach(element => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }
}
