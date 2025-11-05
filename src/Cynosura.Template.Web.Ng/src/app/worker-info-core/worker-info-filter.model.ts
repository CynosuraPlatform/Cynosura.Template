import { EntityFilter } from '../core/models/entity-filter.model';

export class WorkerInfoFilter extends EntityFilter {
  name?: string;
  className?: string;
  retryCountFrom?: number;
  retryCountTo?: number;
  retryIntervalFrom?: string;
  retryIntervalTo?: string;
}
