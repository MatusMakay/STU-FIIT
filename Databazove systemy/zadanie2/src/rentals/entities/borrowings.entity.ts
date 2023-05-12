import {
  Column,
  CreateDateColumn,
  DeleteDateColumn,
  Entity,
  JoinColumn,
  JoinTable,
  ManyToMany,
  ManyToOne,
  OneToMany,
  OneToOne,
  PrimaryColumn,
  PrimaryGeneratedColumn,
  UpdateDateColumn,
} from 'typeorm';
import { Prolongations } from 'src/library/entities/prolongations.entity';
import { Reminders } from 'src/library/entities/reminders.entity';
import { Customers } from '../../customers/entities/customer.entity';
import { Copy } from 'src/copy/entities/copy.entity';

@Entity()
export class Rentals {
  @PrimaryColumn('uuid')
  id: string;

  @ManyToOne(() => Customers, (customer) => customer.borrowings)
  customer: Customers;

  @OneToMany(() => Prolongations, (prolongation) => prolongation.id_borrowing)
  prolongations: Prolongations[];

  @OneToMany(() => Reminders, (reminder) => reminder.id_borrowing)
  reminders: Reminders[];

  @ManyToOne(() => Copy)
  @JoinColumn()
  copy: Copy;

  @Column({ default: null })
  status: string;

  @Column()
  start_date: Date;

  @Column()
  end_date: Date;

  @Column()
  duration: number;

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
