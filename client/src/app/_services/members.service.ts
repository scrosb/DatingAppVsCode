import { HttpClient, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of, pipe } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import {Member} from '../_models/member';
import { PaginatedResult } from '../_models/pagination';
import { User } from '../_models/user';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';


// const httpOptions = {
//   headers: new HttpHeaders({
//     Authorization: 'Bearer '+ JSON.parse(localStorage.getItem('user'))?.token
//   })
// }

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  //use this as our members state. 
  members: Member[] = [];
  //Map Stores values in key- value formant. 
  memberCache = new Map();
  user:User;
  userParams: UserParams;
  

  constructor(private http: HttpClient, private accountService:AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user =>{
      this.user = user;
      //Creating a new
      this.userParams = new UserParams(user);

    })
   }

   //this is used to keep filters between page loads. 
   getUserParams(){
     return this.userParams;
   }

   setUserParams(params:UserParams){
     this.userParams = params;
   }

   resetUserParams(){
     this.userParams = new UserParams(this.user);
     return this.userParams;
   }
  //need to pass a token into HTTP options. 
  getMembers(userParams: UserParams){

    var response = this.memberCache.get(Object.values(userParams).join('-'))

    if(response){
      return of(response);
    }

    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);

    params = params.append('minAge', userParams.minAge.toString())
    params = params.append('maxAge', userParams.maxAge.toString())
    params = params.append('gender', userParams.gender)
    params = params.append('orderBy', userParams.orderBy)
    //store members so loading doesn't happen every time. 
    // if(this.members.length>0) return of(this.members);
    
    return getPaginatedResult<Member[]>(this.baseUrl + 'users', params, this.http)
     .pipe(map(response =>{
       this.memberCache.set(Object.values(userParams).join('-'), response);
       return response;
     }))
  }
  
  getMember(username:string){
    //There are no members in the members array. 
    // const member = this.members.find(x => x.username === username);
    // if(member !== undefined) return of(member);

    //find an individual member inside the membercache. 
    //get all of the values of the member cache
    const member = [...this.memberCache.values()]
    //As we call this on the array, we're going to concat every elem.result into the array. 
    .reduce((arr, elem) => arr.concat(elem.result), [])
    .find((member: Member) => member.username === username);

    if(member){
      return of(member);
    }
    //reduce function 
    console.log(member);
    return this.http.get<Member>(this.baseUrl + 'users/'+username);

  }

  updateMember(member: Member){
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    )
  }

  setMainPhoto(photoId: number){
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {});
  }

  deletePhoto(photoId: number){
    return this.http.delete(this.baseUrl+ 'users/delete-photo/'+photoId);
  }

  //Likes functionality
  addLike(username: string){
    //Add empty body for post. 
    return this.http.post(this.baseUrl+'likes/'+username, {});
  }

  getLikes(predicate: string, pageNumber, pageSize){
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('predicate', predicate);
    return getPaginatedResult<Partial<Member[]>>(this.baseUrl+'likes', params, this.http);
  } 
}
