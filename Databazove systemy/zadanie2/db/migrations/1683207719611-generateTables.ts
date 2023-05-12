import { MigrationInterface, QueryRunner } from "typeorm";

export class GenerateTables1683207719611 implements MigrationInterface {
    name = 'GenerateTables1683207719611'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`CREATE TABLE "autors" ("id" uuid NOT NULL, "name" character varying NOT NULL, "surname" character varying NOT NULL, "description" character varying, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, CONSTRAINT "PK_2de8eb9466ba181d2998cccf14d" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "copy" ("id" uuid NOT NULL, "publisher" character varying NOT NULL, "year" integer NOT NULL, "status" character varying NOT NULL, "type" character varying NOT NULL, "available" boolean NOT NULL DEFAULT false, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, "publicationId" uuid, CONSTRAINT "PK_bb60241fcdd659825ef1ff0fd84" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "populars" ("id" uuid NOT NULL, "description" character varying NOT NULL, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, "idPublicationId" uuid, CONSTRAINT "PK_e763c8104e649250b96a8a74813" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "genre" ("id" uuid NOT NULL, "name" character varying NOT NULL, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, CONSTRAINT "PK_0285d4f1655d080cfcf7d1ab141" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "publications" ("id" uuid NOT NULL, "type" character varying, "available" character varying, "pages" integer, "title" character varying NOT NULL, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, CONSTRAINT "PK_2c4e732b044e09139d2f1065fae" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "reviews" ("id" uuid NOT NULL, "comment" character varying NOT NULL, "num_starts" character varying NOT NULL, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, "idCustomerId" uuid, "idPublicationId" uuid, CONSTRAINT "PK_231ae565c273ee700b283f15c1d" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "reading_lists" ("id" uuid NOT NULL, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, "idCustomerId" uuid, "idPublicationId" uuid, CONSTRAINT "REL_c07b25d1794704b34c13eec725" UNIQUE ("idPublicationId"), CONSTRAINT "PK_38dba929b13ab9c59c8eee96ba2" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "publications_wish_lists" ("id" uuid NOT NULL, "name" character varying NOT NULL, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, "idCustomerId" uuid, "idPublicationId" uuid, CONSTRAINT "REL_00d43657d2b5428570c810a2cd" UNIQUE ("idPublicationId"), CONSTRAINT "PK_863aa09600800a4609df823d2ea" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "reservations" ("id" uuid NOT NULL, "state" character varying, "expired" boolean, "everything_returned" boolean, "pick_up_date" TIMESTAMP, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, "customerId" uuid, "publicationId" uuid, CONSTRAINT "PK_da95cef71b617ac35dc5bcda243" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "prolongations" ("id" uuid NOT NULL, "old_return_date" TIMESTAMP NOT NULL, "new_return_date" TIMESTAMP NOT NULL, "idBorrowingId" uuid, CONSTRAINT "PK_d134e01faac444957110fbdf555" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "finess" ("id" uuid NOT NULL, "sum" integer NOT NULL, "is_payed" boolean NOT NULL, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, CONSTRAINT "PK_170c806d47d3885037cc64ac5f0" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "reminders" ("id" uuid NOT NULL, "description" character varying NOT NULL, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, "idBorrowingId" uuid, "idFinessId" uuid, CONSTRAINT "PK_38715fec7f634b72c6cf7ea4893" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "rentals" ("id" uuid NOT NULL, "status" character varying, "start_date" TIMESTAMP NOT NULL, "end_date" TIMESTAMP NOT NULL, "duration" integer NOT NULL, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, "customerId" uuid, "copyId" uuid, CONSTRAINT "PK_2b10d04c95a8bfe85b506ba52ba" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "customers" ("id" uuid NOT NULL, "name" character varying NOT NULL, "surname" character varying NOT NULL, "birth_date" TIMESTAMP NOT NULL, "email" character varying NOT NULL, "personal_identificator" character varying NOT NULL, "hasParent" boolean NOT NULL DEFAULT false, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, CONSTRAINT "PK_133ec679a801fab5e070f73d3ea" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "cards" ("id" uuid NOT NULL, "magstripe" character varying NOT NULL, "status" character varying NOT NULL, "created_at" TIMESTAMP NOT NULL DEFAULT now(), "updated_at" TIMESTAMP NOT NULL DEFAULT now(), "deleted_at" TIMESTAMP, "userId" uuid, CONSTRAINT "REL_7b7230897ecdeb7d6b0576d907" UNIQUE ("userId"), CONSTRAINT "PK_5f3269634705fdff4a9935860fc" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "library" ("id" uuid NOT NULL, "name" character varying NOT NULL, CONSTRAINT "PK_3a61ae2e897d9b5a59a64e91aa4" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "publications_categories_genre" ("publicationsId" uuid NOT NULL, "genreId" uuid NOT NULL, CONSTRAINT "PK_8f507d6f645dc79e7114a82dd7d" PRIMARY KEY ("publicationsId", "genreId"))`);
        await queryRunner.query(`CREATE INDEX "IDX_c14380d470a74fdf17c59cafb4" ON "publications_categories_genre" ("publicationsId") `);
        await queryRunner.query(`CREATE INDEX "IDX_0c48ac4512c2e599f5368d808e" ON "publications_categories_genre" ("genreId") `);
        await queryRunner.query(`CREATE TABLE "publications_authors_autors" ("publicationsId" uuid NOT NULL, "autorsId" uuid NOT NULL, CONSTRAINT "PK_8f71018b693e36ebdbbd81ff955" PRIMARY KEY ("publicationsId", "autorsId"))`);
        await queryRunner.query(`CREATE INDEX "IDX_479d0a649ac400b2333dbb8b7d" ON "publications_authors_autors" ("publicationsId") `);
        await queryRunner.query(`CREATE INDEX "IDX_e0b33ab0cbf4676fc104b9dc05" ON "publications_authors_autors" ("autorsId") `);
        await queryRunner.query(`ALTER TABLE "copy" ADD CONSTRAINT "FK_2011525112301d7db90cc96ec3e" FOREIGN KEY ("publicationId") REFERENCES "publications"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "populars" ADD CONSTRAINT "FK_7e5e65a337bcb098dd437c92d5e" FOREIGN KEY ("idPublicationId") REFERENCES "publications"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "reviews" ADD CONSTRAINT "FK_91a7c955b10b80d97c5cabd9868" FOREIGN KEY ("idCustomerId") REFERENCES "customers"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "reviews" ADD CONSTRAINT "FK_1207105b7d2ca2c45f72a62edb9" FOREIGN KEY ("idPublicationId") REFERENCES "publications"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "reading_lists" ADD CONSTRAINT "FK_4c010eeafb08660f99c76882b7e" FOREIGN KEY ("idCustomerId") REFERENCES "customers"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "reading_lists" ADD CONSTRAINT "FK_c07b25d1794704b34c13eec7258" FOREIGN KEY ("idPublicationId") REFERENCES "publications"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "publications_wish_lists" ADD CONSTRAINT "FK_8a7ac7ff7e575dd6add9c634de9" FOREIGN KEY ("idCustomerId") REFERENCES "customers"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "publications_wish_lists" ADD CONSTRAINT "FK_00d43657d2b5428570c810a2cde" FOREIGN KEY ("idPublicationId") REFERENCES "publications"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "reservations" ADD CONSTRAINT "FK_487ec4ed757eed0d34c7ddee79b" FOREIGN KEY ("customerId") REFERENCES "customers"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "reservations" ADD CONSTRAINT "FK_73788f054d669651cb8c2b618d4" FOREIGN KEY ("publicationId") REFERENCES "publications"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "prolongations" ADD CONSTRAINT "FK_9c80e54603bb6f1fe982c45285e" FOREIGN KEY ("idBorrowingId") REFERENCES "rentals"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "reminders" ADD CONSTRAINT "FK_6d3d673a6f5aad418744a91702c" FOREIGN KEY ("idBorrowingId") REFERENCES "rentals"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "reminders" ADD CONSTRAINT "FK_cd51ee54bb77526f1268281495b" FOREIGN KEY ("idFinessId") REFERENCES "finess"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "rentals" ADD CONSTRAINT "FK_c2d7d6dba8d2e356f8017cc8d49" FOREIGN KEY ("customerId") REFERENCES "customers"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "rentals" ADD CONSTRAINT "FK_e49aa38cf46ecf44fcd6cb2861a" FOREIGN KEY ("copyId") REFERENCES "copy"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "cards" ADD CONSTRAINT "FK_7b7230897ecdeb7d6b0576d907b" FOREIGN KEY ("userId") REFERENCES "customers"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
        await queryRunner.query(`ALTER TABLE "publications_categories_genre" ADD CONSTRAINT "FK_c14380d470a74fdf17c59cafb44" FOREIGN KEY ("publicationsId") REFERENCES "publications"("id") ON DELETE CASCADE ON UPDATE CASCADE`);
        await queryRunner.query(`ALTER TABLE "publications_categories_genre" ADD CONSTRAINT "FK_0c48ac4512c2e599f5368d808e0" FOREIGN KEY ("genreId") REFERENCES "genre"("id") ON DELETE CASCADE ON UPDATE CASCADE`);
        await queryRunner.query(`ALTER TABLE "publications_authors_autors" ADD CONSTRAINT "FK_479d0a649ac400b2333dbb8b7dd" FOREIGN KEY ("publicationsId") REFERENCES "publications"("id") ON DELETE CASCADE ON UPDATE CASCADE`);
        await queryRunner.query(`ALTER TABLE "publications_authors_autors" ADD CONSTRAINT "FK_e0b33ab0cbf4676fc104b9dc05f" FOREIGN KEY ("autorsId") REFERENCES "autors"("id") ON DELETE CASCADE ON UPDATE CASCADE`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "publications_authors_autors" DROP CONSTRAINT "FK_e0b33ab0cbf4676fc104b9dc05f"`);
        await queryRunner.query(`ALTER TABLE "publications_authors_autors" DROP CONSTRAINT "FK_479d0a649ac400b2333dbb8b7dd"`);
        await queryRunner.query(`ALTER TABLE "publications_categories_genre" DROP CONSTRAINT "FK_0c48ac4512c2e599f5368d808e0"`);
        await queryRunner.query(`ALTER TABLE "publications_categories_genre" DROP CONSTRAINT "FK_c14380d470a74fdf17c59cafb44"`);
        await queryRunner.query(`ALTER TABLE "cards" DROP CONSTRAINT "FK_7b7230897ecdeb7d6b0576d907b"`);
        await queryRunner.query(`ALTER TABLE "rentals" DROP CONSTRAINT "FK_e49aa38cf46ecf44fcd6cb2861a"`);
        await queryRunner.query(`ALTER TABLE "rentals" DROP CONSTRAINT "FK_c2d7d6dba8d2e356f8017cc8d49"`);
        await queryRunner.query(`ALTER TABLE "reminders" DROP CONSTRAINT "FK_cd51ee54bb77526f1268281495b"`);
        await queryRunner.query(`ALTER TABLE "reminders" DROP CONSTRAINT "FK_6d3d673a6f5aad418744a91702c"`);
        await queryRunner.query(`ALTER TABLE "prolongations" DROP CONSTRAINT "FK_9c80e54603bb6f1fe982c45285e"`);
        await queryRunner.query(`ALTER TABLE "reservations" DROP CONSTRAINT "FK_73788f054d669651cb8c2b618d4"`);
        await queryRunner.query(`ALTER TABLE "reservations" DROP CONSTRAINT "FK_487ec4ed757eed0d34c7ddee79b"`);
        await queryRunner.query(`ALTER TABLE "publications_wish_lists" DROP CONSTRAINT "FK_00d43657d2b5428570c810a2cde"`);
        await queryRunner.query(`ALTER TABLE "publications_wish_lists" DROP CONSTRAINT "FK_8a7ac7ff7e575dd6add9c634de9"`);
        await queryRunner.query(`ALTER TABLE "reading_lists" DROP CONSTRAINT "FK_c07b25d1794704b34c13eec7258"`);
        await queryRunner.query(`ALTER TABLE "reading_lists" DROP CONSTRAINT "FK_4c010eeafb08660f99c76882b7e"`);
        await queryRunner.query(`ALTER TABLE "reviews" DROP CONSTRAINT "FK_1207105b7d2ca2c45f72a62edb9"`);
        await queryRunner.query(`ALTER TABLE "reviews" DROP CONSTRAINT "FK_91a7c955b10b80d97c5cabd9868"`);
        await queryRunner.query(`ALTER TABLE "populars" DROP CONSTRAINT "FK_7e5e65a337bcb098dd437c92d5e"`);
        await queryRunner.query(`ALTER TABLE "copy" DROP CONSTRAINT "FK_2011525112301d7db90cc96ec3e"`);
        await queryRunner.query(`DROP INDEX "public"."IDX_e0b33ab0cbf4676fc104b9dc05"`);
        await queryRunner.query(`DROP INDEX "public"."IDX_479d0a649ac400b2333dbb8b7d"`);
        await queryRunner.query(`DROP TABLE "publications_authors_autors"`);
        await queryRunner.query(`DROP INDEX "public"."IDX_0c48ac4512c2e599f5368d808e"`);
        await queryRunner.query(`DROP INDEX "public"."IDX_c14380d470a74fdf17c59cafb4"`);
        await queryRunner.query(`DROP TABLE "publications_categories_genre"`);
        await queryRunner.query(`DROP TABLE "library"`);
        await queryRunner.query(`DROP TABLE "cards"`);
        await queryRunner.query(`DROP TABLE "customers"`);
        await queryRunner.query(`DROP TABLE "rentals"`);
        await queryRunner.query(`DROP TABLE "reminders"`);
        await queryRunner.query(`DROP TABLE "finess"`);
        await queryRunner.query(`DROP TABLE "prolongations"`);
        await queryRunner.query(`DROP TABLE "reservations"`);
        await queryRunner.query(`DROP TABLE "publications_wish_lists"`);
        await queryRunner.query(`DROP TABLE "reading_lists"`);
        await queryRunner.query(`DROP TABLE "reviews"`);
        await queryRunner.query(`DROP TABLE "publications"`);
        await queryRunner.query(`DROP TABLE "genre"`);
        await queryRunner.query(`DROP TABLE "populars"`);
        await queryRunner.query(`DROP TABLE "copy"`);
        await queryRunner.query(`DROP TABLE "autors"`);
    }

}
