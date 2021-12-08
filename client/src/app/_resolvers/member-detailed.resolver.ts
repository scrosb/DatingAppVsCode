import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { Member } from "../_models/member";
import { MembersService } from "../_services/members.service";


@Injectable({
    providedIn: "root"
})

//There's no way to access navigation extras in resolver, so we have to use the route snapshot
export class MemberDetailedResolver implements Resolve<Member>{

    constructor(private memberService:MembersService){
        
    }

    resolve(route: ActivatedRouteSnapshot): Observable<Member> {
        return this.memberService.getMember(route.paramMap.get('username'));
    }


}