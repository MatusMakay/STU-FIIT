import {
  CreateDateColumn,
  UpdateDateColumn,
  DeleteDateColumn,
  Entity,
  PrimaryColumn,
  OneToOne,
  OneToMany,
  JoinColumn,
  Column,
} from 'typeorm';
import { Customers } from './customer.entity';

@Entity()
export class Cards {
  @PrimaryColumn('uuid')
  id: string;

  @OneToOne(() => Customers, (customer) => customer.id_card)
  @JoinColumn()
  user: Customers;

  @Column()
  magstripe: string;

  @Column()
  status: string;

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
