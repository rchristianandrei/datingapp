import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HomeComponent } from './features/home/home.component';
import { RegisterComponent } from './features/register/register.component';
import { SharedModule } from './modules/shared/shared.module';
import { NotfoundComponent } from './features/notfound/notfound.component';
import { authInterceptor } from './interceptors/auth/auth.interceptor';
import { ComponentsModule } from './components/components.module';
import { MembersModule } from './features/members/members.module';
import { ProfileModule } from './features/profile/profile.module';
import { LoginModule } from './features/login/login.module';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RegisterComponent,
    NotfoundComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    ComponentsModule,
    MembersModule,
    ProfileModule,
    LoginModule,
  ],
  providers: [provideHttpClient(withInterceptors([authInterceptor]))],
  bootstrap: [AppComponent],
})
export class AppModule {}
