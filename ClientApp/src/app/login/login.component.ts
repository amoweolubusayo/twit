import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';
import { LoginModel } from '../shared/shared.model';
import { SharedService } from '../shared/shared.service';


@Component({
  selector: 'app-login-component',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit{
  public res
  durationInSeconds = 10;
  constructor(
    public service: SharedService,
    public model : LoginModel,
    public _snackBar: MatSnackBar,
    public router : Router)
    {
  }
  ngOnInit(): void {
  }
  onSubmit(form: NgForm) {
    if(form.valid){
    this.service.login(form.value)
      .subscribe((data: LoginModel[]) => {
        this.res = data;
        console.log("let's see: "+this.res.token);
      this._snackBar.open('Login successful','Close', {
        duration: this.durationInSeconds * 1000,
      });
      this.router.navigate(["/explore"],this.res.token);
      localStorage.setItem('TokenInfo', JSON.stringify(this.res.token));
      localStorage.setItem('Email', JSON.stringify(this.res.email));
      localStorage.setItem('Id', JSON.stringify(this.res.id));
    form.reset();
  },
  error => {
    this._snackBar.open('Wrong username or password','Close',{
      duration: this.durationInSeconds * 1000
    });
    form.reset();
  });
  }
    console.log(form.value);
  }
}
