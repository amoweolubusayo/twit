import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders,HttpHandler, HttpEvent, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs';
import {RegisterModel, UpdateProfileModel, ExploreModel, LikeModel, LoginModel, PostModel, LikeInfoModel} from '../shared/shared.model'


let token = JSON.parse(localStorage.getItem('TokenInfo'));
console.log(JSON.parse(localStorage.getItem('TokenInfo')));

var header = {
  headers: new HttpHeaders()
    .set('Authorization',  `Bearer ${token}`)
}

@Injectable({
    providedIn: 'root'
})

export class SharedService {
    readonly APIUrl = "http://localhost:5000/api";
    constructor(private http: HttpClient) {}
    register(model: RegisterModel) {
      return this.http.post (this.APIUrl + '/User/register', model);
    }
    login(model: LoginModel){
      return this.http.post (this.APIUrl + '/User/login',model);
    }
     explore(): Observable < ExploreModel[] > {
       return this.http.get < ExploreModel[] > (this.APIUrl + '/Posts/getAllPosts');
     }
     getPostsByUser(id): Observable < ExploreModel[] > {
      return this.http.get < ExploreModel[] > (this.APIUrl + '/Posts/getpostByUserId'+'?userId=' + id,header);
    }
    getNumberOfLikes(postId): Observable < LikeInfoModel[] > {
      return this.http.get < LikeInfoModel[] > (this.APIUrl + '/Posts/getLikesPerPost'+'?postId=' + postId,header);
    }
     getProfileInfo(email: string): Observable < any > {
      return this.http.get < any > (this.APIUrl + '/User/profileinfo'+ '?email=' + email,header);
    }
     updateProfile(model: UpdateProfileModel) {
      return this.http.post (this.APIUrl + '/User/updateprofile', model,header);
    }
    likePost(model: LikeModel) {
      return this.http.post (this.APIUrl + '/Posts/likepost', model, header);
    }
    addPost(model: PostModel) {
      return this.http.post (this.APIUrl + '/Posts/addpost', model, header);
    }
    deletePost(model: LikeModel) {
      return this.http.post (this.APIUrl + '/Posts/deletepost', model, header);
    }

}
