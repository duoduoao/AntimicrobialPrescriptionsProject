import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
 

import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],  
  template: `
    <h2>Dashboard</h2>
    <nav>
      <a routerLink="/prescriptions">Prescriptions</a> |
      <a routerLink="/prescriptions/add">Add Prescription</a> |
      <a routerLink="/reports">Reports</a> |
      <a routerLink="/login-mock">Login Mock</a>
    </nav>
  `
})
export class Dashboard {}
