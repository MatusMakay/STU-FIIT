import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Finess } from './entities/finess.entity';
import { Library } from './entities/library.entity';
import { Prolongations } from './entities/prolongations.entity';
import { Reminders } from './entities/reminders.entity';


@Module({
    imports: [
        TypeOrmModule.forFeature([
            Finess,
            Library,
            Prolongations,
            Reminders
        ])
    ]
})
export class LibraryModule {}
