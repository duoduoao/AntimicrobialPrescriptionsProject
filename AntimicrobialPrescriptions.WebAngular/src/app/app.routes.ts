import { Routes } from '@angular/router';
import { AuthGuard } from './core/auth.guard';
import { PrescriptionForm } from './features/prescription-form/prescription-form';
import { PrescriptionList } from './features/prescription-list/prescription-list';
import { Dashboard } from './features/dashboard/dashboard';
import { Reports } from './features/reports/reports'; 
import { LoginComponent } from './auth/login/login.component';
import { InfectionControlPrescriptions } from './features/infection-control-prescriptions/infection-control-prescriptions';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },  
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: Dashboard, canActivate: [AuthGuard] },
  {
    path: 'prescriptions',
    canActivateChild: [AuthGuard],
    data: { role: 'Clinician' },    
    children: [
      { path: '', component: PrescriptionList },
      { path: 'add', component: PrescriptionForm },
    ]
  },
  {
    path: 'reports',
    component: Reports,
    canActivate: [AuthGuard],
    data: { role: 'InfectionControl' }
  },

   {
    path: 'infection-control-prescriptions',
    component: InfectionControlPrescriptions,
    canActivate: [AuthGuard],
    data: { role: 'InfectionControl' }
  },
    { path: '**', redirectTo: 'login' },
];
