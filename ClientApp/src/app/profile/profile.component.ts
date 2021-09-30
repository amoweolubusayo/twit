import { HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { SharedService } from '../shared/shared.service';

let email = JSON.parse(localStorage.getItem('Email'));
@Component({
  selector: 'app-profile-component',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})


export class ProfileComponent implements OnInit{
  public data = [];
  constructor(
    public service: SharedService,
    public _snackBar: MatSnackBar)
    {
  }

  ngOnInit() {
    let data = this.service.getProfileInfo(email).subscribe((data: []) => {
      this.data = data;
      console.log(this.data);
    });
  }
}

