import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ToastrModule } from 'ngx-toastr';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      autoDismiss: true,
      positionClass: 'toast-bottom-right',
    }),
    TabsModule.forRoot(),
    BsDropdownModule.forRoot(),
  ],
  exports: [BsDropdownModule, TabsModule],
})
export class SharedModule {}
