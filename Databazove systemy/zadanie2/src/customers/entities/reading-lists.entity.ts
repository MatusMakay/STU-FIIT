import {
  CreateDateColumn,
  UpdateDateColumn,
  DeleteDateColumn,
  Entity,
  PrimaryColumn,
  ManyToOne,
  ManyToMany,
  OneToOne,
  JoinColumn,
} from 'typeorm';
import { Customers } from './customer.entity';
import { Publications } from 'src/publication/entities/publication.entity';

@Entity()
export class ReadingLists {
  @PrimaryColumn('uuid')
  id: string;

  @ManyToOne(() => Customers, (customer) => customer.reading_lists)
  id_customer: Customers;

  @OneToOne(() => Publications)
  @JoinColumn()
  id_publication: Publications;

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
