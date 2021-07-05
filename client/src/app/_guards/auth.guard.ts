import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  //need a constructor to inject our account service and toastr service. 
  constructor(private accountService: AccountService, private toastr:ToastrService){}
  //WE set up our account service as an observable so it can be watched and subscribed to. 
  canActivate(): Observable<boolean>{
    return this.accountService.currentUser$.pipe(
      map(user => {
        if(user) return true;
        this.toastr.error('You shall not Pass!');
        return false;
      })
    )
  }
  
}
