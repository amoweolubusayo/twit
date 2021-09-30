import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { ExploreComponent } from './explore/explore.component';
import { PostsComponent } from './posts/posts.component';
import { PersonalPostsComponent } from './personalposts/personalposts.component';
import { ProfileComponent } from './profile/profile.component';
import { SharedService } from './shared/shared.service';
import { RegisterModel, ExploreModel, LoginModel } from './shared/shared.model';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule, MatSidenavModule,MatCardModule, MatFormFieldModule, MatIconModule } from '@angular/material';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';




@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    RegisterComponent,
    LoginComponent,
    ExploreComponent,
    PostsComponent,
    PersonalPostsComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'register', component: RegisterComponent },
      { path: 'login', component: LoginComponent },
      { path: 'explore', component: ExploreComponent },
      { path: 'post', component: PostsComponent},
      { path: 'personalpost', component: PersonalPostsComponent},
      { path: 'profile', component: ProfileComponent}
    ]),
    BrowserAnimationsModule,
    MatButtonModule,
    MatSidenavModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatSnackBarModule
  ],
  providers: [SharedService,RegisterModel,ExploreModel,LoginModel],
  bootstrap: [AppComponent]
})
export class AppModule { }
