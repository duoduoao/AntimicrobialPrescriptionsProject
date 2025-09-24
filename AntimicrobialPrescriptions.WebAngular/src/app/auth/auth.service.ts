import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/enviornment';


interface LoginResponse {
  token: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private currentTokenSubject = new BehaviorSubject<string | null>(null);
  currentToken$ = this.currentTokenSubject.asObservable();

  constructor(private http: HttpClient) {
    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('jwt_token');
      if (token) {
        this.currentTokenSubject.next(token);
      }
    }
  }

  login(userName: string, password: string): Observable<LoginResponse> {
    const url = `${environment.apiBaseUrl}/auth/login`;
    return this.http.post<LoginResponse>(url, { userName, password }).pipe(
      tap(response => {
        if (response.token) {
          localStorage.setItem('jwt_token', response.token);
          this.currentTokenSubject.next(response.token);
        }
      })
    );
  }

  logout(): void {
    if (typeof window !== 'undefined') {
      localStorage.removeItem('jwt_token');
    }
    this.currentTokenSubject.next(null);
  }

  getToken(): string | null {
    return this.currentTokenSubject.value;
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  getRole(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;
    } catch {
      return null;
    }
  }

  hasRole(roles: string[]): boolean {
    const role = this.getRole();
    return role ? roles.includes(role) : false;
  }
}