import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './features/login/login.component';
import { HomeComponent } from './features/home/home.component';
import { RegisterComponent } from './features/register/register.component';
import { SharedModule } from './modules/shared/shared.module';
import { NotfoundComponent } from './features/notfound/notfound.component';
import { ProfileComponent } from './features/profile/profile.component';
import { authInterceptor } from './interceptors/auth/auth.interceptor';
import { ComponentsModule } from './components/components.module';
import { MembersModule } from './features/members/members.module';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    NotfoundComponent,
    ProfileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    ComponentsModule,
    MembersModule,
  ],
  providers: [provideHttpClient(withInterceptors([authInterceptor]))],
  bootstrap: [AppComponent],
})
export class AppModule {}
