import { Component, NgModule } from '@angular/core';
import {  NgForm } from '@angular/forms';
import { SharedService } from '../shared/shared.service';
import { MatSliderModule } from '@angular/material/slider';
import { ExploreModel, LikeModel } from '../shared/shared.model';
import { MatSnackBar } from '@angular/material';

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
      console.log(this.data);
    });
  }
  onSubmit() {
    this._snackBar.open('Liked','Close', {
      duration: this.durationInSeconds * 1000,
    });
  }
}
