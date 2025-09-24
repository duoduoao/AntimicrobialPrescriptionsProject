import { Component, Input } from '@angular/core';
 
import { CommonModule } from '@angular/common';  // Import CommonModule
@Component({
  selector: 'app-status-badge',
  standalone: true,
  imports: [CommonModule], 
  template: `
    <span [ngClass]="badgeClass" class="status-badge">{{ status }}</span>
  `,
  styles: [`
    .status-badge {
      padding: 0.2em 0.6em;
      border-radius: 12px;
      font-size: 0.75rem;
      font-weight: 600;
      color: white;
      text-transform: uppercase;
      display: inline-block;
    }
    .active { background-color: #28a745; }       /* Green */
    .reviewed { background-color: #ffc107; }     /* Yellow */
    .discontinued { background-color: #dc3545; } /* Red */
  `]
})
export class StatusBadge {
  @Input() status: 'Active' | 'Reviewed' | 'Discontinued' = 'Active';

  get badgeClass(): string {
    return this.status.toLowerCase();
  }
}
