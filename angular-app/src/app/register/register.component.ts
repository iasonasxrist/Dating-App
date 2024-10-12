import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import {
  FormGroup,
  Validators,
  FormBuilder,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Router } from '@angular/router';

import { AuthService } from '../_services/auth.service';
import { User } from '../_models/user';
import { AlertService } from '../_services/alert.service';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-register',

  standalone: true,
  imports: [FormsModule, NgClass, ReactiveFormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
})
export class RegisterComponent implements OnInit {
  @Output() cancelRequest = new EventEmitter();
  user: User;
  registerForm: FormGroup | undefined;
  bsConfig: Partial<BsDatepickerConfig> | undefined;

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
    if (this.registerForm?.valid) {
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

  cancel() {
    this.cancelRequest.emit(false);
  }
}
