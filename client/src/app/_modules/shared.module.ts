import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';


    //forRoot, means it has some services or components it needs added. 
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right'
    })
  ],
  exports: [
    BsDropdownModule,
    ToastrModule
  ]
})
export class SharedModule { }
