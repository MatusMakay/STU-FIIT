import {
  CreateDateColumn,
  Column,
  UpdateDateColumn,
  DeleteDateColumn,
  Entity,
  PrimaryColumn,
  OneToOne,
  JoinColumn,
  OneToMany,
} from 'typeorm';
import { Cards } from './cards.entity';
import { Reviews } from './reviews.entity';
import { ReadingLists } from './reading-lists.entity';
import { PublicationsWishLists } from 'src/publication/entities/publication-wishlists.entity';
import { Reservations } from '../../reservations/entities/reservations.entity';
import { Rentals } from '../../rentals/entities/borrowings.entity';

@Entity()
export class Customers {
  @PrimaryColumn('uuid')
  id: string;

  /**
   * RELATIONS:
   * Cards, Reviews, Reading Lists, Publications Wish Lists
   */
  @OneToOne(() => Cards, (card) => card.user)
  id_card: Cards;

  @OneToMany(() => Reviews, (review) => review.id_customer)
  reviews: Reviews[];

  @OneToMany(() => ReadingLists, (reading_list) => reading_list.id_customer)
  reading_lists: Reviews[];

  @OneToMany(() => PublicationsWishLists, (wish_list) => wish_list.id_customer)
  wish_lists: PublicationsWishLists[];

  @OneToMany(() => Reservations, (reservation) => reservation.customer)
  reservations: Reservations[];

  @OneToMany(() => Rentals, (borrowing) => borrowing.customer)
  borrowings: Rentals[];

  @Column()
  name: string;

  @Column()
  surname: string;

  @Column({ type: 'date' })
  birth_date: Date;

  @Column()
  email: string;

  @Column()
  personal_identificator: string;

  @Column({ default: false })
  hasParent: boolean;

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
