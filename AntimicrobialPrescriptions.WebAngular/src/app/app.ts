import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router'; 

import { NavbarComponent } from './navbar/navbar.component';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component'; 


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, RouterModule],
  template: `
    <app-navbar></app-navbar> 
 
    <router-outlet></router-outlet>
  `,
})
export class App {}
