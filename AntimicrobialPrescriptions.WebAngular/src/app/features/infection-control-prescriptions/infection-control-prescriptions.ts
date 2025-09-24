import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrescriptionService } from '../../core/prescription.service';
import { FormsModule } from '@angular/forms';
import { Prescription } from '../../core/prescription.model';
import { PrescriptionStatus } from '../../core/prescription-status.enum';

@Component({
  selector: 'app-infection-control-prescriptions',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './infection-control-prescriptions.html',
  styleUrls: ['./infection-control-prescriptions.css'],
})
export class InfectionControlPrescriptions implements OnInit {
  prescriptions: Prescription[] = [];
  loading = false;
  error: string | null = null;

  PrescriptionStatus = PrescriptionStatus; 

  filterStatus: PrescriptionStatus | 'All' = 'All';
  filterByDrug = '';

  constructor(private prescriptionService: PrescriptionService) {}

  ngOnInit(): void {
    this.loadPrescriptions();
  }

  loadPrescriptions(): void {
    this.loading = true;
    this.prescriptionService.getPrescriptions().subscribe({
      next: (data) => {
        this.prescriptions = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load prescriptions';
        this.loading = false;
        console.error(err);
      }
    });
  }

  filteredPrescriptions(): Prescription[] {
    return this.prescriptions.filter(p =>
      (this.filterStatus === 'All' || p.status === this.filterStatus) &&
      (this.filterByDrug === '' || p.antimicrobialName.toLowerCase().includes(this.filterByDrug.toLowerCase()))
    );
  }

  onMarkReviewed(prescription: Prescription): void {
    this.prescriptionService.markReviewed(prescription.id).subscribe({
      next: () => prescription.status = PrescriptionStatus.Reviewed,
      error: () => alert('Failed to mark as reviewed')
    });
  }

  onDiscontinue(prescription: Prescription): void {
    if (!confirm('Are you sure you want to discontinue this prescription?')) return;

    this.prescriptionService.discontinue(prescription.id).subscribe({
      next: () => prescription.status = PrescriptionStatus.Discontinued,
      error: () => alert('Failed to discontinue prescription')
    });
  }
}
