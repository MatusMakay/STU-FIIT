import {
  Column,
  CreateDateColumn,
  DeleteDateColumn,
  UpdateDateColumn,
  Entity,
  ManyToOne,
  PrimaryColumn,
} from 'typeorm';
import { Publications } from './publication.entity';

@Entity()
export class Populars {
  @PrimaryColumn('uuid')
  id: string;

  @ManyToOne(() => Publications, (publication) => publication.populars)
  id_publication: Publications;

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
