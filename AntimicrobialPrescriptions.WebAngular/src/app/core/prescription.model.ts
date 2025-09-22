
import { UserRole } from '../auth/user-role.enum';
import { PrescriptionStatus } from './prescription-status.enum';
export interface Prescription {
  id: string;
  patientId: string;
  antimicrobialName: string;
  dose: string;
  frequency: string;
  route: string;
  indication: string;
  startDate: Date;
  expectedEndDate: Date;
  prescriberName: string;
  prescriberRole: UserRole;
  status: PrescriptionStatus;
  auditLog: AuditEntry[];
}

export interface AuditEntry {
  timestamp: Date;
  user: string;
  action: string;
}
