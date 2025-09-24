import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PrescriptionService } from '../../core/prescription.service';
import { Prescription } from '../../core/prescription.model';

interface GroupedReportItem {
  drug: string;
  indication: string;
  duration: number;
  count: number;
}

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './reports.html',
  styleUrls: ['./reports.css'],
})
export class Reports implements OnInit {
  groupedData: GroupedReportItem[] = [];
  loading = false;
  error: string | null = null;

  constructor(private prescriptionService: PrescriptionService) {}

  ngOnInit(): void {
    console.log('Reports ngOnInit');
    this.loadReports();
  }

  private loadReports(): void {
    this.loading = true;
    this.prescriptionService.getPrescriptions().subscribe({
      next: (prescriptions: Prescription[]) => {
        this.groupedData = this.groupPrescriptions(prescriptions);
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load prescriptions';
        this.loading = false;
        // console.error('Error loading prescriptions:', err);
      },
    });
  }

  private groupPrescriptions(prescriptions: Prescription[]): GroupedReportItem[] {
    const map = new Map<string, GroupedReportItem>();

    prescriptions.forEach((p) => {
      const duration = this.calculateDurationDays(p.startDate, p.expectedEndDate);
      const key = `${p.antimicrobialName}|${p.indication}|${duration}`;

      if (map.has(key)) {
        map.get(key)!.count++;
      } else {
        map.set(key, {
          drug: p.antimicrobialName,
          indication: p.indication,
          duration,
          count: 1,
        });
      }
    });

    return Array.from(map.values());
  }

  private calculateDurationDays(start?: Date | string, end?: Date | string): number {
    if (!start || !end) return 0;
    const startDate = new Date(start);
    const endDate = new Date(end);
    const diff = endDate.getTime() - startDate.getTime();
    return Math.floor(diff / (1000 * 60 * 60 * 24));
  }
}
