import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { SharedService } from '../shared/shared.service';
import { UpdateProfileModel } from '../shared/shared.model';

let email = JSON.parse(localStorage.getItem('Email'));
@Component({
  selector: 'app-editprofile-component',
  templateUrl: './editprofile.component.html',
  styleUrls: ['./editprofile.component.css']
})


export class EditProfileComponent implements OnInit{
  public res
  public data = [];
  durationInSeconds = 10
  constructor(
    public service: SharedService,
    public _snackBar: MatSnackBar,
    public model : UpdateProfileModel)
    {
  }

  ngOnInit() {
    let data = this.service.getProfileInfo(email).subscribe((data: []) => {
      this.data = data;
      console.log(this.data);
    });
  }
  onSubmit(form: NgForm) {
    if(form.valid){
    this.service.updateProfile(form.value)
      .subscribe((data: UpdateProfileModel[]) => {
        this.res = data;
        console.log("let's see: "+this.res.message);
      this._snackBar.open(this.res.message,'Close', {
        duration: this.durationInSeconds * 1000,
      });
  },
  error => {
    this._snackBar.open('Error','Close',{
      duration: this.durationInSeconds * 1000
    });
    form.reset();
  });
  }
    console.log(form.value);
  }
}

