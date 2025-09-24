import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink],
  template: `
    <nav class="navbar">
      <a routerLink="/prescriptions">Prescriptions</a> |
      <a routerLink="/prescriptions/add">Add Prescription</a> |
      <a routerLink="/infection-control-prescriptions">Infection Control</a> |
      <a routerLink="/reports">Reports</a> |
      <!-- <a routerLink="/login-mock">Login Mock</a> -->
    
    </nav>
  `,
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {}
