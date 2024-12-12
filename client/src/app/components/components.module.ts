import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavComponent } from './nav/nav.component';
import { UsercardComponent } from './usercard/usercard.component';
import { AppRoutingModule } from '../app-routing.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
  declarations: [NavComponent, UsercardComponent],
  imports: [CommonModule, AppRoutingModule, BsDropdownModule],
  exports: [NavComponent, UsercardComponent],
})
export class ComponentsModule {}
