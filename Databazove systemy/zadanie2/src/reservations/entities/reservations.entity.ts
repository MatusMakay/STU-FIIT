import {
  Column,
  CreateDateColumn,
  UpdateDateColumn,
  DeleteDateColumn,
  Entity,
  PrimaryColumn,
  ManyToOne,
  OneToMany,
  OneToOne,
  JoinColumn,
} from 'typeorm';
import { Cards } from '../../customers/entities/cards.entity';
import { Rentals } from '../../rentals/entities/borrowings.entity';
import { Customers } from '../../customers/entities/customer.entity';
import { Publications } from 'src/publication/entities/publication.entity';

@Entity()
export class Reservations {
  @PrimaryColumn('uuid')
  id: string;

  @ManyToOne(() => Customers, (customer) => customer.reservations)
  customer: Customers;

  @ManyToOne(() => Publications)
  @JoinColumn()
  publication: Publications;

  @Column({
    default: null,
    select: false,
  })
  state: string;

  @Column({
    default: null,
    select: false,
  })
  expired: boolean;

  @Column({
    default: null,
    select: false,
  })
  everything_returned: boolean;

  @Column({
    default: null,
    select: false,
  })
  pick_up_date: Date;

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
