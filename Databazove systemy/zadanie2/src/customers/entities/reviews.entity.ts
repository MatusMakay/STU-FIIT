import {
  Column,
  CreateDateColumn,
  UpdateDateColumn,
  DeleteDateColumn,
  Entity,
  PrimaryColumn,
  ManyToOne,
} from 'typeorm';
import { Customers } from './customer.entity';
import { Publications } from 'src/publication/entities/publication.entity';

@Entity()
export class Reviews {
  @PrimaryColumn('uuid')
  id: string;

  @ManyToOne(() => Customers, (customer) => customer.reviews)
  id_customer: Customers;

  @ManyToOne(() => Publications, (publication) => publication.reviews)
  id_publication: Publications;

  @Column()
  comment: string;

  @Column()
  num_starts: string;

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
