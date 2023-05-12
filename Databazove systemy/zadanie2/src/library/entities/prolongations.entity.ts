import { Rentals } from "src/rentals/entities/borrowings.entity";
import { Column, Entity, ManyToOne, PrimaryColumn } from "typeorm";

@Entity()
export class Prolongations {
    @PrimaryColumn("uuid")
    id: string;

    @ManyToOne(() => Rentals, (borrowing) => borrowing.prolongations)
    id_borrowing: Rentals;

    @Column()
    old_return_date: Date;

    @Column()
    new_return_date: Date;
}
