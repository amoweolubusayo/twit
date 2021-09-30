import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SharedService } from '../shared/shared.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ExploreModel } from '../shared/shared.model';

let id = JSON.parse(localStorage.getItem('Id'));
@Component({
  selector: 'app-personalposts-component',
  templateUrl: './personalposts.component.html',
  styleUrls: ['./personalposts.component.css']
})
export class PersonalPostsComponent{
  public data = [];
  constructor(
    public service: SharedService)
    {
  }
  ngOnInit() {
    let data = this.service.getPostsByUser(id).subscribe((data: ExploreModel[]) => {
      this.data = data;
      console.log(this.data);
    });
  }
}
