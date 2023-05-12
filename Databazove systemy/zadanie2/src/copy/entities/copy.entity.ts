import {
  Column,
  CreateDateColumn,
  UpdateDateColumn,
  DeleteDateColumn,
  Entity,
  PrimaryColumn,
  ManyToOne,
  JoinColumn,
  OneToMany,
  JoinTable,
} from 'typeorm';
import { Publications } from '../../publication/entities/publication.entity';
import { Rentals } from 'src/rentals/entities/borrowings.entity';

@Entity()
export class Copy {
  @PrimaryColumn('uuid')
  id: string;

  @ManyToOne(() => Publications)
  publication: Publications;

  @Column()
  publisher: string;

  @Column()
  year: number;

  @Column()
  status: string;

  @Column()
  type: string;

  @Column({ default: false, select: false })
  available: boolean;

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
