import { Component, NgModule } from '@angular/core';
import {  NgForm } from '@angular/forms';
import { SharedService } from '../shared/shared.service';
import { MatSliderModule } from '@angular/material/slider';
import { ExploreModel, LikeInfoModel, LikeModel } from '../shared/shared.model';
import { MatSnackBar } from '@angular/material';

let id = JSON.parse(localStorage.getItem('Id'));
@NgModule ({
  imports: [
    MatSliderModule,
  ]
})

@Component({
  selector: 'app-explore-component',
  templateUrl: './explore.component.html',
   styleUrls: ['./explore.component.css']
})
export class ExploreComponent{
   public data = [];
   public resp = [];
   public res
   public user
   durationInSeconds = 10
   public likeModel: {}
  constructor(
    public service: SharedService,
    public _snackBar: MatSnackBar,)
    {
  }
  ngOnInit() {
    let data = this.service.explore().subscribe((data: ExploreModel[]) => {
      this.data = data;
      this.user = id
      console.log(this.data);
    });
  }
  onSubmit(form: NgForm) {
    if(form.valid){
    this.service.likePost(form.value)
      .subscribe((data: LikeModel[]) => {
        this.res = data;
        console.log("let's see: "+this.res.message);
      this._snackBar.open(this.res.message,'Close', {
        duration: this.durationInSeconds * 1000,
      });
  },
  error => {
    this._snackBar.open('An error occured','Close',{
      duration: this.durationInSeconds * 1000
    });
    form.reset();
  });
  }
    console.log(form.value);
  }
}
