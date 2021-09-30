export class RegisterModel {
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
constructor() {
    this.firstName = "";
    this.lastName = "";
    this.userName = "";
    this.email = "";
    this.password = "";
  }
}
export class LoginModel {
  email: string;
  password: string;
constructor() {
    this.email = "";
    this.password = "";
  }
}
export class ExploreModel {
  status: boolean;
  message: string;
  data: {
   postId: Number;
   content : string;
   isDeleted: boolean;
   isLiked: boolean;
   postedBy: string
  }
constructor() {
    this.status = false;
    this.message = "";
    this.data = {
      postId : 0,
      content : "",
      isDeleted: false,
      isLiked :false,
      postedBy : ""
    }
  }
}
export class RegisterSuccessModel {
  status: boolean;
  message: string
constructor() {
    this.status = false;
    this.message = "";
  }
}
export class ProfileInfoModel {
  status: boolean;
  message: string;
  data: {
   firstName: string;
   lastName : string;
   userName: string;
   Email: string;
  }
constructor() {
    this.status = false;
    this.message = "";
    this.data = {
      firstName : "",
      lastName : "",
      userName: "",
      Email : "",
    }
  }
}
export class UpdateProfileModel {
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
constructor() {
    this.firstName = "";
    this.lastName = "";
    this.userName = "";
    this.email = "";
}
}
export class LikeModel {
  userId: Number;
  postId: Number;
constructor() {
    this.postId = 0;
    this.userId = 0;
}
}
export class PostModel {
  email: string;
  content: string;
constructor() {
    this.email = "";
    this.content = "";
  }
}


