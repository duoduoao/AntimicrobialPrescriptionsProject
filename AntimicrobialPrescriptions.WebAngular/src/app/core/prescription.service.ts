import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Prescription } from './prescription.model';
import { Observable } from 'rxjs';
import { environment } from '../../environments/enviornment';
@Injectable({
  providedIn: 'root',
})
export class PrescriptionService {
  private apiUrl = `${environment.apiBaseUrl}/prescriptions`;

  constructor(private http: HttpClient) {}

  getPrescriptions(filters?: any): Observable<Prescription[]> {
     return this.http.get<Prescription[]>(this.apiUrl, { params: filters });
  }

  getPrescription(id: string): Observable<Prescription> {
    return this.http.get<Prescription>(`${this.apiUrl}/${id}`);
  }

  createPrescription(prescription: Prescription): Observable<Prescription> {
    return this.http.post<Prescription>(this.apiUrl, prescription);
  }

  updatePrescription(id: string, update: Partial<Prescription>): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, update);
  }
  markReviewed(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/review`, {});
  }

  discontinue(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/${id}/discontinue`, {});
  }
}
