import { Rentals } from 'src/rentals/entities/borrowings.entity';
import {
  Column,
  CreateDateColumn,
  DeleteDateColumn,
  UpdateDateColumn,
  Entity,
  ManyToOne,
  PrimaryColumn,
} from 'typeorm';
import { Finess } from './finess.entity';

@Entity()
export class Reminders {
  @PrimaryColumn('uuid')
  id: string;

  @ManyToOne(() => Rentals, (borrowing) => borrowing.reminders)
  id_borrowing: Rentals;

  @ManyToOne(() => Finess, (fines) => fines.reminders)
  id_finess: Finess;

  @Column()
  description: string;

  @CreateDateColumn({ type: 'timestamp', default: () => 'CURRENT_TIMESTAMP' })
  created_at: Date;

  @UpdateDateColumn({
    type: 'timestamp',
    default: () => 'CURRENT_TIMESTAMP',
    onUpdate: 'CURRENT_TIMESTAMP',
  })
  updated_at: Date;

  @DeleteDateColumn({
    type: 'timestamp',
    default: null,
  })
  deleted_at: Date;
}
