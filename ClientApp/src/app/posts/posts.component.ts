import { Component , OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgForm } from '@angular/forms';
import { SharedService } from '../shared/shared.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PostModel } from '../shared/shared.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-posts-component',
  templateUrl: './posts.component.html'
})
export class PostsComponent implements OnInit{
  public res
  public user
  durationInSeconds = 5;
  constructor(
    public service: SharedService,
    public _snackBar: MatSnackBar,
    public router : Router)
    {
  }

ngOnInit() {
  let email = JSON.parse(localStorage.getItem('Email'));
  this.user = email
  console.log("check here"+ this.user)
}
onSubmit(form: NgForm) {
  if(form.valid){

  this.service.addPost(form.value).subscribe((data: PostModel[]) => {
    this.res = data;
    console.log("let's see: "+this.res.message);
    this._snackBar.open(this.res.message,'Close', {
      duration: this.durationInSeconds * 1000,
    });
    this.router.navigate(["/explore"]);
    form.reset();
},
error => {
  this._snackBar.open('Failed to post','Close',{
    duration: this.durationInSeconds * 1000
  });
});
}
  console.log(form.value);

}
}
