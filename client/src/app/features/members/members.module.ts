import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MembersRoutingModule } from './members-routing.module';
import { MembersComponent } from './members.component';
import { ComponentsModule } from '../../components/components.module';

@NgModule({
  declarations: [MembersComponent],
  imports: [CommonModule, ComponentsModule, MembersRoutingModule],
})
export class MembersModule {}
