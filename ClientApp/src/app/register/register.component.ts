import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgForm } from '@angular/forms';
import { SharedService } from '../shared/shared.service';
import { RegisterModel, RegisterSuccessModel } from '../shared/shared.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register-component',
  templateUrl: './register.component.html',
})

export class RegisterComponent implements OnInit {
  public reg
  durationInSeconds = 10;
  constructor(
    public service: SharedService,
    public model : RegisterModel,
    public _snackBar: MatSnackBar,
    public router : Router)
    {
  }
  ngOnInit(): void {
    }
    onSubmit(form: NgForm) {
      if(form.valid){
      this.service.register(form.value)
        .subscribe((data: RegisterModel[]) => {
          this.reg = data;
          console.log("let's see: "+this.reg.message);
        this._snackBar.open(this.reg.message,'Close', {
          duration: this.durationInSeconds * 1000,
        });
        this.router.navigate(["/login"]);
      form.reset();
    },
    error => {
      this._snackBar.open('User already exists','Close',{
        duration: this.durationInSeconds * 1000
      });
      form.reset();
    });
    }
      console.log(form.value);
    }
}

