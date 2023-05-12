import {
  Column,
  CreateDateColumn,
  UpdateDateColumn,
  DeleteDateColumn,
  Entity,
  OneToMany,
  PrimaryColumn,
} from 'typeorm';
import { Reminders } from './reminders.entity';

@Entity()
export class Finess {
  @PrimaryColumn('uuid')
  id: string;

  @OneToMany(() => Reminders, (reminder) => reminder.id_borrowing)
  reminders: Reminders[];

  @Column()
  sum: number;

  @Column()
  is_payed: boolean;

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
