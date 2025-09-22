import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

import { Prescription } from '../../core/prescription.model';
import { AuthService } from '../../auth/auth.service';
import { UserRole } from '../../auth/user-role.enum';
import { environment } from '../../../environments/enviornment';
import { PrescriptionStatus } from '../../core/prescription-status.enum';

@Component({
  selector: 'app-prescription-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './prescription-form.html',
  styleUrls: ['./prescription-form.css'],
})
export class PrescriptionForm {
  antimicrobialName = signal('');
  patientId = signal('');
  dose = signal('');
  frequency = signal('');
  route = signal('');
  indication = signal('');
  startDate = signal('');
  expectedEndDate = signal('');
  prescriberName = signal('');

  constructor(private authService: AuthService, private http: HttpClient) {}

  get prescriberRole(): UserRole | null {
    const role = this.authService.getRole();
    return Object.values(UserRole).includes(role as UserRole) ? (role as UserRole) : null;
  }

  submit(event: Event) {
    event.preventDefault();  

    if (
      !this.patientId() ||
      !this.antimicrobialName() ||
      !this.dose() ||
      !this.frequency() ||
      !this.route() ||
      !this.indication() ||
      !this.startDate() ||
      !this.expectedEndDate() ||
      !this.prescriberName()
    ) {
      alert('Please fill all required fields.');
      return;
    }

    const newPrescription: Prescription = {
      id: crypto.randomUUID(),
      patientId: this.patientId(),
      antimicrobialName: this.antimicrobialName(),
      dose: this.dose(),
      frequency: this.frequency(),
      route: this.route(),
      indication: this.indication(),
      startDate: new Date(this.startDate()),
      expectedEndDate: new Date(this.expectedEndDate()),
      prescriberName: this.prescriberName(),
      prescriberRole: this.prescriberRole ?? UserRole.Clinician,
      status: PrescriptionStatus.Active,
      auditLog: [
        {
          timestamp: new Date(),
          user: this.prescriberName(),
          action: 'Created',
        },
      ],
    };

    const url = `${environment.apiBaseUrl}/prescriptions`;

   
    this.http.post<Prescription>(url, newPrescription).subscribe({
      next: createdPrescription => {
        console.log('Prescription successfully submitted:', createdPrescription);
        alert('Prescription submitted successfully.');
        this.resetForm();
      },
      error: err => {
        console.error('Failed to submit prescription:', err);
        alert('Failed to submit prescription. Please try again.');
      }
    });
  }

  resetForm() {
    this.antimicrobialName.set('');
    this.patientId.set('');
    this.dose.set('');
    this.frequency.set('');
    this.route.set('');
    this.indication.set('');
    this.startDate.set('');
    this.expectedEndDate.set('');
    this.prescriberName.set('');
  }
}
