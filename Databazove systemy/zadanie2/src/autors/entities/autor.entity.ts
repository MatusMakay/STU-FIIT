import {
  Column,
  CreateDateColumn,
  DeleteDateColumn,
  Entity,
  PrimaryColumn,
  UpdateDateColumn,
} from 'typeorm';

@Entity()
export class Autors {
  @PrimaryColumn('uuid')
  id: string;

  @Column()
  name: string;

  @Column()
  surname: string;

  @Column({
    default: null,
    select: false,
  })
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
    select: false,
  })
  deleted_at: Date;
}
