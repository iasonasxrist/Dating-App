import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {NgClass} from "@angular/common";
import {Router} from "@angular/router";
import {User} from "../_models/user";
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import {AuthService} from "../_services/auth.service";
import {AlertService} from "../_services/alert.service";
@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    FormsModule,
    NgClass,
    ReactiveFormsModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  @Output() cancelRequest = new EventEmitter();
  user:User;
  registerForm: FormGroup;
  bsConfig:Partial<BsDatepickerConfig>;

constructor(
  private authService:AuthService,
  private alertService :AlertService,
  private formBuilder: FormBuilder,
  private router :Router
) {

}
ngOnInit() {
  this.bsConfig = {
    containerClass:'theme-red',
    dateInputFormat:"DD-MM-YYYY"
  }
  this.createRegisterForm();
}

createRegisterForm(){
  this.registerForm = this.formBuilder.group({
    gender: ['male'],
    username: ['', Validators.required],
    knownAs: ['', Validators.required],
    dateOfBirth: [null, Validators.required],
    city: ['', Validators.required],
    country: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
    confirmPassword: ['', Validators.required]
  }, {validator: this.passwordMatchValidator})
}

  passwordMatchValidator(g:FormGroup){
   return g.get('password')?.value === g.get('confirmPassword')?.value ? null : {'mismatch' : true};
  }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(
        ()=>this.alertService.success("Registration Successful"),
        (error)=> this.alertService.error(error),
        ()=> this.authService.login(this.user).subscribe(
          ()=> this.router.navigate(['/members']),
          (error)=> this.alertService.error(error),
        )
      );
    }
  }

  cancel(){
   this.cancelRequest.emit(false);
  }
}
