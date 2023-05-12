import { Reviews } from 'src/customers/entities/reviews.entity';
import {
  Column,
  CreateDateColumn,
  UpdateDateColumn,
  DeleteDateColumn,
  Entity,
  JoinTable,
  ManyToMany,
  OneToMany,
  PrimaryColumn,
} from 'typeorm';
import { Copy } from '../../copy/entities/copy.entity';
import { Autors } from 'src/autors/entities/autor.entity';
import { Populars } from './populars.entity';
import { Genre } from 'src/genre/entities/genre.entity';

@Entity()
export class Publications {
  @PrimaryColumn('uuid')
  id: string;

  @ManyToMany(() => Genre, { cascade: true, onDelete: 'CASCADE' })
  @JoinTable()
  categories: Genre[];

  @ManyToMany(() => Autors, { cascade: true, onDelete: 'CASCADE' })
  @JoinTable()
  authors: Autors[];

  @OneToMany(() => Copy, (copy) => copy.publication)
  copy: Copy[];

  @OneToMany(() => Populars, (popular) => popular.id_publication)
  populars: Populars[];

  @OneToMany(() => Reviews, (review) => review.id_publication)
  reviews: Reviews[];

  @Column({ default: null })
  type: string;

  @Column({ default: null })
  available: string;

  @Column({ default: null })
  pages: number;

  @Column()
  title: string;

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
