
import { Component, NgModule } from '@angular/core';
import {  NgForm } from '@angular/forms';
import { SharedService } from '../shared/shared.service';
import { MatSliderModule } from '@angular/material/slider';
import { ExploreModel, LikeInfoModel, LikeModel, UsersModel } from '../shared/shared.model';
import { MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';

let id = JSON.parse(localStorage.getItem('Id'));
@Component({
  selector: 'app-users-component',
  templateUrl: './users.component.html',
   styleUrls: ['./users.component.css']
})
export class UsersComponent{
  public data = [];
  public resp = [];
  public res
  public user
  durationInSeconds = 10
  public likeModel: {}
 constructor(
   public service: SharedService,
   public _snackBar: MatSnackBar,
   public router : Router)
   {
 }
 ngOnInit() {
   let data = this.service.getAllUsers().subscribe((data: UsersModel[]) => {
     this.data = data;
     this.user = id
     console.log(this.data);
   });
 }
}
