import { Customers } from 'src/customers/entities/customer.entity';
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
import { Publications } from './publication.entity';

@Entity()
export class PublicationsWishLists {
  @PrimaryColumn('uuid')
  id: string;

  @Column()
  name: string;

  @ManyToOne(() => Customers, (customer) => customer.wish_lists)
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
