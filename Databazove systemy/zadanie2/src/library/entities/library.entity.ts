import { Column, Entity, PrimaryColumn } from "typeorm";

@Entity()
export class Library {
    @PrimaryColumn("uuid")
    id: string;

    @Column()
    name: string;
}
