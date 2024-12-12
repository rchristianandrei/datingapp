import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavComponent } from './nav/nav.component';
import { UsercardComponent } from './usercard/usercard.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [NavComponent, UsercardComponent],
  imports: [CommonModule, RouterModule, BsDropdownModule],
  exports: [NavComponent, UsercardComponent],
})
export class ComponentsModule {}
