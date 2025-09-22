import { Component, signal, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { Prescription } from '../../core/prescription.model';
import { AuthService } from '../../auth/auth.service';
import { UserRole } from '../../auth/user-role.enum';
import { environment } from '../../../environments/enviornment';
import { PrescriptionStatus } from '../../core/prescription-status.enum';

@Component({
  selector: 'app-prescription-list',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './prescription-list.html',
  styleUrls: ['./prescription-list.css'],
})
export class PrescriptionList {
  prescriptions = signal<Prescription[]>([]);

filterStatus = signal<PrescriptionStatus | 'All'>('All');
  filterByDrug = signal<string>('');

    PrescriptionStatus = PrescriptionStatus; 
  filteredPrescriptions = computed(() => {
    return this.prescriptions().filter(p => 
      (this.filterStatus() === 'All' || p.status === this.filterStatus()) &&
      (this.filterByDrug() === '' || p.antimicrobialName.toLowerCase().includes(this.filterByDrug().toLowerCase()))
    );
  });

  constructor(private authService: AuthService, private http: HttpClient) {
    this.loadPrescriptions();
  }

loadPrescriptions() {
  const url = `${environment.apiBaseUrl}/prescriptions`;
  this.http.get<Prescription[]>(url).subscribe({
    next: data => this.prescriptions.set(data),
    error: err => console.error('Failed to load prescriptions:', err)
  });
}

  canReview() {
    return this.authService.hasRole([UserRole.InfectionControl]);
  }

reviewPrescription(p: Prescription) {
  if(this.canReview()) {
    const url = `${environment.apiBaseUrl}/prescriptions/${p.id}/review`;
    this.http.post(url, {}).subscribe({
      next: () => {
       p.status = PrescriptionStatus.Reviewed;
        p.auditLog.push({ timestamp: new Date(), user: 'Infection Control User', action: 'Reviewed' });
        this.prescriptions.update(list => [...list]);
      },
      error: err => console.error('Failed to mark reviewed', err)
    });
  }
}

discontinuePrescription(p: Prescription) {
  if(this.canReview()) {
    const url = `${environment.apiBaseUrl}/prescriptions/${p.id}/discontinue`;
    this.http.post(url, {}).subscribe({
      next: () => {
        p.status = PrescriptionStatus.Discontinued;
        p.auditLog.push({ timestamp: new Date(), user: 'Infection Control User', action: 'Discontinued' });
        this.prescriptions.update(list => [...list]);
      },
      error: err => console.error('Failed to mark discontinued', err)
    });
  }
}
}
