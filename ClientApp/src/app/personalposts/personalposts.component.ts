import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SharedService } from '../shared/shared.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ExploreModel, LikeModel } from '../shared/shared.model';

let id = JSON.parse(localStorage.getItem('Id'));
@Component({
  selector: 'app-personalposts-component',
  templateUrl: './personalposts.component.html',
  styleUrls: ['./personalposts.component.css']
})
export class PersonalPostsComponent{
  public data = [];
  public user
  public res
  durationInSeconds = 10;
  constructor(
    public service: SharedService,
    public _snackBar: MatSnackBar)
    {
  }
  ngOnInit() {
    let data = this.service.getPostsByUser(id).subscribe((data: ExploreModel[]) => {
      this.data = data;
      this.user = id
      console.log(this.data);
    });
  }
  onSubmit(form: NgForm) {

    if(form.valid){
    this.service.deletePost(form.value)
      .subscribe((data: LikeModel[]) => {
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
